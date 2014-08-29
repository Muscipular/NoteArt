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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteArt.View.Controls
{
    /// <summary>
    ///
    /// </summary>
    public class DragableGroup : ContentControl
    {
        public static readonly DependencyProperty GroupTagProperty = DependencyProperty.Register(
            "GroupTag", typeof(object), typeof(DragableGroup), new PropertyMetadata(default(object)));

        public object GroupTag
        {
            get { return (object)GetValue(GroupTagProperty); }
            set { SetValue(GroupTagProperty, value); }
        }

        public static readonly DependencyProperty DragableGroupLocationProperty = DependencyProperty.Register(
            "DragableGroupLocation", typeof(DragableGroupLocation), typeof(DragableGroup), new PropertyMetadata(default(DragableGroupLocation)));

        public DragableGroupLocation DragableGroupLocation
        {
            get { return (DragableGroupLocation)GetValue(DragableGroupLocationProperty); }
            set { SetValue(DragableGroupLocationProperty, value); }
        }

        public static readonly DependencyProperty CanMinsizeProperty = DependencyProperty.Register(
            "CanMinsize", typeof(bool), typeof(DragableGroup), new PropertyMetadata(default(bool)));

        public bool CanMinsize
        {
            get { return object.ReferenceEquals(True, GetValue(CanMinsizeProperty)); }
            set { SetValue(CanMinsizeProperty, value ? True : False); }
        }

        public static readonly DependencyProperty CanDragProperty = DependencyProperty.Register(
            "CanDrag", typeof(bool), typeof(DragableGroup), new PropertyMetadata(default(bool)));

        public bool CanDrag
        {
            get { return object.ReferenceEquals(True, GetValue(CanDragProperty)); }
            set { SetValue(CanDragProperty, value ? True : False); }
        }

        private static readonly object True = true;
        private static readonly object False = false;

        static DragableGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableGroup), new FrameworkPropertyMetadata(typeof(DragableGroup)));
        }
    }

    public enum DragableGroupLocation
    {
        Left, Top, Right, Bottom
    }
}
