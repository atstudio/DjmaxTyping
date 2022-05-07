using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Typing
{
    /// <summary>
    /// ConfigWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfigWindow : Window
    {
        Configure cfg = new Configure();
        public delegate void SendMessageDlg(string msg);
        public event SendMessageDlg SendMsg;

        public ConfigWindow()
        {
            InitializeComponent();

            // INI 파일에 있는 내용으로 셋팅
            PlayFileTextBox.Text = (string) cfg.Get("audiopath");
        }
 
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void FindFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Filter = "미디어 파일 (*.mp3, *.wav, *.m4a)|*.mp3;*.wav;*.m4a|mp3 (*.mp3)|*.mp3|wav (*.wav)|*.wav|m4a (*.m4a)|*.m4a";

            if (dlgOpenFile.ShowDialog().ToString().Equals("OK"))
            {
                PlayFileTextBox.Text = dlgOpenFile.FileName;
            }
        }

        private void SaveCloseButton_Click(object sender, RoutedEventArgs e)
        {
            DoSave(PlayFileTextBox.Text);
            this.DialogResult = true;
        }

        private void DoSave(string fileName)
        {
            cfg.Put("audioPath", fileName);
            SendMsg(fileName);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DoSave(PlayFileTextBox.Text);
            System.Windows.Forms.MessageBox.Show("설정이 저장되었습니다!");
        }
    }
}
