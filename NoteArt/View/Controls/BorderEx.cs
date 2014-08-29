using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteArt.View.Controls
{
    public class BorderEx : Border
    {
        public static readonly DependencyProperty LeftBorderBrushProperty = DependencyProperty.Register(
            "LeftBorderBrush", typeof(Brush), typeof(BorderEx), new PropertyMetadata(default(Brush)));

        public Brush LeftBorderBrush
        {
            get { return (Brush)GetValue(LeftBorderBrushProperty); }
            set { SetValue(LeftBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty RightBorderBrushProperty = DependencyProperty.Register(
            "RightBorderBrush", typeof(Brush), typeof(BorderEx), new PropertyMetadata(default(Brush)));

        public Brush RightBorderBrush
        {
            get { return (Brush)GetValue(RightBorderBrushProperty); }
            set { SetValue(RightBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty TopBorderBrushProperty = DependencyProperty.Register(
            "TopBorderBrush", typeof(Brush), typeof(BorderEx), new PropertyMetadata(default(Brush)));

        public Brush TopBorderBrush
        {
            get { return (Brush)GetValue(TopBorderBrushProperty); }
            set { SetValue(TopBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty BottomBorderBrushProperty = DependencyProperty.Register(
            "BottomBorderBrush", typeof(Brush), typeof(BorderEx), new PropertyMetadata(default(Brush)));

        public Brush BottomBorderBrush
        {
            get { return (Brush)GetValue(BottomBorderBrushProperty); }
            set { SetValue(BottomBorderBrushProperty, value); }
        }

        private static Pen LeftPenCache;
        private static Pen RightPenCache;
        private static Pen TopPenCache;
        private static Pen BottomPenCache;

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var thickness = BorderThickness;
            if (thickness.Left > 0.0)
            {
                var pen = LeftPenCache ?? (LeftPenCache = new Pen());
                pen.Brush = LeftBorderBrush;
                pen.Thickness = thickness.Left;
                var d = pen.Thickness * 0.5;
                dc.DrawLine(pen, new Point(d, 0), new Point(d, RenderSize.Height));
            }
            if (thickness.Top > 0.0)
            {
                var pen = TopPenCache ?? (TopPenCache = new Pen());
                pen.Brush = TopBorderBrush;
                pen.Thickness = thickness.Top;
                var d = pen.Thickness * 0.5;
                dc.DrawLine(pen, new Point(0, d), new Point(RenderSize.Width, d));
            }
            if (thickness.Right > 0.0)
            {
                var pen = RightPenCache ?? (RightPenCache = new Pen());
                pen.Brush = RightBorderBrush;
                pen.Thickness = thickness.Right;
                var d = pen.Thickness * 0.5;
                dc.DrawLine(pen, new Point(RenderSize.Width - d, 0), new Point(RenderSize.Width - d, RenderSize.Height));
            }
            if (thickness.Bottom > 0.0)
            {
                var pen = BottomPenCache ?? (BottomPenCache = new Pen());
                pen.Brush = BottomBorderBrush;
                pen.Thickness = thickness.Bottom;
                var d = pen.Thickness * 0.5;
                dc.DrawLine(pen, new Point(d, RenderSize.Height - d), new Point(RenderSize.Width, RenderSize.Height - d));
            }
        }
    }
}
