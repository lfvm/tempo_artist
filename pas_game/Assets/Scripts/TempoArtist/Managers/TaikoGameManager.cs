using System.Diagnostics;
using TempoArtist.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TempoArtist.Managers
{
    public class TaikoGameManager : MonoBehaviour
    {
        [SerializeField] private double GameTimeMs;
        
        // Instance of this object
        public static TaikoGameManager instance;
        
        public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
        
        public bool gameFinished;
        public bool useMusicTimeline;
        public bool isGameReady;

        public int noteTimeOffset = 0;
        
        // Reference to the GameSetup object instance
        private TaikoGameSetup TaikoGameSetup;
        
        private const float scorePerOkNote = 50f;
        private const float scorePerGoodNote = 100f;
        private const float scorePerPerfectNote = 300f;
        private const float scorePerMiss = 0f;

        public int score;
        public int combo;
        public float health;
        public int scrollSpeed;
        public float OD;
        public float HPDrain;
        public int maxCombo;
        
        public int NextObjToHit = 0; 
        private int nextObjectID;
        private int NextObjToActivateID = 0;

        [SerializeField] private int okHits;
        [SerializeField] private int goodHits;
        [SerializeField] private int perfectHits;
        [SerializeField] private int missedHits;
        
        [SerializeField] private double accuracy;

        private string rank;
        
        private bool resultsCreated = false;

        public GameObject HitZone;
        
       // private ResultsManager resultsManager;
       private MapResult mapResults;
        
        public Text scoreText;
        public Text comboText;
        public Text timeText;
        public Text accuracyText;
        
        private Stopwatch Stopwatch { get; } = new Stopwatch();

        private void Awake()
        {
            HitZone = GameObject.Find("HitZone");

            instance = this;
        }

        private void Start()
        {
            TaikoGameSetup = TaikoGameSetup.instance;
        }
        
        public void SetGameReady()
        {
            Stopwatch.Start();
            isGameReady = true;
        }

        private void Update()
        {
            if (!isGameReady)
            {
                return;
            }

            if (isGameReady)
            {
                if (!TaikoGameSetup.MusicSource.isPlaying)
                    TaikoGameSetup.MusicSource.Play();
            }
            
            if (gameFinished)
            {
                callResultsWindow();
                TaikoGameSetup.MusicSource.Stop();
            }
            
            IterateObjectQueue();
            GetTimeInMs();
            
            health = handleHealth();
            rank = calculateRank(); 
            accuracy = CalculateAccuracy();
            HandleCombo(combo);
            
            timeText.text = GetTimeInMs().ToString();
            accuracyText.text = CalculateAccuracy().ToString();
        }

        private void callResultsWindow()
        {
            if (!resultsCreated)
            {
                MapResult.score = score;
                MapResult.maxCombo = maxCombo;
                MapResult.okHits = okHits;
                MapResult.goodHits = goodHits;
                MapResult.perfectHits = perfectHits;
                MapResult.missedHits = missedHits;
                MapResult.rank = rank;
                MapResult.accuracy = accuracy;
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
            accuracy = (scorePerPerfectNote * perfectHits + goodHits * scorePerGoodNote + okHits * scorePerOkNote + scorePerMiss * missedHits) /
                       ((perfectHits + goodHits + okHits + missedHits) * scorePerPerfectNote);
            return accuracy * 100;
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
                var n when n >= 40 => "D",
                var n when n >= 55 => "C",
                var n when n >= 70 => "B",
                var n when n >= 85 => "B",
                var n when n >= 90 => "A",
                var n when n >= 95 => "S",
                _ => "F"
            };
            return rank;
        }


        private void IterateObjectQueue()
        {
            if (GetTimeInMs() >= TaikoGameSetup.objectActivationQueue[NextObjToActivateID].time - Timing.ODTiming.GetODTimingForOkHit(OD))
            {
                (TaikoGameSetup.objectActivationQueue[NextObjToActivateID]).gameObject.SetActive(true);
                NextObjToActivateID++;
            }
            
            if (nextObjectID == TaikoGameSetup.objectActivationQueue.Count && nextObjectID == TaikoGameSetup.objectInteractQueue.Count)
                gameFinished = true;
        }
        
        // public void IterateInteractionQueue(int? thisId = null)
        // {
        //     if (thisId != null)
        //     {
        //         NextObjToHit = (int)thisId + 1;
        //         Debug.Log($"set nextHitObj to:{NextObjToHit}");
        //         return;
        //     }
        // }

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
    }
}