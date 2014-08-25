using System;
using System.Linq;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
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

        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog of = new Microsoft.Win32.OpenFileDialog();
            of.DefaultExt = ".txt";
            of.Filter = "Text File|*.txt|Osu Format|*.osu";
            of.Multiselect = false;
            of.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Nullable<bool> result = of.ShowDialog();

            if (result == true)
            {
                OpenFileTextbox.Text = of.FileName;
                //OsuParser p = new OsuParser(of.FileName);
            }
        }

        private void WindowMain_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DrawEdgeRectangle();
                DragMove();
            }
        }

        private void DrawEdgeRectangle()
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int port = 28571;
                string host = "127.0.0.1";

                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(ipe);

                byte[] buffer = Encoding.ASCII.GetBytes("Hello Malody, I'm Editor");
                s.Send(buffer, buffer.Length, 0);

                s.Close();
            }
            catch (ArgumentNullException exc)
            {
            }
            catch (SocketException exc)
            {
            }

            //LocalisationManager inst = LocalisationManager.Instance;
        }

    }
}
