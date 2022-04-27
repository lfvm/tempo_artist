using System.Collections.Generic;
using UnityEngine;

namespace TempoArtist.Beatmaps
{
    public class Beatmap
    {
        public Metadata metadata { get; set; }
        public General general { get; set; }
        public DifficultyInfo difficulty { get; set; }
        public List<BeatmapHitObject> hitObjects { get; set; }
        public AudioClip MusicSource { get; set; }
    }
    
    public class BeatmapHitObject
    {
        public string x { get; set; }
        public string y { get; set; }
        public string time { get; set; }
        public string hitsound { get; set; }
    }

    public class Metadata
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string BeatmapID { get; set; }
    }

    public class DifficultyInfo
    {
        public string HPDrainRate { get; set; }
        public string OverallDifficulty { get; set; }
        public string ApproachRate { get; set; }
    }

    public class General
    {
        public string AudioFilename { get; set; }
        public string mode { get; set; }
    }
}