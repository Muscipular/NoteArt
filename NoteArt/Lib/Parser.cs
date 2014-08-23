using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NoteArt
{
    class Parser
    {
        public Parser()
        {
        }

        public Parser(string filename)
        {
            using (FileStream FS = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader SR = new StreamReader(FS as Stream))
                {
                    int ct = 0;
                    while (!SR.EndOfStream && ct++ < 5)
                    {
                        MessageBox.Show(SR.ReadLine(), "Parser", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}
