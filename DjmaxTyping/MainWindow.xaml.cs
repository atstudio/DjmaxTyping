using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Typing {
    public partial class MainWindow : Window {
        private MediaPlayer[] players;
        private int index;
        private Dictionary<int, bool> pressed = new Dictionary<int, bool>();
        private string defaultAudioPath;
        private string defaultImagePath;

        ConfigWindow cfgWnd;
        Configure cfg = new Configure();

        public MainWindow()
        {
            InitializeComponent();
            Hook.KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            Hook.KeyboardHook.KeyUp += KeyboardHook_KeyUp;
            Hook.KeyboardHook.HookStart();

            InitAssets(null);
        }

        ~MainWindow() {
            Hook.KeyboardHook.HookEnd();
        }

        private void InitAssets(string _)
        {
            // 지정된 오디오, 이미지 파일 불러오기
            defaultAudioPath = (string)cfg.Get("audioPath");
            defaultImagePath = (string)cfg.Get("imagePath");

            if (defaultAudioPath.Equals(""))
            {
                // ini 파일이 없는 경우 
                defaultAudioPath = $"{Path.Combine(Directory.GetCurrentDirectory(), "Sound.wav")}";
                cfg.Put("audioPath", defaultAudioPath);
            }
            if (defaultImagePath.Equals(""))
            {
                defaultImagePath = $"{Path.Combine(Directory.GetCurrentDirectory(), "djmax.gif")}";
                cfg.Put("imagePath", defaultImagePath);
            }

            InitPlayer();
            InitBackground();
        }

        private void InitPlayer()
        {
            players = new MediaPlayer[5];
            var audioPath = new Uri($"file:///{defaultAudioPath}");
            for (var i = 0; i < players.Length; i++)
            {
                players[i] = new MediaPlayer();
                players[i].Open(audioPath);
            }
        }
        private void InitBackground()
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri($"file:///{defaultImagePath}");
            image.EndInit();

            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(BgImage, image);
        }

        private bool KeyboardHook_KeyDown(int vkCode)
        {
            if (!pressed.ContainsKey(vkCode))
            {
                pressed[vkCode] = false;
            }
            if (!pressed[vkCode])
            {
                Play();
                pressed[vkCode] = true;
            }
            return true;
        }

        private bool KeyboardHook_KeyUp(int vkCode)
        {
            pressed[vkCode] = false;
            return true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) {
                DragMove();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                return;
            }
            else
            {
                Play();
            }
        }

        private void Play()
        {
            players[index].Stop();
            players[index].Play();
            index = (index + 1) % players.Length;
        }

        private void EXIT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CONFIG_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cfgWnd = new ConfigWindow();
            cfgWnd.SendMsg += new ConfigWindow.SendMessageDlg(InitAssets);
            cfgWnd.Owner = this;
            cfgWnd.ShowDialog();
        }
    }
}
