using System;
using TempoArtist.Beatmaps;
using TempoArtist.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TempoArtist.Objects
{
    public class BeatmapCard : MonoBehaviour
    {
        public GameObject cardTitle { get; set; }
        public GameObject cardArtist { get; set; }
        public Beatmap Beatmap { get; set; }
        
        // public UnityAction onBeatmapCardClicked;

        public Button button;
    
        private SongSelectManager songSelectManager;
        
        private void Awake()
        {
            //button = transform.GetComponent<Button>();
            cardTitle = transform.GetChild(0).gameObject;
            cardArtist = transform.GetChild(1).gameObject;
        }

        private void Start()
        { 
            songSelectManager = SongSelectManager.Instance;
            button.onClick.AddListener(OnBeatmapCardClicked);
        }
    
        void OnBeatmapCardClicked()
        {
            songSelectManager.SetSelectedBeatmapCard(this);
            // songSelectManager.PlayAudioSample();
        }
    }
}
