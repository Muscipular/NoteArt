using System;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using NoteArt.Lib;

namespace NoteArt.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //接收消息
            Messenger.Default.Register<DateTime>(this, (d) =>
            {
                MessageBox.Show(this, d.ToString() + "_" + ViewModelLocator.Locator.GetExports<string>("MainTitle").Count());
                this.Close();
            });
        }
    }
}
