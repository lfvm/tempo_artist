using System;
using System.Collections.Generic;
using TempoArtist.Objects;
using TempoArtist.Objects.HitObjects;
using TempoArtist.Beatmaps;
using TempoArtist.Managers;
using TempoArtist.Utils;
using UnityEngine;

namespace TempoArtist.Managers
{
    public class TaikoGameSetup : MonoBehaviour
    {
        // Reference to the HitObject prefab
        [SerializeField] private TaikoHitObject HitObject; 
        
        // Instance of this GameSetup object
        public static TaikoGameSetup instance;
        
        public Beatmap Beatmap { get; set; }

        public List<TaikoHitObject> objectInteractQueue;
        public List<TaikoHitObject> objectActivationQueue;

        public AudioSource MusicSource;
        
        [SerializeField] private List<TaikoHitObject> notes;
        
        // Reference to the GameManager instance
        private TaikoGameManager TaikoGameManager;

        private SongSelectManager SongSelectManager;

        private bool songReady;
        
        private const string defaultBeatmapPath = "Assets/Resources/Beatmaps/Akasha/Akasha.json";

        private void Awake()
        {
            instance = this;
        }
        
        private void Start()
        {
            objectActivationQueue = new List<TaikoHitObject>();
            objectInteractQueue = new List<TaikoHitObject>();
            
            SongSelectManager = SongSelectManager.Instance;
            TaikoGameManager = TaikoGameManager.instance;
            
            Beatmap = SongSelectManager.selectedBeatmap;
            SetBeatmapSong();

            if (songReady)
                InstantiateObjects();

            TaikoGameManager.useMusicTimeline = true;
        }

        private void SetBeatmapSong()
        {
            MusicSource.clip = Beatmap.MusicSource;
            songReady = true;
        }

        private void InstantiateObjects()
        {
            // loop through all notes in the beatmap file and assign the position and startTime to and HitObject object
            foreach (var BeatmapObject in Beatmap.hitObjects)
            {
                var x = 10;
                var y = 2.5f;
                var time = Int32.Parse(BeatmapObject.time);
                var hitsound = Int32.Parse(BeatmapObject.hitsound);
                
                var hitObject = Instantiate(HitObject, new Vector3(x, y,0), Quaternion.identity);
                
                hitObject.x = x;
                hitObject.y = y;
                hitObject.time = time;
                HitObject.hitsound = hitsound;

                hitObject.name = objectActivationQueue.Count + "-Hitcircle";
                hitObject.queueId = objectActivationQueue.Count;
                
                objectActivationQueue.Add(hitObject);
                notes.Add(hitObject);
                
                TaikoGameManager.SetGameReady();
            }
        }
    }
}