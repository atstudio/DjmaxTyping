using System.Windows;
using System.ComponentModel;

namespace Typing {
    public partial class App : Application {
        Configure cfg = new Configure();
        private void Run(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Closing += SaveWindowPosition;
            RestoreWindow();
            MainWindow.Show();
        }
        protected void RestoreWindow()
        {
            string windowTopValue = (string)cfg.Get("top");
            string windowLeftValue = (string)cfg.Get("left");
            string windowWidthValue = (string)cfg.Get("width");
            string windowHeightValue = (string)cfg.Get("height");

            double windowPositionTop;
            double windowPositionLeft;
            double windowWidth;
            double windowHeight;

            if (double.TryParse(windowTopValue, out windowPositionTop)
                && double.TryParse(windowLeftValue, out windowPositionLeft)
                && double.TryParse(windowWidthValue, out windowWidth)
                && double.TryParse(windowHeightValue, out windowHeight))
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
