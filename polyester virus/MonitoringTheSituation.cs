namespace polyester_virus
{
    public partial class MonitoringTheSituation : Form
    {
        public MonitoringTheSituation()
        {
            InitializeComponent();
            progressBar1.MarqueeAnimationSpeed = 100;
            StartPosition = FormStartPosition.Manual;
            Rectangle area = Screen.PrimaryScreen!.WorkingArea;
            int x = Random.Shared.Next(area.Left, area.Right - Width);
            int y = Random.Shared.Next(area.Top, area.Bottom - Height);
            Location = new Point(x, y);
            TopMost = true;
        }
    }
}
