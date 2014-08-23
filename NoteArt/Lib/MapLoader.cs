using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteArt
{
    sealed class MapLoader
    {
        private static MapLoader instance = new MapLoader();

        //This Constructer will be called after real constructer
        //Simply leave it empty to tell compilor the instance need not to be initialized before first call
        static MapLoader()
        {

        }

        public static MapLoader Instance
        {
            get
            {
                int c = 0;
                return instance;
            }
        }

        //This is the real constructor, init members here
        private MapLoader()
        {

        }
    }
}
