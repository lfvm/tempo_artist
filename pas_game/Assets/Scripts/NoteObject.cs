using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
    public KeyCode keyToPress;
    
    public float x;
    public float y;
    public float arTiming;
    public float pressTime;

    private float songPosMs;
    
    public int hitTime;
    public int startTime;

    public bool canBePressed;

    public float AR;
    
    private bool hit;

    private int offset;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        keyToPress = x switch
        {
            -1.5f => KeyCode.A,
            -0.5f => KeyCode.S,
            0.5f => KeyCode.K,
            1.5f => KeyCode.L,
            _ => keyToPress
        };
        gameObject.SetActive(true);
        arTiming = Timing.ARTiming.GetArTimingBiggerThanFive(AR);
        AR = GameController.instance.AR;
    }

    private void Update()
    {
        if (GameController.instance.gameHasStarted())
        {
            StartCoroutine(MoveNote());
        }

        if (!Input.GetKeyDown(keyToPress)) return;
        if (!canBePressed) return;
        
        hit = true;
        
        if (GetSongTimeInMs() > hitTime - Timing.ODTiming.GetODTimingForPerfectHit(GameController.instance.GetOD()) + 360 || GetSongTimeInMs() < hitTime + Timing.ODTiming.GetODTimingForPerfectHit(GameController.instance.GetOD()) + 360)
        {
            GameController.instance.PerfectHit();
            Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
        }
        else if (GetSongTimeInMs() > (hitTime - Timing.ODTiming.GetODTimingForGoodHit(GameController.instance.GetOD())) + 360 || GetSongTimeInMs() < hitTime + Timing.ODTiming.GetODTimingForGoodHit(GameController.instance.GetOD()) + 360 )
        {
            GameController.instance.GoodHit();
            Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
        }
        else
        {
            GameController.instance.Okhit();
            Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
        }
        gameObject.SetActive(false);
    }

    IEnumerator MoveNote()
    {
        transform.Translate(Vector3.down * AR * Time.smoothDeltaTime);
        yield return null;

    }

    private float GetSongTimeInMs()
    {
        return GameController.instance.songPositionMs;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator")) canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Activator")) return;
        canBePressed = false;
        if (hit) return;
        GameController.instance.NoteMiss();
        Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
        gameObject.SetActive(false);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}