using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public int noteSpeed;
    public bool hasStarted;
    void Start()
    {
        beatTempo = beatTempo / 60f;   
    }
    
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, noteSpeed * Time.deltaTime, 0f);
        }
    }
}
