﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Typing
{
    internal enum VKeys
    {
        VK_LBUTTON = 0x01,  //Left mouse button
        VK_RBUTTON = 0x02,  //Right mouse button
        VK_CANCEL = 0x03,  //Control-break processing
        VK_MBUTTON = 0x04,  //Middle mouse button (three-button mouse)
        VK_BACK = 0x08,  //BACKSPACE key
        VK_TAB = 0x09,  //TAB key
        VK_CLEAR = 0x0C,  //CLEAR key
        VK_RETURN = 0x0D,  //ENTER key
        VK_SHIFT = 0x10,  //SHIFT key
        VK_CONTROL = 0x11,  //CTRL key
        VK_ALT = 0x12,  //ALT key
        VK_PAUSE = 0x13,  //PAUSE key
        VK_CAPITAL = 0x14,  //CAPS LOCK key
        VK_HANGUL = 0x15,
        VK_ESCAPE = 0x1B,  //ESC key
        VK_SPACE = 0x20,  //SPACEBAR
        VK_PAGE_UP = 0x21,  //PAGE UP key
        VK_PAGE_DOWN = 0x22,  //PAGE DOWN key
        VK_END = 0x23,  //END key
        VK_HOME = 0x24,  //HOME key
        VK_LEFT = 0x25,  //LEFT ARROW key
        VK_UP = 0x26,  //UP ARROW key
        VK_RIGHT = 0x27,  //RIGHT ARROW key
        VK_DOWN = 0x28,  //DOWN ARROW key
        VK_SELECT = 0x29,  //SELECT key
        VK_PRINT = 0x2A,  //PRINT key
        VK_EXECUTE = 0x2B,  //EXECUTE key
        VK_SNAPSHOT = 0x2C,  //PRINT SCREEN key
        VK_INSERT = 0x2D,  //INS key
        VK_DELETE = 0x2E,  //DEL key
        VK_HELP = 0x2F,  //HELP key
        VK_0 = 0x30,  //0 key
        VK_1 = 0x31,  //1 key
        VK_2 = 0x32,  //2 key
        VK_3 = 0x33,  //3 key
        VK_4 = 0x34,  //4 key
        VK_5 = 0x35,  //5 key
        VK_6 = 0x36,  //6 key
        VK_7 = 0x37,  //7 key
        VK_8 = 0x38,  //8 key
        VK_9 = 0x39,  //9 key
        VK_A = 0x41,  //A key
        VK_B = 0x42,  //B key
        VK_C = 0x43,  //C key
        VK_D = 0x44,  //D key
        VK_E = 0x45,  //E key
        VK_F = 0x46,  //F key
        VK_G = 0x47,  //G key
        VK_H = 0x48,  //H key
        VK_I = 0x49,  //I key
        VK_J = 0x4A,  //J key
        VK_K = 0x4B,  //K key
        VK_L = 0x4C,  //L key
        VK_M = 0x4D,  //M key
        VK_N = 0x4E,  //N key
        VK_O = 0x4F,  //O key
        VK_P = 0x50,  //P key
        VK_Q = 0x51,  //Q key
        VK_R = 0x52,  //R key
        VK_S = 0x53,  //S key
        VK_T = 0x54,  //T key
        VK_U = 0x55,  //U key
        VK_V = 0x56,  //V key
        VK_W = 0x57,  //W key
        VK_X = 0x58,  //X key
        VK_Y = 0x59,  //Y key
        VK_Z = 0x5A,  //Z key
        VK_NUMPAD0 = 0x60,  //Numeric keypad 0 key
        VK_NUMPAD1 = 0x61,  //Numeric keypad 1 key
        VK_NUMPAD2 = 0x62,  //Numeric keypad 2 key
        VK_NUMPAD3 = 0x63,  //Numeric keypad 3 key
        VK_NUMPAD4 = 0x64,  //Numeric keypad 4 key
        VK_NUMPAD5 = 0x65,  //Numeric keypad 5 key
        VK_NUMPAD6 = 0x66,  //Numeric keypad 6 key
        VK_NUMPAD7 = 0x67,  //Numeric keypad 7 key
        VK_NUMPAD8 = 0x68,  //Numeric keypad 8 key
        VK_NUMPAD9 = 0x69,  //Numeric keypad 9 key
        VK_SEPARATOR = 0x6C,  //Separator key
        VK_SUBTRACT = 0x6D,  //Subtract key
        VK_DECIMAL = 0x6E,  //Decimal key
        VK_DIVIDE = 0x6F,  //Divide key
        VK_F1 = 0x70,  //F1 key
        VK_F2 = 0x71,  //F2 key
        VK_F3 = 0x72,  //F3 key
        VK_F4 = 0x73,  //F4 key
        VK_F5 = 0x74,  //F5 key
        VK_F6 = 0x75,  //F6 key
        VK_F7 = 0x76,  //F7 key
        VK_F8 = 0x77,  //F8 key
        VK_F9 = 0x78,  //F9 key
        VK_F10 = 0x79,  //F10 key
        VK_F11 = 0x7A,  //F11 key
        VK_F12 = 0x7B,  //F12 key
        VK_SCROLL = 0x91,  //SCROLL LOCK key
        VK_LSHIFT = 0xA0,  //Left SHIFT key
        VK_RSHIFT = 0xA1,  //Right SHIFT key
        VK_LCONTROL = 0xA2,  //Left CONTROL key
        VK_RCONTROL = 0xA3,  //Right CONTROL key
        VK_LALT = 0xA4,   //Left ALT key
        VK_RALT = 0xA5,  //Right ALT key
        VK_PLAY = 0xFA,  //Play key
        VK_ZOOM = 0xFB, //Zoom key
    }

    public partial class MainWindow : Window
    {
        private readonly MediaPlayer[] players = new MediaPlayer[5];
        private int playersIndex;
        private readonly Dictionary<int, bool> isPressed = new Dictionary<int, bool>();
        private readonly Configure cfg = new Configure();
        private ConfigWindow cfgWnd;
        private string audioPath;
        private string imagePath;
        private double volume;
        private bool isMute = false;

        public MainWindow()
        {
            InitializeComponent();
            Hook.KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            Hook.KeyboardHook.KeyUp += KeyboardHook_KeyUp;
            Hook.KeyboardHook.HookStart();

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new MediaPlayer();
            }

            InitSettings(null);

            foreach (VKeys vkCode in Enum.GetValues(typeof(VKeys)))
            {
                isPressed[(int)vkCode] = false;
            }
        }

        ~MainWindow()
        {
            Hook.KeyboardHook.HookEnd();
        }

        private void InitSettings(string _)
        {
            // 지정된 설정값 불러오기
            audioPath = (string)cfg.Get("audioPath");
            if (audioPath.Equals(""))
            {
                // ini 파일이 없는 경우 
                audioPath = $"{Path.Combine(Directory.GetCurrentDirectory(), "Sound.wav")}";
                cfg.Put("audioPath", audioPath);
            }

            imagePath = (string)cfg.Get("imagePath");
            if (imagePath.Equals(""))
            {
                imagePath = $"{Path.Combine(Directory.GetCurrentDirectory(), "djmax.gif")}";
                cfg.Put("imagePath", imagePath);
            }

            if (!double.TryParse((string)cfg.Get("volume"), out volume))
            {
                volume = 50.0;
                cfg.Put("volume", volume.ToString("F0"));
            }

            InitPlayer();
            InitBackground();
        }

        private void InitPlayer()
        {
            Uri audioPathUri = new Uri($"file:///{audioPath}");

            foreach (MediaPlayer player in players)
            {
                player.Close();
                player.Open(audioPathUri);
                player.Volume = volume / 100;
            }
        }
        private void InitBackground()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri($"file:///{imagePath}");
            image.EndInit();

            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(BgImage, image);
        }

        private bool KeyboardHook_KeyDown(int vkCode)
        {
            if (!isPressed.ContainsKey(vkCode))
            {
                isPressed[vkCode] = false;
            }

            if (!isPressed[vkCode])
            {
                Play();
                isPressed[vkCode] = true;
            }

            if ((isPressed[(int)VKeys.VK_LCONTROL] || isPressed[(int)VKeys.VK_LCONTROL]) && vkCode == (int)VKeys.VK_M)
            {
                ToggleMute();
            }

            return true;
        }

        private bool KeyboardHook_KeyUp(int vkCode)
        {
            isPressed[vkCode] = false;
            return true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
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
            players[playersIndex].Stop();
            players[playersIndex].Play();
            playersIndex = (playersIndex + 1) % players.Length;
        }

        private void ToggleMute()
        {
            isMute = !isMute;

            foreach (MediaPlayer player in players)
            {
                player.Volume = isMute ? 0.0 : volume / 100;
            }
        }

        private void EXIT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CONFIG_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cfgWnd = new ConfigWindow();
            cfgWnd.SendMsg += new ConfigWindow.SendMessageDlg(InitSettings);
            cfgWnd.Owner = this;
            cfgWnd.ShowDialog();
        }
    }
}
