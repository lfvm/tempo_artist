﻿using System;
using System.Collections;
using UnityEngine;

namespace TempoArtist.Objects
{
    public class HitObject : MonoBehaviour
    {
        public int Time
        { 
            get => time;
            set => time = Time;
        }
        
        public float X
        {
            get => x;
            set => x = Y;
        }
        
        public float Y
        {
            get => Y;
            set => y = Y;
        }
        
        public int QueueID
        {
            get => queueId;
            set => QueueID = queueId;
        }
        
        
        // Reference to the GameManager and GameSetup instances.
        private GameManager GameManager;
        private GameSetup GameSetup;

        // x and Y position of the note
        private float x;
        private float y;
        // when the note has to be hit
        private int time;
        // When the note should become active
        private float startTime;
        private float speed;
        private int queueId;

        private float scrollSpeed;
        private float ODTimingOkHit;
        private float ODTimingGoodHit;
        private float ODTimingPerfectHit;

        private bool canBeHit;
        private bool hit;

        private KeyCode keyToPress;

        private float PerfectInteractionTimeInMs;
        private float InteractionBoundsStartTimeInMs;
        private float InteractionBoundsEndTimeInMs;

        private void Awake()
        {
            GameManager = GameManager.instance;
            GameSetup = GameSetup.instance;
            
            transform.GetComponent<Rigidbody2D>().simulated = false;
            transform.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.SetActive(false);
        }
        
        void Start()
        {
            ODTimingOkHit = Timing.ODTiming.GetODTimingForOkHit(GameManager.OD);
            ODTimingGoodHit = Timing.ODTiming.GetODTimingForGoodHit(GameManager.OD);
            ODTimingPerfectHit = Timing.ODTiming.GetODTimingForPerfectHit(GameManager.OD);
            
            SetKeyToPress();

            PerfectInteractionTimeInMs = time + GameManager.noteTimeOffset;
            InteractionBoundsStartTimeInMs = PerfectInteractionTimeInMs - ODTimingOkHit;
            InteractionBoundsEndTimeInMs = PerfectInteractionTimeInMs + ODTimingOkHit;

            scrollSpeed = GameManager.scrollSpeed;
            startTime = time - scrollSpeed;
        }

        private void Update()
        {
            canBeHit = IsInInteractionBound(GameManager.GetTimeInMs());
            if (GameManager.GetTimeInMs() >= startTime)
            {
                StartCoroutine(HitObjectMove());
            }

            if (IsInInteractionBound(GameManager.GetTimeInMs()))
            {
                transform.GetComponent<Rigidbody2D>().simulated = true;
                transform.GetComponent<CircleCollider2D>().enabled = true;
                
                if (Input.GetKeyDown(keyToPress))
                {
                    hit = true;
                    CalculateHitNoteAccuracy(GameManager.GetTimeInMs());
                    gameObject.SetActive(false);
                }
            }

            if (!IsInInteractionBound(GameManager.GetTimeInMs()) &&
                GameManager.GetTimeInMs() > InteractionBoundsEndTimeInMs)
            {
                GameManager.instance.NoteMiss();
                gameObject.SetActive(false);
            }
        }

        private void CalculateHitNoteAccuracy(double gameTime)
        {
            if (gameTime < PerfectInteractionTimeInMs + ODTimingPerfectHit 
                && gameTime > PerfectInteractionTimeInMs - ODTimingPerfectHit)
            {
                GameManager.PerfectHit();
            }
            else if (gameTime > PerfectInteractionTimeInMs + ODTimingPerfectHit &&
                     gameTime < PerfectInteractionTimeInMs + ODTimingOkHit ||
                     gameTime < PerfectInteractionTimeInMs - ODTimingPerfectHit &&
                     gameTime > PerfectInteractionTimeInMs - ODTimingOkHit)
            {
                GameManager.OkHit();
            }
            GameManager.GoodHit();
        }

        private void SetKeyToPress()
        {
            keyToPress = x switch
            {
                -1.5f => KeyCode.A,
                -0.5f => KeyCode.S,
                0.5f => KeyCode.K,
                1.5f => KeyCode.L,
                _ => keyToPress
            };  
        }

        IEnumerator HitObjectMove()
        {
            speed = y / (scrollSpeed / 1000);
            transform.Translate(Vector3.down * (speed * UnityEngine.Time.deltaTime));
            yield return null;
        }

        public bool IsInInteractionBound(double gameTime)
        {
            if (gameTime >= InteractionBoundsStartTimeInMs
                && gameTime <= InteractionBoundsEndTimeInMs)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Activator")) return;
            if (hit) return;
            canBeHit = false;
            GameManager.NoteMiss();
            gameObject.SetActive(false);
        }
    }
}