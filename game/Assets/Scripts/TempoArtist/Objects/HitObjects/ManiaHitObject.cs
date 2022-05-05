using System;
using System.Collections;
using TempoArtist.Managers;
using UnityEngine;

namespace TempoArtist.Objects.HitObjects
{
    public class ManiaHitObject : MonoBehaviour
    {
        // when the note has to be hit
        public int Time { get; set; }
        // When the note should become active
        public float StartTime { get; private set; }
        // x and Y position of the note
        public float X { get; set; }
        public float Y { get; set; }
        public int QueueID { get; set; }

        // Reference to the GameManager and GameSetup instances.
        private ManiaGameManager GameManager;
        private ManiaGameSetup GameSetup;
        
        private AudioSource hitsound;
        
        [SerializeField] private AudioClip hitClap;
        [SerializeField] private AudioClip hitNormal;
        
        private KeyCode keyToPress;
        
        private float speed;

        private float ODTimingOkHit;
        private float ODTimingGoodHit;
        private float ODTimingPerfectHit;

        private bool canBeHit;
        private bool hit;

        private float PerfectInteractionTimeInMs;
        private float InteractionBoundsStartTimeInMs;
        private float InteractionBoundsEndTimeInMs;

        private void Awake()
        {
            GameManager = ManiaGameManager.instance;
            GameSetup = ManiaGameSetup.instance;
            
            hitsound = GetComponent<AudioSource>();
            
            hitsound.loop = false;
            hitsound.playOnAwake = false;

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
            
            PerfectInteractionTimeInMs = Time + GameManager.NoteTimeOffset;
            InteractionBoundsStartTimeInMs = PerfectInteractionTimeInMs - ODTimingOkHit;
            InteractionBoundsEndTimeInMs = PerfectInteractionTimeInMs + ODTimingOkHit;
            
            speed = GameManager.ScrollSpeed;
            speed = 10.85f / (speed / 1000);
            
            StartTime = Time - speed;
            
            hitsound.clip = hitNormal;

            //Debug.Log($"object id: {queueId} start time: {GameManager.GetTimeInMs()} Interraction bound start: {InteractionBoundsStartTimeInMs} Interraction bound end: {InteractionBoundsEndTimeInMs} Perfect hit time: {PerfectInteractionTimeInMs}");
        }

        private void Update()
        {
            //canBeHit = IsInInteractionBound(GameManager.GetTimeInMs());
            if (GameManager.GetTimeInMs() >= StartTime)
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
                    //Debug.Log($"Object hit time: {time} time hit: {GameManager.GetTimeInMs()} time to get 300: {PerfectInteractionTimeInMs}");
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
            keyToPress = X switch
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
                gameObject.SetActive(false);
            }
        }
    }
}