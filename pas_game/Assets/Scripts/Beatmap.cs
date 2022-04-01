using System;
using System.Collections.Generic;
using System.IO;

namespace BeatmapConverter
{
    public class Beatmap
    {
        public Metadata metadata { get; set; }
        public DifficultyInfo difficulty { get; set; }
        public List<HitObject> hitObjects { get; set; }
    }
    
    public class HitObject
    {
        public string x { get; set; }
        public string y { get; set; }
        public string time { get; set; }
    }

    public class Metadata
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Version { get; set; }
    }

    public class DifficultyInfo
    {
        public string HPDrainRate { get; set; }
        public string OverallDifficulty { get; set; }
        public string ApproachRate { get; set; }
    }
}