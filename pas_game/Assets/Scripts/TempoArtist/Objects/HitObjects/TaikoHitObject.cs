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

        // x and Y position of the note
        public float x;
        public float y;
        // when the note has to be hit
        public int time;
        // When the note should become active
        public int hitsound;
        private float startTime;
        public int queueId;

        private float speed;
        private float ODTimingOkHit;
        private float ODTimingGoodHit;
        private float ODTimingPerfectHit;

        private bool canBeHit;
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
            
            transform.GetComponent<Rigidbody2D>().simulated = false;
            transform.GetComponent<CircleCollider2D>().enabled = true;
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
            speed = 10.85f / (speed / 1000);
            
            startTime = time - speed;

            hitSound.clip = hitNormal;
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
                
                if (Input.GetKeyDown(keysToPress[0]) || Input.GetKeyDown(keysToPress[1]))
                {
                    hit = true;
                    hitSound.Play();
                    CalculateHitNoteAccuracy(GameManager.GetTimeInMs());
                    gameObject.SetActive(false);
                }
            }

            if (!IsInInteractionBound(GameManager.GetTimeInMs()) &&
                GameManager.GetTimeInMs() > InteractionBoundsEndTimeInMs)
            {
                GameManager.NoteMiss();
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
            var xtemp = x + 5.5f;
            speed = xtemp / (speed / 1000);
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
