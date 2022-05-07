using System.Windows;
using System.Windows.Forms;

namespace Typing
{
    public partial class ConfigWindow : Window
    {
        private readonly Configure cfg = new Configure();
        public delegate void SendMessageDlg(string msg);
        public event SendMessageDlg SendMsg;

        public ConfigWindow()
        {
            InitializeComponent();

            // INI 파일에 있는 내용으로 셋팅
            PlayFileTextBox.Text = (string)cfg.Get("audioPath");
            BgImageTextBox.Text = (string)cfg.Get("imagePath");
            VolumeSlider.Value = double.TryParse((string)cfg.Get("volume"), out double volume) ? volume : 50.0;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FindAudioFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog
            {
                Filter = "오디오 파일 (*.mp3, *.wav)|*.mp3;*.wav|mp3 (*.mp3)|*.mp3|wav (*.wav)|*.wav"
            };

            if (dlgOpenFile.ShowDialog().ToString().Equals("OK"))
            {
                PlayFileTextBox.Text = dlgOpenFile.FileName;
            }
        }

        private void FindImageFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog
            {
                Filter = "이미지 파일 (*.gif, *.jpg, *.png)|*.gif;*.jpg;*.png|gif (*.gif)|*.gif|jpg (*.jpg)|*.jpg|png (*.png)|*.png"
            };

            if (dlgOpenFile.ShowDialog().ToString().Equals("OK"))
            {
                BgImageTextBox.Text = dlgOpenFile.FileName;
            }
        }

        private void SaveCloseButton_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
            DialogResult = true;
        }

        private void DoSave()
        {
            cfg.Put("audioPath", PlayFileTextBox.Text);
            cfg.Put("imagePath", BgImageTextBox.Text);
            cfg.Put("volume", VolumeSlider.Value.ToString("F0"));
            SendMsg(null);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
            System.Windows.Forms.MessageBox.Show("설정이 저장되었습니다.");
        }
    }
}
