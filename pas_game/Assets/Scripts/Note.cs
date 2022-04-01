using UnityEngine;

public class Note
{
    public bool canBePressed;

    private bool hit;
    private int hitTime;
    public KeyCode keyToPress;

    public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;

    private float xPos;
    private float yPos;

    public Note(float xPos, float yPos, int hitTime)
    {
        switch (xPos)
        {
            case 64:
                this.xPos = -1.5f;
                break;
            case 192:
                this.xPos = -0.5f;
                break;
            case 320:
                this.xPos = 0.5f;
                break;
            case 448:
                this.xPos = 1.5f;
                break;
        }

        keyToPress = xPos switch
        {
            -1.5f => KeyCode.A,
            -0.5f => KeyCode.S,
            0.5f => KeyCode.K,
            1.5f => KeyCode.L,
            _ => keyToPress
        };

        // When the note is supposed to be hit in milliseconds.
        this.hitTime = hitTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
            if (canBePressed)
            {
                hit = true;

                if (yPos > -3.3f || yPos < -4.5)
                {
                    GameController.instance.Okhit();
                    Object.Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
                else if (yPos > -3.5f || yPos < -4.3)
                {
                    GameController.instance.GoodHit();
                    Object.Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
                else
                {
                    GameController.instance.PerfectHit();
                    Object.Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
                }
            }
    }

    private void CheckNotePressed()
    {
        if (xPos < xPos - hitTime)
            canBePressed = true;
        canBePressed = false;
        if (!hit)
        {
            GameController.instance.NoteMiss();
            Object.Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
        }
    }
}