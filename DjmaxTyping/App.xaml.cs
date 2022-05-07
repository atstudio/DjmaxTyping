using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace Typing {
    public partial class App : Application {
        Configure cfg = new Configure();
        private void Run(object sender, StartupEventArgs e)
        {
            this.MainWindow = new MainWindow();
            this.MainWindow.Closing += SaveWindowPosition;
            RestoreWindow();
            this.MainWindow.Show();
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
                this.MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                this.MainWindow.Top = windowPositionTop;
                this.MainWindow.Left = windowPositionLeft;
                this.MainWindow.Width = windowWidth;
                this.MainWindow.Height = windowHeight;
            }
        }

        private void SaveWindowPosition(object sender, CancelEventArgs e)
        {
            string currentWindowPositionTop = this.MainWindow.Top.ToString("G");
            string currentWindowPositionLeft = this.MainWindow.Left.ToString("G");
            string currentWindowWidth = this.MainWindow.Width.ToString("G");
            string currentWindowHeight = this.MainWindow.Height.ToString("G");

            cfg.Put("top", currentWindowPositionTop);
            cfg.Put("left", currentWindowPositionLeft);
            cfg.Put("width", currentWindowWidth);
            cfg.Put("height", currentWindowHeight);
        }
    }
}
