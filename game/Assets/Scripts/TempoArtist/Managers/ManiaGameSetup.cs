using System;
using System.Collections.Generic;
using TempoArtist.Beatmaps;
using TempoArtist.Objects;
using TempoArtist.Objects.HitObjects;
using TempoArtist.Utils;
using UnityEngine;
using UnityEngineInternal;

namespace TempoArtist.Managers
{
    public class ManiaGameSetup : MonoBehaviour
    {
        // Reference to the HitObject prefab
        [SerializeField] private ManiaHitObject HitObject; 
        
        // Instance of this GameSetup object
        public static ManiaGameSetup instance;
        
        // Reference to the GameManager instance
        private ManiaGameManager GameManager;
        private SongSelectManager SongSelectManager;
        
        public Beatmap Beatmap { get; set; }
        
        public List<ManiaHitObject> notes;

        public AudioSource MusicSource;

        public List<ManiaHitObject> objectInteractQueue = new List<ManiaHitObject>();
        public List<ManiaHitObject> objectActivationQueue = new List<ManiaHitObject>();

        public bool AddOffset { get; set; }

        private const string defaultBeatmapPath = "Assets/Resources/Beatmaps/BeastBassBomb/BEAST BASS BOMB.json";
        
        public bool SongReady { get; set; }

        private void Awake()
        {
            instance = this;
        }
        
        private void Start()
        {
            GameManager = ManiaGameManager.instance;
            SongSelectManager = SongSelectManager.Instance;

            GameManager.useMusicTimeline = true;
            GameManager.ScrollSpeed = Settings.ManiaScrollSpeed;
            GameManager.NoteTimeOffset = Settings.Offset;

            Beatmap = SongSelectManager.selectedBeatmap;
            SetBeatmapSong();

            if (SongReady)
                InstantiateObjects();
        }

        private void SetBeatmapSong()
        {
            MusicSource.clip = Beatmap.MusicSource;
            SongReady = true;
        }

        private void InstantiateObjects()
        {
            // loop through all notes in the beatmap file and assign the position and startTime to and HitObject object
            foreach (var BeatmapObject in Beatmap.hitObjects)
            {
                var x = float.Parse(BeatmapObject.x);
                var y = 5.5f;
                var time = Int32.Parse(BeatmapObject.time);
            
                float newX = 0;
            
                // Assign new x values based on the old values from the original beatmap
                newX = x switch
                {
                    64 => -1.5f,
                    192 => -0.5f,
                    320 => 0.5f,
                    448 => 1.5f,
                    _ => newX
                };
            
                var hitObject = Instantiate(HitObject, new Vector3(newX, y,0), Quaternion.identity);
                
                hitObject.X = newX;
                hitObject.Y = y;
                hitObject.Time = time + (int)GameManager.NoteTimeOffset;

                hitObject.name = objectActivationQueue.Count + "-Hitcircle";
                hitObject.QueueID = objectActivationQueue.Count;
                
                objectActivationQueue.Add(hitObject);
                notes.Add(hitObject);
                
                GameManager.SetGameReady();
            }
        }
    }
}