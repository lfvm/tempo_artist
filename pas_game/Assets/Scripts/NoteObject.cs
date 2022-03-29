using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    private bool hit = false;
    
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
                GameController.instance.NoteHit();
                hit = true;
                gameObject.SetActive(false);
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
                GameController.instance.NoteMiss();
        }
    }
}
