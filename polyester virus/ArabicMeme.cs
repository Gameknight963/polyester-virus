using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace polyester_virus
{
    public partial class ArabicMeme : Form
    {
        public ArabicMeme()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Rectangle area = Screen.PrimaryScreen!.WorkingArea;
            int x = Random.Shared.Next(area.Left, area.Right - Width);
            int y = Random.Shared.Next(area.Top, area.Bottom - Height);
            Location = new Point(x, y);
            TopMost = true;
        }
    }
}
