using launcherdotnet.Windows;
using LibVLCSharp.Shared;
using polyester_virus.Windows;

namespace polyester_virus
{
    public partial class Form1 : Form
    {
        public bool DoVirus = false;
        MediaPlayer audioPlayer;

        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Black;
            DwmApi.SetAccentState(Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, 0x27950366);
            DwmApi.ExtendFrame(Handle);
            Draggable.MakeDraggable(this);
            Media media = new Media(
                Program.libVLC,
                new StreamMediaInput(Resources.v4med_bark_fart));
            audioPlayer = new MediaPlayer(Program.libVLC);
            audioPlayer.Play(media);
            Text = "spider polyseter man so tuff 🥶🥶";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DoVirus)
            {
                audioPlayer.Stop();
                return;
            }
            Media media = new Media(
                Program.libVLC,
                new StreamMediaInput(Resources.wet_fart));
            audioPlayer = new MediaPlayer(Program.libVLC);
            audioPlayer.Play(media);
            e.Cancel = true;
            base.OnFormClosing(e);
        }

        void button()
        {
            audioPlayer.Stop();
            DoVirus = true;
            Close();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            button();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button();
        }
    }
}
