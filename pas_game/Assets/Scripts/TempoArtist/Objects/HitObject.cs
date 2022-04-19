using System;
using System.Collections;
using UnityEngine;

namespace TempoArtist.Objects
{
    public class HitObject : MonoBehaviour
    {
        // Reference to the GameManager and GameSetup instances.
        private GameManager GameManager;
        private GameSetup GameSetup;

        private GameObject hitZone;
        
        // x and Y position of the note
        public float x;
        public float y;
        
        // when the note has to be hit
        public int time;
        // When the note should become active
        public float startTime;

        public float speed;

        public int queueId;
        
        public bool IsHitAttempted;

        public float scrollSpeed;

        public bool canBeHit;
        private bool hit;

        public KeyCode keyToPress;

        // public event EventHandler OnInteract;

        public int InteractionID;
        public int AccuracyLaybackMs;
        public float PerfectInteractionTimeInMs;
        public float InteractionBoundsStartTimeInMs;
        public float InteractionBoundsEndTimeInMs;

        private void Awake()
        {
            GameManager = GameManager.instance;
            GameSetup = GameSetup.instance;

            hitZone = GameObject.Find("HitZone1");
            
            transform.GetComponent<Rigidbody2D>().simulated = false;
            transform.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.SetActive(false);
        }
        
        void Start()
        {
            keyToPress = x switch
            {
                -1.5f => KeyCode.A,
                -0.5f => KeyCode.S,
                0.5f => KeyCode.K,
                1.5f => KeyCode.L,
                _ => keyToPress
            };
            
            PerfectInteractionTimeInMs = time + GameManager.noteTimeOffset;
            InteractionBoundsStartTimeInMs = PerfectInteractionTimeInMs - Timing.ODTiming.GetODTimingForOkHit(GameManager.OD);
            InteractionBoundsEndTimeInMs = PerfectInteractionTimeInMs + Timing.ODTiming.GetODTimingForOkHit(GameManager.OD);

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
                    CalculateHitNoteAccuracy(GameManager.GetTimeInMs());
                    hit = true;
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

        private void CalculateHitNoteAccuracy(double time)
        {
            if (time >= this.time + Timing.ODTiming.GetODTimingForPerfectHit(GameManager.OD) 
                || time <= this.time + Timing.ODTiming.GetODTimingForPerfectHit(GameManager.OD))
            {
                GameManager.PerfectHit();
            }
            else if (time >= this.time + Timing.ODTiming.GetODTimingForGoodHit(GameManager.OD)
                     || time <= this.time + Timing.ODTiming.GetODTimingForGoodHit(GameManager.OD))
            {
                GameManager.GoodHit();
            }
            GameManager.OkHit();
        }

        IEnumerator HitObjectMove()
        {
            speed = y / (scrollSpeed / 1000);
            transform.Translate(Vector3.down * (speed * Time.deltaTime));
            yield return null;
        }

        public bool IsInInteractionBound(double time)
        {
            if (time >= InteractionBoundsStartTimeInMs
                && time <= InteractionBoundsEndTimeInMs)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("a");
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