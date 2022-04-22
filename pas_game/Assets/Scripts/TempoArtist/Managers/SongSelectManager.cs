using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using TempoArtist.Objects;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace TempoArtist.Managers
{
    public class SongSelectManager : MonoBehaviour
    {
        public static SongSelectManager Instance;
        
        private GameSetup GameSetup;
        private UIManager UIManager;
        
        private List<string> jsonBeatmapPaths;
        private List<Beatmap> beatmapList;

        private Button playButton;

        private GameObject beatmapsPanel;
        private GameObject canvas;
        
        [SerializeField] private BeatmapCard beatmapCard;
        [SerializeField] private MapInfoCard mapInfoCard;

        private Beatmap tempBeatmap;
        public Beatmap selectedBeatmap { get; set; }
        
        private BeatmapCard selectedBeatmapCard;
        
        public AudioSource beatmapSong;

        private bool beatmapCardSelected;

        private bool samplePlaying = false;

        public string selectedBeatmapName;

        private void Awake()
        {
            Instance = this;

            jsonBeatmapPaths = new List<string>();
            beatmapList = new List<Beatmap>();
            
            beatmapsPanel = GameObject.Find("BeatmapsPanel");
            canvas = GameObject.Find("Canvas");
        }

        void Start()
        {
            mapInfoCard = MapInfoCard.Instance;
            UIManager = UIManager.Instance;
            GameSetup = GameSetup.instance;
            
            var beatmapFoldersPath = "./Assets/Resources/Beatmaps";
            
            CreateBeatmaps(beatmapFoldersPath);
            CreateBeatmapMapCards();

            PrintBeatmapList();

            //beatmapSong.clip = beatmapList[0].MusicSource;
        }

        void Update()
        {
            if (beatmapCardSelected)
            {
                UpdateMapInfoCard(selectedBeatmapCard);
            }
        }

        private void PrintBeatmapList()
        {
            foreach (var beatmap in beatmapList)
            {
                Debug.Log(beatmap.MusicSource);
            }
        }

        private void UpdateMapInfoCard(BeatmapCard card)
        {
            mapInfoCard.UpdateMapInfoText(selectedBeatmapCard);
        }

        private void CreateBeatmaps(string path)
        {
            var beatmapFolders = Directory.GetDirectories(path);

            string[] beatmapJsonPaths;
            string[] beatmapSongPaths;
            
            foreach (var beatmapFolderPath in beatmapFolders)
            {
                beatmapJsonPaths = Directory.GetFiles(beatmapFolderPath, "*.json");
                beatmapSongPaths = Directory.GetFiles(beatmapFolderPath, "*.mp3");
                
                Beatmap beatmap = JsonParser.JsonToBeatmap(beatmapJsonPaths[0]);

                var beatmapSongPath = beatmapSongPaths[0];
                var dividedPath = beatmapSongPaths[0].Split('/');
                var auxPath =  dividedPath[dividedPath.Length - 1];
                auxPath = GetFullPathWithoutExtension(auxPath);
                beatmap.MusicSource = Resources.Load<AudioClip>(auxPath);
                beatmapList.Add(beatmap);
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
        
        public void SetSelectedBeatmapCard(BeatmapCard card)
        {
            selectedBeatmapCard = card;
            beatmapCardSelected = true;
            selectedBeatmap = card.Beatmap;
            selectedBeatmapName = card.Beatmap.metadata.Title;
        }

        public void playSelectedBeatmap()
        {
            if (beatmapCardSelected)
            {
                SceneManager.LoadScene("Game");
            }
        }

        public void PlayAudioSample()
        {
            beatmapSong.clip = selectedBeatmap.MusicSource;
            beatmapSong.Play();
            samplePlaying = true;
        }

        private String GetFullPathWithoutExtension(String path)
        {
            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        }
    }
}
