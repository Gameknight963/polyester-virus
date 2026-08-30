using LibVLCSharp.Shared;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace polyester_virus
{
    public partial class Form2 : Form
    {
        private static Media? media;

        private MediaPlayer mediaPlayer;

        private static List<Form2> instances = new();

        private readonly System.Windows.Forms.Timer movementTimer = new();
        private Point velocity;
        Form2? target;

        public Form2()
        {
            InitializeComponent();
            instances.Add(this);
            MakeDraggable(this);

            mediaPlayer = new MediaPlayer(Program.libVLC);

            media ??= new(
                Program.libVLC,
                new StreamMediaInput(
                    new MemoryStream(Resources.polyester_video)));

            videoView1.MediaPlayer = mediaPlayer;

            StartPosition = FormStartPosition.Manual;

            movementTimer.Interval = 10;
            velocity = new Point(
                Random.Shared.Next(-8, 9),
                Random.Shared.Next(-8, 9));

            Rectangle area = Screen.PrimaryScreen!.WorkingArea;
            int x = Random.Shared.Next(area.Left, area.Right - Width);
            int y = Random.Shared.Next(area.Top, area.Bottom - Height);
            Location = new Point(x, y);

            Form2 target = instances[Random.Shared.Next(instances.Count)];

            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
        }

        private void MovementTimer_Tick(object? sender, EventArgs e)
        {
            Rectangle area = Screen.PrimaryScreen!.WorkingArea;

            if (target != null)
            {
                int dx = target.Left - Left;
                int dy = target.Top - Top;
                if (Math.Abs(dx) > 10)
                    velocity.X += Math.Sign(dx);

                if (Math.Abs(dy) > 10)
                    velocity.Y += Math.Sign(dy);
            }

            int newX = Left + velocity.X;
            int newY = Top + velocity.Y;

            if (newX <= area.Left || newX + Width >= area.Right)
            {
                velocity.X = -velocity.X;
                newX = Math.Clamp(newX, area.Left, area.Right - Width);
            }

            if (newY <= area.Top || newY + Height >= area.Bottom)
            {
                velocity.Y = -velocity.Y;
                newY = Math.Clamp(newY, area.Top, area.Bottom - Height);
            }

            Location = new Point(newX, newY);

            if (Random.Shared.Next(100) < 3)
            {
                velocity = new Point(
                    Random.Shared.Next(-100, 100),
                    Random.Shared.Next(-100, 100));
            }
        }

        public static Form2 Create()
        {
            Form2 form = new();
            form.TopMost = true;
            SynchronizationContext context = SynchronizationContext.Current
                ?? throw new InvalidOperationException("No synchronization context (wtf)");

            form.mediaPlayer.Playing += async (object? sender, EventArgs e) =>
            {
                form.mediaPlayer.EnableKeyInput = false;
                form.mediaPlayer.EnableMouseInput = false;
            };

            form.mediaPlayer.TimeChanged += (object? sender, MediaPlayerTimeChangedEventArgs e) =>
            {
                if (e.Time > 1)
                {
                    context.Post(_ =>
                    {
                        form.Show();
                        form.BringToFront();
                    }, null);
                }
            };

            form.mediaPlayer.Play(media!);

            return form;
        }

        private bool closing = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (closing) return;
            e.Cancel = true;
            base.OnFormClosing(e);
        }

        public static void CloseAll()
        {
            foreach (Form2 form in instances)
            {
                form.closing = true;
                form.Close();
                form.Dispose();
            }
            instances.Clear();
        }

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW")]
        private static partial IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int HTCAPTION = 2;

        private void MakeDraggable(Control control)
        {
            control.MouseDown += (_, e) =>
            {
                if (e.Button != MouseButtons.Left)
                    return;

                ReleaseCapture();
                SendMessage(
                    Handle,
                    WM_NCLBUTTONDOWN,
                    new IntPtr(HTCAPTION),
                    IntPtr.Zero);
            };

            foreach (Control child in control.Controls)
                MakeDraggable(child);
        }
    }
}
