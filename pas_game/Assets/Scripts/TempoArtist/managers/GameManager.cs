﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TempoArtist;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace TempoArtist
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private double GameTimeMs;
        
        // Instance of this object
        public static GameManager instance;
        
        public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
        
        public bool gameFinished;
        public bool useMusicTimeline;
        public bool isGameReady;

        public int noteTimeOffset = 0;
        
        // Reference to the GameSetup object instance
        private GameSetup GameSetup;
        
        private int scorePerOkNote = 50;
        private int scorePerGoodNote = 100;
        private int scorePerPerfectNote = 300;
        private int scorePerMiss = 0;

        public int score;
        public int combo;
        public float health;
        public int scrollSpeed;
        public float OD;
        
        public int NextObjToHit = 0;
        [SerializeField] private int nextObjectID;
         private int NextObjToActivateID = 0;

        private int okHits;
        private int goodHits;
        private int perfectHits;
        private int missedHits;

        private string rank;

        [SerializeField] private float accuracy;
        
        private bool resultsCreated = false;

        public GameObject HitZone1;
        public GameObject HitZone2;
        public GameObject HitZone3;
        public GameObject HitZone4;

       // private ResultsManager resultsManager;
       private MapResult mapResults;
        
        public Text scoreText;
        public Text comboText;
        public Text msText;
        public Text accuracyText;
        
        private Stopwatch Stopwatch { get; } = new Stopwatch();

        private void Awake()
        {
            HitZone1 = GameObject.Find("HitZone1");
            HitZone2 = GameObject.Find("HitZone2");
            HitZone3 = GameObject.Find("HitZone3");
            HitZone4 = GameObject.Find("HitZone4");
            
            instance = this;
        }

        private void Start()
        {
            GameSetup = GameSetup.instance;
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
                if (!GameSetup.MusicSource.isPlaying)
                    GameSetup.MusicSource.Play();
                //handleHealth();
            }
            
            if (gameFinished)
            {
                callResultsWindow();
                GameSetup.MusicSource.Stop();
            }
            
            GameTimeMs = Stopwatch.ElapsedMilliseconds;
            IterateObjectQueue();
            GetTimeInMs();
            msText.text = GetTimeInMs().ToString();
            rank = calculateRank(); 
            //accuracy = CalculateAccuracy();
            //accuracyText.text = CalculateAccuracy().ToString();
        }

        private void callResultsWindow()
        {
            if (!resultsCreated)
            {
                MapResult.score = score;
                MapResult.maxCombo = combo;
                MapResult.okHits = okHits;
                MapResult.goodHits = goodHits;
                MapResult.perfectHits = perfectHits;
                MapResult.missedHits = missedHits;
                MapResult.totalNotes = GameSetup.instance.notes.Count;
                MapResult.rank = rank;
                MapResult.accuracy = accuracy;
                resultsCreated = true;
                SceneManager.LoadScene("Results");
            }
        }

        private float CalculateAccuracy()
        {
            accuracy = ((scorePerPerfectNote * perfectHits) + (goodHits * scorePerGoodNote) +
                        (okHits * scorePerOkNote) + (scorePerMiss * missedHits)) /
                       ((perfectHits + goodHits + okHits + missedHits) * scorePerPerfectNote);
            return accuracy;
        }

        private void handleHealth()
        {
            throw new NotImplementedException();
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
            if (GetTimeInMs() >= GameSetup.objectActivationQueue[NextObjToActivateID].time - Timing.ODTiming.GetODTimingForOkHit(OD))
            {
                (GameSetup.objectActivationQueue[NextObjToActivateID]).gameObject.SetActive(true);
                NextObjToActivateID++;

            }
            if (nextObjectID == GameSetup.objectActivationQueue.Count && nextObjectID == GameSetup.objectInteractQueue.Count)
                gameFinished = true;
        }
        
        public void IterateInteractionQueue(int? thisId = null)
        {
            if (thisId != null)
            {
                NextObjToHit = (int)thisId + 1;
                Debug.Log($"set nextHitObj to:{NextObjToHit}");
                return;
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
            Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += scorePerOkNote * combo;
            NoteHit();
            okHits++;
        }

        public void GoodHit()
        {
            Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            NoteHit();
            score += scorePerGoodNote * combo;
            goodHits++;
        }

        public void PerfectHit()
        {
            Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            NoteHit();
            score += scorePerPerfectNote * combo;
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