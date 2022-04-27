using UnityEngine;

namespace TempoArtist.Objects
{
    public class MapResult
    {
        public int score;
        public int okHits;
        public int goodHits;
        public int perfectHits;
        public int missedHits;
        public int maxCombo;

        public int mapId;
        
        public double accuracy;

        public string rank;
        
        // public static void Init(int score, int okHits, int goodHits, int perfectHits, int missedHits, int maxCombo, string rank, float accuracy, int totalNotes)
        // {
        //     this.score = score;
        //     this.okHits = okHits;
        //     this.goodHits = goodHits;
        //     this.perfectHits = perfectHits;
        //     this.missedHits = missedHits;
        //     this.maxCombo = maxCombo;
        //     this.rank = rank;
        //     this.accuracy = accuracy;
        //     this.totalNotes = totalNotes;
        // }
    }
}
