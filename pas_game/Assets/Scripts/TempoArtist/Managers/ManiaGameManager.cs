using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TempoArtist;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TempoArtist.Beatmaps;
using TempoArtist.Objects;

namespace TempoArtist.Managers
{
    public class ManiaGameManager : MonoBehaviour
    {
        [SerializeField] private double GameTimeMs;
        
        // Instance of this object
        public static ManiaGameManager instance;
        
        public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
        
        public bool gameFinished;
        public bool useMusicTimeline;
        public bool isGameReady;

        private float noteTimeOffset = 0;
        
        // Reference to the GameSetup object instance
        private ManiaGameSetup GameSetup;
        
        // private ResultsManager resultsManager;
        private MapResult MapResults;

        private const float scorePerOkNote = 50f;
        private const float scorePerGoodNote = 100f;
        private const float scorePerPerfectNote = 300f;
        private const float scorePerMiss = 0f;
        
        public float ScrollSpeed
        {
            get => scrollSpeed;
            set => scrollSpeed = value;
        }

        public float NoteTimeOffset
        {
            get => noteTimeOffset;
            set => noteTimeOffset = value;
        }
        
        [SerializeField] private int score;
        [SerializeField] private int combo;
        [SerializeField] private float health;
        [SerializeField] private float scrollSpeed;
        [SerializeField] public float OD;
        [SerializeField] private float HPDrain;
        [SerializeField] private int maxCombo;
        [SerializeField] private double accuracy;
        
        [SerializeField] private int NextObjToActivateID = 0;

        [SerializeField] private int okHits;
        [SerializeField] private int goodHits;
        [SerializeField] private int perfectHits;
        [SerializeField] private int missedHits;

        [SerializeField] private string rank;
        
        private bool resultsCreated = false;

        public GameObject HitZone1;
        public GameObject HitZone2;
        public GameObject HitZone3;
        public GameObject HitZone4;

        public Text scoreText;
        public Text comboText;
        public Text timeText;
        public Text accuracyText;

        public bool allNotesInActive;
        private bool GamePaused;

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        private void Awake()
        {
            instance = this;
            
            HitZone1 = GameObject.Find("HitZone1");
            HitZone2 = GameObject.Find("HitZone2");
            HitZone3 = GameObject.Find("HitZone3");
            HitZone4 = GameObject.Find("HitZone4");
        }

        private void Start()
        {
            GameSetup = ManiaGameSetup.instance;
        }
        
        public void SetGameReady()
        {
            Stopwatch.Start();
            isGameReady = true;
        }

        private void Update()
        {
            if (!isGameReady || GamePaused)
            {
                return;
            }

            if (isGameReady)
            {
                if (!GameSetup.MusicSource.isPlaying)
                    GameSetup.MusicSource.Play();
            }
            
            if (gameFinished)
            {
                callResultsWindow();
                GameSetup.MusicSource.Stop();
            }

            if (GameSetup.notes.Count != 0 && NextObjToActivateID < GameSetup.objectActivationQueue.Count)
            {
                IterateObjectQueue();
            }
            
            if (AllNotesInActive())
                gameFinished = true;
            
            GetTimeInMs();
            
            health = handleHealth();
            rank = calculateRank(); 
            accuracy = CalculateAccuracy();
            HandleCombo(combo);
            
            timeText.text = GetTimeInMs().ToString();
            accuracyText.text = CalculateAccuracy().ToString("0.00") + "%";

            allNotesInActive = AllNotesInActive();
        }

        private void callResultsWindow()
        {
            if (!resultsCreated)
            {
                MapResult mapResult = new MapResult();
                
                mapResult.score = score;
                mapResult.maxCombo = maxCombo;
                mapResult.okHits = okHits;
                mapResult.goodHits = goodHits;
                mapResult.perfectHits = perfectHits;
                mapResult.missedHits = missedHits;
                mapResult.rank = rank;
                mapResult.accuracy = accuracy;
                mapResult.mapId = Int32.Parse(GameSetup.Beatmap.metadata.BeatmapID);
                
                ResultsManager.instance.mapResult = mapResult;
                resultsCreated = true;
                SceneManager.LoadScene("Results");
            }
        }

        private void HandleCombo(int combo)
        {
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
        }

        private double CalculateAccuracy()
        {
            var acc = (scorePerPerfectNote * perfectHits + goodHits * scorePerGoodNote + okHits * scorePerOkNote + scorePerMiss * missedHits) /
                       ((perfectHits + goodHits + okHits + missedHits) * scorePerPerfectNote);
            return acc * 100;
        }

        private float handleHealth()
        {
            health -= HPDrain * Time.deltaTime;
            return health;
        }

        private string calculateRank()
        {
            rank = accuracy switch
            {
                var n when n >= 60 => "D",
                var n when n >= 70 => "C",
                var n when n >= 80 => "B",
                var n when n >= 90 => "A",
                var n when n >= 95 => "S",
                _ => "F"
            };
            return rank;
        }


        private void IterateObjectQueue()
        {
            if (GetTimeInMs() >= GameSetup.objectActivationQueue[NextObjToActivateID].StartTime - Timing.ODTiming.GetODTimingForOkHit(OD))
            {
                (GameSetup.objectActivationQueue[NextObjToActivateID]).gameObject.SetActive(true);
                NextObjToActivateID++;
            }
        }

        public double GetTimeInMs()
        {
            // if (useMusicTimeline)
            // {
            //     return GameSetup.MusicSource.time * 1000;
            // }
            //
            // if (Stopwatch.Elapsed.TotalMilliseconds - startOffsetMs >= 0)
            // {
            //     useMusicTimeline = true;
            //     Stopwatch.Stop();
            //     return GameSetup.MusicSource.time * 1000;
            // }
            return Stopwatch.Elapsed.TotalMilliseconds;
        }
        
        private void NoteHit()
        {
            combo++;
            scoreText.text = score.ToString();
            comboText.text = combo.ToString();
        }

        public void OkHit()
        {
            NoteHit();
            Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerOkNote * combo;
            okHits++;
        }

        public void GoodHit()
        {
            NoteHit();
            Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerGoodNote * combo;
            goodHits++;
        }

        public void PerfectHit()
        {
            NoteHit();
            Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerPerfectNote * combo;
            perfectHits++;
        }

        public void NoteMiss()
        {
            Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
            combo = 0;
            comboText.text = combo.ToString();
            missedHits++;
        }

        private bool AllNotesInActive()
        {
            if (GameSetup.notes.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}