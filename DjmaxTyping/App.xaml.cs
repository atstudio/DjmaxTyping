using System.ComponentModel;
using System.Windows;

namespace Typing
{
    public partial class App : Application
    {
        private readonly Configure cfg = new Configure();
        private void Run(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Closing += SaveWindowPosition;
            RestoreWindow();
            MainWindow.Show();
        }
        protected void RestoreWindow()
        {
            if (double.TryParse((string)cfg.Get("top"), out double windowPositionTop)
                && double.TryParse((string)cfg.Get("left"), out double windowPositionLeft)
                && double.TryParse((string)cfg.Get("width"), out double windowWidth)
                && double.TryParse((string)cfg.Get("height"), out double windowHeight))
            {
                MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                MainWindow.Top = windowPositionTop;
                MainWindow.Left = windowPositionLeft;
                MainWindow.Width = windowWidth;
                MainWindow.Height = windowHeight;
            }
        }

        private void SaveWindowPosition(object sender, CancelEventArgs e)
        {
            cfg.Put("top", MainWindow.Top.ToString("G"));
            cfg.Put("left", MainWindow.Left.ToString("G"));
            cfg.Put("width", MainWindow.Width.ToString("G"));
            cfg.Put("height", MainWindow.Height.ToString("G"));
        }
    }
}
