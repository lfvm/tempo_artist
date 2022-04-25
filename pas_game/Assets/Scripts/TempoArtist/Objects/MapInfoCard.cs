using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TempoArtist.Objects
{
    public class MapInfoCard : MonoBehaviour
    {
        public static MapInfoCard Instance;
        
        public BeatmapCard beatmapCard { get; set; }
        public GameObject mapInfoCardDifficulty { get; set; }
        public GameObject mapInfoCardNumNotes { get; set; }
        public GameObject MapInfoCardMode { get; set; }
        public string mapInfoCardDifficultyText { get; set; }
        public string mapInfoCardNumNotesText { get; set; }
        public string mapInfoCardModeText { get; set; }

        private string beatmapMode;

        private void Awake()
        {
            Instance = this;

            mapInfoCardDifficulty = transform.GetChild(0).gameObject;
            mapInfoCardNumNotes = transform.GetChild(1).gameObject;
            MapInfoCardMode = transform.GetChild(2).gameObject;

            mapInfoCardDifficultyText = mapInfoCardDifficulty.GetComponent<TMP_Text>().text;
            mapInfoCardNumNotesText = mapInfoCardNumNotes.GetComponent<TMP_Text>().text;
            mapInfoCardModeText = mapInfoCardNumNotes.GetComponent<TMP_Text>().text;
        }

        void Start()
        {
            var position = transform.position;
        
            mapInfoCardDifficultyText = "Map Difficulty";
            mapInfoCardNumNotesText = "Map Notes";
            mapInfoCardModeText = "Map Mode";

            position.x = 250;
            position.y = -65;
        }

        public void UpdateMapInfoText(BeatmapCard card)
        {
            beatmapMode = card.Beatmap.general.mode switch
            {
                "1" => "Taiko",
                "3" => "Mania",
                _ => card.Beatmap.general.mode.ToString()
            };

            mapInfoCardDifficulty.GetComponent<TMP_Text>().text = "Difficulty: " + card.Beatmap.difficulty.OverallDifficulty;
            mapInfoCardNumNotes.GetComponent<TMP_Text>().text = "Number of notes: " + card.Beatmap.hitObjects.Count;
            MapInfoCardMode.GetComponent<TMP_Text>().text = "Game mode: " + beatmapMode;
        }
    }
}
