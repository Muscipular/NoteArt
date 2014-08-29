using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteArt.View.Controls
{
    /// <summary>
    /// make control support drag
    /// </summary>
    public class DragableControl : ContentControl
    {
        static DragableControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableControl), new FrameworkPropertyMetadata(typeof(DragableControl)));
        }

        public static readonly DependencyProperty DragBarLocationProperty = DependencyProperty.Register(
            "DragBarLocation", typeof(DragBarLocation), typeof(DragableControl), new PropertyMetadata(default(DragBarLocation)));

        public DragBarLocation DragBarLocation
        {
            get { return (DragBarLocation)GetValue(DragBarLocationProperty); }
            set { SetValue(DragBarLocationProperty, value); }
        }

        public DragableControl()
        {
            
        }
    }

    public class DragBar : Control
    {
        public static readonly DependencyProperty DragBarBrushProperty = DependencyProperty.Register(
            "DragBarBrush", typeof(Brush), typeof(DragBar), new PropertyMetadata(Brushes.Black));

        public Brush DragBarBrush
        {
            get { return (Brush)GetValue(DragBarBrushProperty); }
            set { SetValue(DragBarBrushProperty, value); }
        }

        public static readonly DependencyProperty DragBarLocationProperty = DependencyProperty.Register(
            "DragBarLocation", typeof(DragBarLocation), typeof(DragBar), new PropertyMetadata(default(DragBarLocation)));

        public DragBarLocation DragBarLocation
        {
            get { return (DragBarLocation)GetValue(DragBarLocationProperty); }
            set { SetValue(DragBarLocationProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", typeof(double), typeof(DragBar), new PropertyMetadata(0.8));

        public double Thickness
        {
            get { return (double) GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        //        public static readonly DependencyProperty DragBarBrush2Property = DependencyProperty.Register(
        //            "DragBarBrush2", typeof(Brush), typeof(DragBar), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0x3f, 0xff, 0xff, 0xff))));
        //
        //        public Brush DragBarBrush2
        //        {
        //            get { return (Brush)GetValue(DragBarBrush2Property); }
        //            set { SetValue(DragBarBrush2Property, value); }
        //        }

        private static Pen drawPen;
        //        private static Pen drawPen2;

        protected override void OnRender(DrawingContext drawingContext)
        {
            var pen = drawPen ?? (drawPen = new Pen());
            var thickness = Thickness;
            pen.Thickness = thickness;
            pen.Brush = DragBarBrush;
            //            var pen2 = drawPen2 ?? (drawPen2 = new Pen());
            //            pen2.Thickness = 1;
            //            pen2.Brush = DragBarBrush;
            var height = this.RenderSize.Height;
            var width = this.RenderSize.Width;
            switch (DragBarLocation)
            {
                case DragBarLocation.Left:
                case DragBarLocation.Right:
                    {
                        var startX = 6;
                        var startY = (int)(height * 0.1);
                        for (int i = startY; i < height - startY - 2; i += 3)
                        {
                            drawingContext.DrawLine(pen, new Point(startX, i), new Point(startX + 5, i));
                            drawingContext.PushOpacity(0.12);
                            drawingContext.DrawLine(pen, new Point(startX + 1, i + thickness), new Point(startX + 5 + 1, i + thickness));
                            drawingContext.Pop();
                        }
                        break;
                    }
                case DragBarLocation.Top:
                case DragBarLocation.Bottom:
                    {
                        var startY = 6;
                        var startX = (int)(width * 0.1);
                        for (int i = startX; i < width - startX - 2; i += 3)
                        {
                            drawingContext.DrawLine(pen, new Point(i, startY), new Point(i, startY + 5));
                            drawingContext.PushOpacity(0.12);
                            drawingContext.DrawLine(pen, new Point(i + thickness, startY + 1), new Point(i + thickness, startY + 5 + 1));
                            drawingContext.Pop();
                        }
                        break;
                    }
            }
            base.OnRender(drawingContext);
        }
    }
    public enum DragBarLocation
    {
        Left, Top, Right, Bottom
    }
}