using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using TempoArtist.Objects;
using TMPro;
using UnityEngine.UI;

namespace TempoArtist.Managers
{
    public class SongSelectManager : MonoBehaviour
    {
        private List<string> jsonBeatmapPaths;
        private List<Beatmap> beatmapList;

        private GameObject beatmapsPanel;
        private GameObject canvas;
        
        [SerializeField] private BeatmapCard beatmapCard;
        [SerializeField] private MapInfoCard mapInfoCard;

        private Beatmap selectedBeatmap;
        public BeatmapCard selectedBeatmapCard;

        private bool beatmapSelected;
        public bool beatmapCardSelected;

        public static SongSelectManager Instance;

        private void Awake()
        {
            Instance = this;
            mapInfoCard = MapInfoCard.Instance;
            jsonBeatmapPaths = new List<string>();
            beatmapList = new List<Beatmap>();
            
            beatmapsPanel = GameObject.Find("BeatmapsPanel");
            canvas = GameObject.Find("Canvas");
        }

        void Start()
        {
            const string beatmapFoldersPath = "./Assets/Beatmaps";
            CreateBeatmaps(beatmapFoldersPath);
            CreateBeatmapMapCards();
        }

        void Update()
        {
            if (beatmapCardSelected)
            {
               UpdateMapInfoCard(selectedBeatmapCard);
            }
        }

        private void UpdateMapInfoCard(BeatmapCard card)
        {
            mapInfoCard.UpdateMapInfoText(selectedBeatmapCard);
        }

        private void CreateBeatmaps(string path)
        {
            var beatmapFolders = Directory.GetDirectories(path);

            foreach (var beatmapFolderPath in beatmapFolders)
            {
                foreach (var jsonBeatmapPath in Directory.GetFiles(beatmapFolderPath, "*.json"))
                {
                    var beatmap = JsonParser.JsonToBeatmap(jsonBeatmapPath);
                    beatmapList.Add(beatmap);
                }
            }
        }

        private void CreateBeatmapMapCards()
        {
            foreach (var beatmap in beatmapList)
            {
                var card = Instantiate(beatmapCard, beatmapsPanel.transform, true);
                card.cardTitle.GetComponent<TMP_Text>().text = beatmap.metadata.Title;
                card.cardArtist.GetComponent<TMP_Text>().text = beatmap.metadata.Artist;
                card.Beatmap = beatmap;
            }
        }

        private void CallGameSetup(Beatmap beatmapToPlay)
        {
            
        }
        
        public void SetSelectedBeatmapCard(BeatmapCard card)
        {
            selectedBeatmapCard = card;
            beatmapCardSelected = true;
        }
    }
}
