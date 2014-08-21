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
            //receive message,
            Messenger.Default.Register<DateTime>(this, (d) =>
            {
                MessageBox.Show(this, d.ToString() + "_" + ViewModelLocator.Locator.GetExports<string>("MainTitle").Count());
                //Close the main window (exit programme at this point)
                this.Close();
            });

            Messenger.Default.Register<UInt32>(this, (v) =>
                {
                    MessageBox.Show(this, v.ToString());

                });
        }
    }
}
