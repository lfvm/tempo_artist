using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    private bool hit = false;

    public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;

    void Start()
    {
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
    
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                // GameController.instance.NoteHit();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
            if (!hit)
            {
                GameController.instance.NoteMiss();
                Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
            }
        }
    }
}
