using NoteArt.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace NoteArt.View.Controls
{
    public class NAWindow : WindowEx
    {
        protected int customBorderThickness = 0;
        public NAWindow()
        {
            WindowStyle = System.Windows.WindowStyle.None;
            AllowsTransparency = true;
            //Effect = new DropShadowEffect()
            //{
            //    Color = System.Windows.Media.Colors.Black,
            //    Direction = 315,
            //    RenderingBias = RenderingBias.Performance,
            //    ShadowDepth = 1,
            //    BlurRadius = 3
            //};
            //BorderThickness = new Thickness(customBorderThickness);
            ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
        }

        //private Thickness? _oldThickness;
        //protected override void OnStateChanged(EventArgs e)
        //{
        //    if (WindowState == System.Windows.WindowState.Maximized)
        //    {
        //        _oldThickness = BorderThickness;
        //        BorderThickness = new Thickness(0);
        //    }
        //    if (WindowState == System.Windows.WindowState.Normal)
        //    {
        //        BorderThickness = _oldThickness ?? new Thickness(customBorderThickness);
        //    }
        //    //Debug.WriteLine("WindowState changed: {0}", WindowState);
        //    base.OnStateChanged(e);
        //}

        protected override void OnSourceInitialized(EventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source == null)
                // Should never be null  
                throw new Exception("Cannot get HwndSource instance.");

            source.AddHook(new HwndSourceHook(this.WndProc));
            base.OnSourceInitialized(e);
        }


        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WinAPIs.WM_GETMINMAXINFO: // WM_GETMINMAXINFO message  
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;

                //case WinAPIs.WM_NCHITTEST: // WM_NCHITTEST message  
                //    return WmNCHitTest(lParam, ref handled);
            }

            return IntPtr.Zero;
        }


        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            // MINMAXINFO structure  
            WinAPIs.MINMAXINFO mmi = (WinAPIs.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(WinAPIs.MINMAXINFO));

            // Get handle for nearest monitor to this window  
            WindowInteropHelper wih = new WindowInteropHelper(this);
            IntPtr hMonitor = WinAPIs.MonitorFromWindow(wih.Handle, WinAPIs.MONITOR_DEFAULTTONEAREST);

            // Get monitor info  
            WinAPIs.MONITORINFOEX monitorInfo = new WinAPIs.MONITORINFOEX();
            monitorInfo.cbSize = Marshal.SizeOf(monitorInfo);
            WinAPIs.GetMonitorInfo(new HandleRef(this, hMonitor), monitorInfo);

            // Get HwndSource  
            HwndSource source = HwndSource.FromHwnd(wih.Handle);
            if (source == null)
                // Should never be null  
                throw new Exception("Cannot get HwndSource instance.");
            if (source.CompositionTarget == null)
                // Should never be null  
                throw new Exception("Cannot get HwndTarget instance.");

            // Get transformation matrix  
            Matrix matrix = source.CompositionTarget.TransformFromDevice;

            // Convert working area  
            WinAPIs.RECT workingArea = monitorInfo.rcWork;
            var p1 = new Point(workingArea.Right - workingArea.Left, workingArea.Bottom - workingArea.Top);
            Point dpiIndependentSize =
                matrix.Transform(p1);

            //ebug.WriteLine("Working Area: {0} {1} {2} {3}", workingArea.Top, workingArea.Right, workingArea.Bottom, workingArea.Left);

            // Convert minimum size  
            Point dpiIndenpendentTrackingSize = matrix.Transform(new Point(
                this.MinWidth,
                this.MinHeight
                ));

            //Debug.WriteLine("p1: {0} {1}", p1.X, p1.Y);
            //Debug.WriteLine("dpiIndependentSize: {0} {1}", dpiIndependentSize.X, dpiIndependentSize.Y);
            //Debug.WriteLine("dpiIndenpendentTrackingSize: {0} {1}", dpiIndenpendentTrackingSize.X, dpiIndenpendentTrackingSize.Y);


            // Set the maximized size of the window  
            mmi.ptMaxSize.x = (int)p1.X;
            mmi.ptMaxSize.y = (int)p1.Y;

            // Set the position of the maximized window  
            mmi.ptMaxPosition.x = 0;
            mmi.ptMaxPosition.y = 0;

            // Set the minimum tracking size  
            mmi.ptMinTrackSize.x = (int)dpiIndenpendentTrackingSize.X;
            mmi.ptMinTrackSize.y = (int)dpiIndenpendentTrackingSize.Y;

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        /// <summary>  
        /// Corner width used in HitTest  
        /// </summary>  
        //private readonly int cornerWidth = 5;

        //private Point mousePoint;

        // private IntPtr WmNCHitTest(IntPtr lParam, ref bool handled)
        // {
        //     // Update cursor point  
        //     // The low-order word specifies the x-coordinate of the cursor.  
        //     // #define GET_X_LPARAM(lp) ((int)(short)LOWORD(lp))  
        //     this.mousePoint.X = (int)(short)(lParam.ToInt32() & 0xFFFF);
        //     // The high-order word specifies the y-coordinate of the cursor.  
        //     // #define GET_Y_LPARAM(lp) ((int)(short)HIWORD(lp))  
        //     this.mousePoint.Y = (int)(short)(lParam.ToInt32() >> 16);
        //
        //     // Do hit test  
        //     handled = true;
        //     if (Math.Abs(this.mousePoint.Y - this.Top) <= this.cornerWidth
        //         && Math.Abs(this.mousePoint.X - this.Left) <= this.cornerWidth)
        //     { // Top-Left  
        //         return new IntPtr((int)WinAPIs.HitTest.HTTOPLEFT);
        //     }
        //     else if (Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.cornerWidth
        //         && Math.Abs(this.mousePoint.X - this.Left) <= this.cornerWidth)
        //    { // Bottom-Left  
        //        return new IntPtr((int)WinAPIs.HitTest.HTBOTTOMLEFT);
        //    }
        //    else if (Math.Abs(this.mousePoint.Y - this.Top) <= this.cornerWidth
        //        && Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.cornerWidth)
        //    { // Top-Right  
        //        return new IntPtr((int)WinAPIs.HitTest.HTTOPRIGHT);
        //    }
        //    else if (Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.cornerWidth
        //        && Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.cornerWidth)
        //    { // Bottom-Right  
        //        return new IntPtr((int)WinAPIs.HitTest.HTBOTTOMRIGHT);
        //    }
        //    else if (Math.Abs(this.mousePoint.X - this.Left) <= this.customBorderThickness)
        //    { // Left  
        //        return new IntPtr((int)WinAPIs.HitTest.HTLEFT);
        //    }
        //    else if (Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.customBorderThickness)
        //    { // Right  
        //        return new IntPtr((int)WinAPIs.HitTest.HTRIGHT);
        //    }
        //    else if (Math.Abs(this.mousePoint.Y - this.Top) <= this.customBorderThickness)
        //    { // Top  
        //        return new IntPtr((int)WinAPIs.HitTest.HTTOP);
        //    }
        //    else if (Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.customBorderThickness)
        //    { // Bottom  
        //        return new IntPtr((int)WinAPIs.HitTest.HTBOTTOM);
        //    }
        //    else
        //    {
        //        handled = false;
        //        return IntPtr.Zero;
        //    }
        //}
    }
}
