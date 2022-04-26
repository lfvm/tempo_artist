using System;
using System.Collections;
using TempoArtist.Managers;
using UnityEngine;

namespace TempoArtist.Objects.HitObjects
{
    public class ManiaHitObject : MonoBehaviour
    {
        public int Time
        { 
            get => time;
            set { time = value; }
        }
        
        public float X
        {
            get => x;
            set { x = value; }
        }
        
        public float Y
        {
            get => y;
            set { y = value; }
        }
        
        public int QueueID
        {
            get => queueId;
            set {queueId = value; }
        }

        // Reference to the GameManager and GameSetup instances.
        private ManiaGameManager GameManager;
        private ManiaGameSetup GameSetup;
        
        private AudioSource hitsound;
        
        [SerializeField] private AudioClip hitClap;
        [SerializeField] private AudioClip hitNormal;
        
        private KeyCode keyToPress;

        // x and Y position of the note
        private float x;
        private float y;
        // when the note has to be hit
        private int time;
        // When the note should become active
        [SerializeField] private float startTime;
        private float speed;
        private int queueId;

        private float scrollSpeed;
        private float ODTimingOkHit;
        private float ODTimingGoodHit;
        private float ODTimingPerfectHit;

        [SerializeField] private bool canBeHit;
        private bool hit;

        [SerializeField] private float PerfectInteractionTimeInMs;
        [SerializeField] private float InteractionBoundsStartTimeInMs;
        [SerializeField] private float InteractionBoundsEndTimeInMs;

        private void Awake()
        {
            GameManager = ManiaGameManager.instance;
            GameSetup = ManiaGameSetup.instance;
            
            hitsound = GetComponent<AudioSource>();

            // GetComponent<Rigidbody2D>().simulated = false;
            // GetComponent<CircleCollider2D>().enabled = true;
            
            gameObject.SetActive(false);
        }
        
        void Start()
        {
            SetKeyToPress();
            
            ODTimingOkHit = Timing.ODTiming.GetODTimingForOkHit(GameManager.OD);
            ODTimingGoodHit = Timing.ODTiming.GetODTimingForGoodHit(GameManager.OD);
            ODTimingPerfectHit = Timing.ODTiming.GetODTimingForPerfectHit(GameManager.OD);
            
            PerfectInteractionTimeInMs = time + GameManager.noteTimeOffset;
            InteractionBoundsStartTimeInMs = PerfectInteractionTimeInMs - ODTimingOkHit;
            InteractionBoundsEndTimeInMs = PerfectInteractionTimeInMs + ODTimingOkHit;

            scrollSpeed = GameManager.scrollSpeed;
            startTime = time - GameManager.noteTimeOffset;

            hitsound.clip = hitNormal;
            
            Debug.Log($"Current time: {GameManager.GetTimeInMs()}");
            Debug.Log($"Interraction bound start: {InteractionBoundsStartTimeInMs} Interraction bound end: {InteractionBoundsEndTimeInMs}");
        }

        private void Update()
        {
            //canBeHit = IsInInteractionBound(GameManager.GetTimeInMs());
            if (GameManager.GetTimeInMs() >= startTime)
            {
                StartCoroutine(HitObjectMove());
            }

            if (canBeHit)
            {
                // transform.GetComponent<Rigidbody2D>().simulated = true;
                // transform.GetComponent<CircleCollider2D>().enabled = true;
                
                if (Input.GetKeyDown(keyToPress))
                {
                    hit = true;
                    hitsound.Play();
                    CalculateHitNoteAccuracy(GameManager.GetTimeInMs());
                    gameObject.SetActive(false);
                    Debug.Log($"Object hit time: {time} time hit: {GameManager.GetTimeInMs()} time to get 300: {PerfectInteractionTimeInMs}");
                }
            }

            // if (!canBeHit && GameManager.GetTimeInMs() > InteractionBoundsEndTimeInMs)
            // {
            //     GameManager.NoteMiss();
            //     gameObject.SetActive(false);
            // }
        }

        private void CalculateHitNoteAccuracy(double gameTime)
        {
            if (transform.position.y < -3.1 && transform.position.y > -3.9)
            {
                GameManager.PerfectHit();
            }
            else if (transform.position.y < -2.9 && transform.position.y > -4.1)
            {
                GameManager.GoodHit();
            }
            else
            {
                GameManager.OkHit();
            }
        }

        // Testing
        private void CalculateHitAccuracy(double gameTime)
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
            var newY = y + 3.85f;
            speed = newY / (scrollSpeed / 1000);
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Activator"))
            {
                canBeHit = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Activator"))
            {
                if (!hit)
                {
                    GameManager.NoteMiss();
                }
                
                canBeHit = false;
                GameSetup.notes.Remove(this);
                Destroy(gameObject);
            }
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }
    }
}