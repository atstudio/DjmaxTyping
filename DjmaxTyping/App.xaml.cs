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
            string currentWindowPositionTop = MainWindow.Top.ToString("G");
            string currentWindowPositionLeft = MainWindow.Left.ToString("G");
            string currentWindowWidth = MainWindow.Width.ToString("G");
            string currentWindowHeight = MainWindow.Height.ToString("G");

            cfg.Put("top", currentWindowPositionTop);
            cfg.Put("left", currentWindowPositionLeft);
            cfg.Put("width", currentWindowWidth);
            cfg.Put("height", currentWindowHeight);
        }
    }
}
