using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TempoArtist.Objects
{
    public class MapInfoCard : MonoBehaviour
    {
        public BeatmapCard beatmapCard { get; set; }
        public GameObject mapInfoCardDifficulty { get; set; }
        public GameObject mapInfoCardNumNotes { get; set; }
        public string mapInfoCardDifficultyText { get; set; }
        public string mapInfoCardNumNotesText { get; set; }

        public static MapInfoCard Instance;

        private void Awake()
        {
            Instance = this;
            Button button = GetComponent<Button>();
        
            mapInfoCardDifficulty = transform.GetChild(0).gameObject;
            mapInfoCardNumNotes = transform.GetChild(1).gameObject;

            mapInfoCardDifficultyText = mapInfoCardDifficulty.GetComponent<TMP_Text>().text;
            mapInfoCardNumNotesText = mapInfoCardNumNotes.GetComponent<TMP_Text>().text;
        }

        void Start()
        {
            var position = transform.position;
        
            mapInfoCardDifficultyText = "Map Difficulty";
            mapInfoCardNumNotesText = "Map Notes";

            position.x = 250;
            position.y = -65;
        }

        public void UpdateMapInfoText(BeatmapCard card)
        {
            mapInfoCardDifficulty.GetComponent<TMP_Text>().text = "Difficulty: " + card.Beatmap.difficulty.OverallDifficulty;
            mapInfoCardNumNotes.GetComponent<TMP_Text>().text = "Number of notes: " +card.Beatmap.hitObjects.Count.ToString();
        }
    }
}
