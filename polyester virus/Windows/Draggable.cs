using System.Runtime.InteropServices;

namespace polyester_virus.Windows
{
    public static partial class Draggable
    {
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

        public static void MakeDraggable(Control control)
        {
            control.MouseDown += (_, e) =>
            {
                if (e.Button != MouseButtons.Left) return;

                ReleaseCapture();
                SendMessage(
                    control.Handle,
                    WM_NCLBUTTONDOWN,
                    new IntPtr(HTCAPTION),
                    IntPtr.Zero);
            };
        }

        public static void MakeDraggableRecursive(Control control)
        {
            control.MouseDown += (_, e) =>
            {
                if (e.Button != MouseButtons.Left) return;

                ReleaseCapture();
                SendMessage(
                    control.Handle,
                    WM_NCLBUTTONDOWN,
                    new IntPtr(HTCAPTION),
                    IntPtr.Zero);
            };

            foreach (Control child in control.Controls)
                MakeDraggableRecursive(child);
        }
    }
}
