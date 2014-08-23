using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace NoteArt.Lib
{
    sealed class LocalisationManager
    {
        private static LocalisationManager instance = new LocalisationManager();

        static LocalisationManager()
        {
        }

        public static LocalisationManager Instance
        {
            get
            {
                return instance;
            }
        }

        private static LocalisationManager Restart(string initFile = "en")
        {
            instance = new LocalisationManager(initFile);

            return Instance;
        }

        private LocalisationManager(string initFile = "en")
        {
            string filename = @"NoteArt.Resources." + initFile + @".txt";
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
            if (null == s)
            {
                MessageBox.Show("Target language is missing!", "NoteArt", MessageBoxButton.OK);
                filename = @"NoteArt.Resources.en.txt";     //try load default localisation file
                s = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
            }
            if (null != s)
            {
                using (StreamReader SR = new StreamReader(s))
                {
                    while (!SR.EndOfStream)
                    {
                        MessageBox.Show(SR.ReadLine(), "LocalisationManager", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Localisation Fail!", "NoteArt", MessageBoxButton.OK);
            }
        }
    }
}
