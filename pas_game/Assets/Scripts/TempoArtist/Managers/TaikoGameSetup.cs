using System;
using System.Collections.Generic;
using TempoArtist.Objects;
using TempoArtist.Beatmaps;
using TempoArtist.Utils;
using UnityEngine;

namespace TempoArtist
{
    public class TaikoGameSetup : MonoBehaviour
    {
        // Reference to the HitObject prefab
        [SerializeField] private TaikoHitObject HitObject; 
        
        // Instance of this GameSetup object
        public static TaikoGameSetup instance;
        
        // Reference to the GameManager instance
        private TaikoGameManager TaikoGameManager;

        public Beatmap Beatmap { get; set; }
        
        public List<TaikoHitObject> notes;

        public AudioSource MusicSource;

        public List<TaikoHitObject> objectInteractQueue = new List<TaikoHitObject>();
        public List<TaikoHitObject> objectActivationQueue = new List<TaikoHitObject>();

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
            TaikoGameManager = TaikoGameManager.instance;
            
            // Path of the beatmap's json file
            Beatmap = JsonParser.JsonToBeatmap("Assets/Beatmaps/Akasha/Akasha.json");

            TaikoGameManager.useMusicTimeline = true;

            InstantiateObjects();
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