using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteArt.ViewModel
{
    public abstract class UIViewModel : VMBase
    {
        protected double _Width;
        protected double _Height;
        public double Width { get { return _Width; } set { Set(() => Width, ref _Width, value); } }
        public double Height { get { return _Height; } set { Set(() => Height, ref _Height, value); } }

    }
}
