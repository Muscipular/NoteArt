using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NoteArt.View.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawEdgeRectangle()
        {

        }

        private int _LastTitleClick = -1;

        private void WindowTitleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (e.Timestamp - _LastTitleClick < 300)
                {
                    //double click!
                    switch (WindowState)
                    {
                        case System.Windows.WindowState.Normal:
                            WindowState = System.Windows.WindowState.Maximized;
                            break;
                        case System.Windows.WindowState.Maximized:
                            WindowState = System.Windows.WindowState.Normal;
                            break;
                    }
                    _LastTitleClick = -1;
                    return;
                }
                _LastTitleClick = e.Timestamp;
            }

            if (WindowState == System.Windows.WindowState.Normal)
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    //DrawEdgeRectangle();
                    DragMove();
                }
            }
            else
            {
                Mouse.AddMouseMoveHandler(this, this.MouseMoveHandler_ForMaximized);
                Mouse.AddMouseUpHandler(this, this.MouseMoveHandler_ForMaximized);
            }
        }

        private void MouseMoveHandler_ForMaximized(object sender, MouseEventArgs e)
        {
            Mouse.RemoveMouseMoveHandler(this, this.MouseMoveHandler_ForMaximized);
            Mouse.RemoveMouseUpHandler(this, this.MouseMoveHandler_ForMaximized);
            if (WindowState == System.Windows.WindowState.Maximized && e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePoint = Mouse.GetPosition(this);
                WindowState = System.Windows.WindowState.Normal;
                var p = this.PointToScreen(mousePoint);
                this.Left = mousePoint.X - Width / 2;
                this.Top = mousePoint.Y - 26;
                Debug.WriteLine("mousePoint: {0} {1}", mousePoint.X, mousePoint.Y);
                Debug.WriteLine("mousePoint2: {0} {1}", p.X, p.Y);
                DragMove();
            }
        }
    }
}
