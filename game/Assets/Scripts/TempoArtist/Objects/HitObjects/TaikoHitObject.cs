using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using TempoArtist.Managers;

namespace TempoArtist.Objects.HitObjects
{
    public class TaikoHitObject : MonoBehaviour
    {
        // Reference to the GameManager and GameSetup instances.
        private TaikoGameManager GameManager;
        private TaikoGameSetup GameSetup;
        
        private SpriteRenderer spriteRenderer;

        public float X
        {
            get => x;
            set => x = value;
        }
        
        public float Y
        {
            get => y;
            set => y = value;
        }
        
        public int Time
        {
            get => time;
            set => time = value;
        }
        
        public float StartTIme
        {
            get => startTime;
            set => startTime = value;
        }
        
        // x and Y position of the note
        public float x;
        public float y;
        // when the note has to be hit
        public int time;
        // When the note should become active
        public int hitsound;
        private float startTime;
        public int queueId;

        [SerializeField] private float speed;
        private float ODTimingOkHit;
        private float ODTimingGoodHit;
        private float ODTimingPerfectHit;

        [SerializeField] private bool canBeHit;
        private bool hit;

        private List<KeyCode> keysToPress;

        private float PerfectInteractionTimeInMs;
        private float InteractionBoundsStartTimeInMs;
        private float InteractionBoundsEndTimeInMs;

        [SerializeField] private Sprite katSprite;
        [SerializeField] private Sprite donSprite;

        [SerializeField] private AudioSource hitSound;
        
        [SerializeField] private AudioClip hitClap;
        [SerializeField] private AudioClip hitNormal;

        private void Awake()
        {
            GameManager = TaikoGameManager.instance;
            GameSetup = TaikoGameSetup.instance;
            
            hitSound = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            gameObject.SetActive(false);
        }
        
        void Start()
        {
            keysToPress = new List<KeyCode>();
            ODTimingOkHit = Timing.ODTiming.GetODTimingForOkHit(GameManager.OD);
            ODTimingGoodHit = Timing.ODTiming.GetODTimingForGoodHit(GameManager.OD);
            ODTimingPerfectHit = Timing.ODTiming.GetODTimingForPerfectHit(GameManager.OD);

            PerfectInteractionTimeInMs = time + GameManager.noteTimeOffset;
            InteractionBoundsStartTimeInMs = PerfectInteractionTimeInMs - ODTimingOkHit;
            InteractionBoundsEndTimeInMs = PerfectInteractionTimeInMs + ODTimingOkHit;
            
            SetKeyToPress();
            SetSpriteAndHitsound();

            speed = GameManager.scrollSpeed;
            speed = 15.5f / (speed / 1000);
            
            startTime = time - speed;

            hitSound.clip = hitNormal;
        }

        private void Update()
        {
            if (GameManager.GetTimeInMs() >= startTime)
            {
                StartCoroutine(HitObjectMove());
            }

            if (Input.GetKeyDown(keysToPress[0]) || Input.GetKeyDown(keysToPress[1]))
            {
                if (canBeHit)
                {
                    hitSound.Play();
                    hit = true;
                    CalculateHitNoteAccuracy(GameManager.GetTimeInMs());
                    gameObject.SetActive(false);
                }
            }
        }

        private void CalculateHitNoteAccuracy(double gameTime)
        {
            if (transform.position.y < -6.5 && transform.position.y > -4.5)
            {
                GameManager.OkHit();
            }
            else if (transform.position.y < -6.2 && transform.position.y > -4.8)
            {
                GameManager.GoodHit();
            }
            else
            {
                GameManager.PerfectHit();
            }
        }

        private void SetKeyToPress()
        {
            switch (hitsound)
            {
                case 0:
                    keysToPress.Add(KeyCode.S);
                    keysToPress.Add(KeyCode.K);
                    break;
                case 8:
                    keysToPress.Add(KeyCode.A);
                    keysToPress.Add(KeyCode.L);
                    break;
                default:
                    keysToPress.Add(KeyCode.A);
                    keysToPress.Add(KeyCode.L);
                    break;
            }
        }

        private void SetSpriteAndHitsound()
        {
            switch (hitsound)
            {
                case 0:
                    spriteRenderer.sprite = donSprite;
                    hitSound.clip = hitNormal;
                    GetComponent<AudioSource>().clip = hitNormal;
                    break;
                case 8:
                    //A - L
                    spriteRenderer.sprite = katSprite;
                    hitSound.clip = hitClap;
                    GetComponent<AudioSource>().clip = hitNormal;
                    break;
                default:
                    spriteRenderer.sprite = katSprite;
                    hitSound.clip = hitNormal;
                    GetComponent<AudioSource>().clip = hitNormal;
                    break;
            }
        }

        IEnumerator HitObjectMove()
        {
            transform.Translate(Vector3.left * (speed * UnityEngine.Time.deltaTime));
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

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Activator"))
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
