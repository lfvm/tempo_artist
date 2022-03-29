using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public int noteSpeed;
    public bool hasStarted;

    private void Update()
    {
        if (!hasStarted)
        {

        }
        else
        {
            transform.position -= new Vector3(0f, noteSpeed * Time.deltaTime, 0f);
        }
    }
}
