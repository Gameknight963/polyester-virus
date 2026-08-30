using launcherdotnet.Windows;

namespace polyester_virus
{
    public partial class BackgroundBlur : Form
    {
        public BackgroundBlur()
        {
            InitializeComponent();
            BackColor = Color.Black;
            DwmApi.SetAccentState(Handle, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND);
            StartPosition = FormStartPosition.Manual;
            Size = Screen.PrimaryScreen!.Bounds.Size;
            Location = Screen.PrimaryScreen.Bounds.Location;
            TopMost = true;
        }
    }
}
