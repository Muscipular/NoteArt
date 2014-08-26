using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace NoteArt.ViewModel
{
    public abstract class VMBase : ViewModelBase, IDisposable
    {
        public void Dispose()
        {
        }
    }
}
