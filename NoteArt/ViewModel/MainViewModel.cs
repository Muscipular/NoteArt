using System;
using System.IO;
using System.Collections.Generic;
using System.Composition;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace NoteArt.ViewModel
{
    [Export]
    public class MainViewModel : ViewModelBase
    {
        [ImportMany("MainTitle")]
        public IEnumerable<string> Titles { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            Close = new RelayCommand(() =>
            {
                MessageBox.Show(String.Join(",", Titles));
                //Send message to messenger, the eventhandler registered in MainWindow.xaml.cs with same type will be triggered
                Messenger.Default.Send(Convert.ToUInt32(5));
                Messenger.Default.Send(new DateTime());
            });
        }


        private string _Title;

        [Import("MainTitle")]
        [ImportMetadataConstraint("Language", "c#")]
        public string Title { get { return _Title; } set { Set(() => Title, ref _Title, value); } }

        public ICommand Close { get; set; }
    }

    public class TestExport
    {
        [Export("MainTitle")]
        [ExportMetadata("Language", "c#")]
        public string X { get; set; }

        public TestExport()
        {
            X = "123456788";
        }
    }
    public class TestExport2
    {
        [ExportMetadata("Language", "c++")]
        [Export("MainTitle")]
        public string X { get; set; }

        public TestExport2()
        {
            X = "987654320";
        }
    }
}