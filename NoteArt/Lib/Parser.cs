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
    class OsuParser
    {
        public OsuParser()
        {

        }

        public OsuParser(string filename)
        {
            using (FileStream FS = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader SR = new StreamReader(FS as Stream))
                {
                    ParsedOsuMap map = new ParsedOsuMap();
                    MapLoader loader = MapLoader.Instance;

                    while (!SR.EndOfStream)
                    {
                        string line = SR.ReadLine();

                    }
                }
            }
        }
    }

    class ParsedOsuMap
    {
        public ParsedOsuMap()
        {
        }

        public string filename;    //related filename
        public string artist;     
        public string artistUnicode;
        public string title;
        public string titleUnicode;
        public string source;
        public string tags;
        public string creator;
        public string version;
        public string audio;

        public float HP;
        public float CS;
        public float OD;
        public float AR;
        public float SM;
        public float SR;

        public UInt32 MapID;
        public UInt32 SetID;

        class TimePoint
        {
        }

        class HitObject
        {
        }

        Dictionary<uint, TimePoint> TimingPoints;
        Dictionary<uint, HitObject> Notes;
    }

    enum OsuGameMode
    {
        ModeStandard = 0,
        ModeTaiko = 1,
        ModeCTB = 2,
        ModeMania = 3
    }

    enum OsuFileSection
    {
        General = 1,
        Editor = 2,
        Metadata = 3,
        Difficulty = 4,
        Events = 5,
        TimingPoints = 6,
        HitObjects = 7
    }
}
