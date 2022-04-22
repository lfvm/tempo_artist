using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using TempoArtist.Objects;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        public AudioSource musicPreview;
        public AudioClip musicPreviewClip;

        public Beatmap selectedBeatmap { get; set; }
        private BeatmapCard selectedBeatmapCard;

        private AudioSource beatmapSong;
        
        private bool beatmapCardSelected;

        private string selectedBeatmapName;

        private bool samplePlaying;
        
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
            beatmapSong = new AudioSource();
            UIManager = UIManager.Instance;
            GameSetup = GameSetup.instance;
            var beatmapFoldersPath = "./Assets/Resources/Beatmaps";
            CreateBeatmaps(beatmapFoldersPath);
            CreateBeatmapMapCards();
            Instantiate(musicPreview);
        }

        void Update()
        {
            if (beatmapCardSelected)
            {
                if (!samplePlaying)
                {
                    PlayAudioSample();
                }
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
                foreach (var filePath in Directory.GetFiles(beatmapFolderPath))
                {
                    var extension = Path.GetExtension(filePath);
                    var beatmap = new Beatmap();
                    if (extension == ".json")
                    {
                        beatmap = JsonParser.JsonToBeatmap(filePath);
                        beatmapList.Add(beatmap);
                    }
                    else if (extension == ".mp3")
                    {
                        var dividedPath = filePath.Split('/');
                        var auxPath =  "Beatmaps/" + dividedPath[dividedPath.Length -2] + "/" + dividedPath[dividedPath.Length - 1];
                        Debug.Log(auxPath);
                        beatmap.MusicSource = Resources.Load<AudioClip>(auxPath);
                        Debug.Log(beatmap.MusicSource);
                    }
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

        private void PlayAudioSample()
        {
            musicPreviewClip = selectedBeatmap.MusicSource;
            musicPreview.clip = musicPreviewClip;
            musicPreview.Play();
            samplePlaying = true;
        }
        
        IEnumerator GetAudioClip(string path)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                   musicPreviewClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }
    }
}
