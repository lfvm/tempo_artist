using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using TempoArtist.Objects;
using TMPro;

namespace TempoArtist.Managers
{
    public class SongSelectManager : MonoBehaviour
    {
        private List<string> jsonBeatmapPaths = new List<string>();
        private List<Beatmap> beatmapList = new List<Beatmap>();

        [SerializeField] private BeatmapCard beatmapCard;

        private GameObject beatmapsPanel;

        private void Awake()
        {
            beatmapsPanel = GameObject.Find("BeatmapsPanel");
        }

        void Start()
        {
            const string beatmapFoldersPath = "./Assets/Beatmaps";
            CreateBeatmaps(beatmapFoldersPath);
            CreateBeatmapMapCards();
        }

        void Update()
        {

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
            }
        }
    }
}
