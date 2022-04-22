using UnityEngine;

namespace TempoArtist.Objects
{
    public class MapResult : MonoBehaviour
    {
        public static int score;
        public static int okHits;
        public static int goodHits;
        public static int perfectHits;
        public static int missedHits;
        public static int totalNotes;

        public static int maxCombo;

        public static string rank;

        public static double accuracy;

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
