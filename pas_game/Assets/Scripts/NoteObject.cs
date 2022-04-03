using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
    public KeyCode keyToPress;
    
    
    public float x;
    public float y;
    public float arTiming;
    
    public int hitTime;
    public int startTime;

    public bool canBePressed;

    public float AR;
    
    private bool hit;
    private bool isMoving;
    
    private Vector3 endPosition;

    public void Init(float xPos, float yPos, int time)
    {
        x = xPos switch
        {
            64 => -1.5f,
            192 => -0.5f,
            320 => 0.5f,
            448 => 1.5f,
            _ => x
        };
        hitTime = time;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameObject.SetActive(true);
        endPosition = new Vector3(transform.position.x, -7, transform.position.y);
        arTiming = Timing.ARTiming.getArTimingBiggerThanFive(AR);
        startTime = hitTime - (int)Timing.ARTiming.getArTimingBiggerThanFive(AR);
        AR = GameController.instance.noteSpeed;
        var posX = gameObject.transform.position.x;
        keyToPress = posX switch
        {
            -1.5f => KeyCode.A,
            -0.5f => KeyCode.S,
            0.5f => KeyCode.K,
            1.5f => KeyCode.L,
            _ => keyToPress
        };
    }

    private void Update()
    {
        if (startTime == GameController.instance.songTimeMs + GameController.instance.songOffset)
        {
            moveNote();
        }

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                hit = true;
                gameObject.SetActive(false);

                if (transform.position.y > -3.3f || transform.position.y < -4.5)
                {
                    GameController.instance.Okhit();
                    Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
                else if (transform.position.y > -3.5f || transform.position.y < -4.3)
                {
                    GameController.instance.GoodHit();
                    Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
                else
                {
                    GameController.instance.PerfectHit();
                    Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
            }
        }
    }

    private void checkForActivationTime()
    {
        if (startTime == Timing.ARTiming.getArTimingBiggerThanFive(AR))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - AR * Time.deltaTime, 0f);
        }
    }

    private void moveNote()
    {
        rigidbody.WakeUp();
        rigidbody.velocity = new Vector2(0, -AR);
        // if (!isMoving)
        // {
        //     transform.position = Vector2.Lerp(
        //         transform.position,
        //         endPosition,
        //         -AR
        //     );    
        //     isMoving = true;
        // }
        //transform.position = new Vector3(transform.position.x, transform.position.y - noteSpeed * Time.deltaTime, 0f);
        
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
        Destroy(gameObject);
    }
}