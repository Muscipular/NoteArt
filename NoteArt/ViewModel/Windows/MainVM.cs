using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteArt.ViewModel.Windows
{
    [Export]
    public class MainVM : UIViewModel
    {
        public MainVM()
        {
            this.Width = 1024;
            this.Height = 768;
        }
    }
}
