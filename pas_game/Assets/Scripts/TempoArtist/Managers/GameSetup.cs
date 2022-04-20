using System;
using System.Collections.Generic;
using TempoArtist.Objects;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using UnityEngine;

namespace TempoArtist
{
    public class GameSetup : MonoBehaviour
    {
        // Reference to the HitObject prefab
        [SerializeField] private HitObject HitObject; 
        
        // Instance of this GameSetup object
        public static GameSetup instance;
        
        // Reference to the GameManager instance
        private GameManager GameManager;

        public Beatmap Beatmap { get; set; }
        
        public List<HitObject> notes;

        public AudioSource MusicSource;
        public AudioSource HitSource;

        public List<HitObject> objectInteractQueue = new List<HitObject>();
        public List<HitObject> objectActivationQueue = new List<HitObject>();

        public static int AccuracyLaybackMs = 100;
        
        public bool AddOffset { get; set; }
        
        private bool ready { get; set; }
        
        private int InteractionID { get; set; } = -1;

        private void Awake()
        {
            instance = this;
        }
        
        private void Start()
        {
            GameManager = GameManager.instance;
            
            // Path of the beatmap's json file
            Beatmap = JsonParser.JsonToBeatmap("Assets/Beatmaps/blue zenith/Blue Zenith.json");
            
            //HitSource = GameObject.Find("HitSource").GetComponent<AudioSource>();
            // = GameObject.Find("MusicSource").GetComponent<AudioSource>();

            GameManager.useMusicTimeline = true;

            InstantiateObjects();
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
                hitObject.Time = time;

                hitObject.name = objectActivationQueue.Count + "-Hitcircle";
                hitObject.QueueID = objectActivationQueue.Count;
                
                objectActivationQueue.Add(hitObject);
                notes.Add(hitObject);
                
                GameManager.SetGameReady();
            }
        }
    }
}