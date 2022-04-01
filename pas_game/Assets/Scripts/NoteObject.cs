using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    public float x;
    public float y;

    private float noteSpeed;

    public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;

    private bool hit;
    private float hitTime;

    public void Init(float xPos, float yPos, float hitTime)
    {
        x = xPos switch
        {
            64 => -1.5f,
            192 => -0.5f,
            320 => 0.5f,
            448 => 1.5f,
            _ => x
        };
        // When the note is supposed to be hit in milliseconds.
        this.hitTime = hitTime;
    }

    private void Start()
    {
        noteSpeed = GameController.instance.noteSpeed;
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
        transform.position = new Vector3(transform.position.x, transform.position.y - noteSpeed * Time.deltaTime, 0f);
        checkLifespan();
        if (Input.GetKeyDown(keyToPress))
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator")) canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.CompareTag("Activator"))
        // {
        //     canBePressed = false;
        //     if (!hit)
        //     {
        //         GameController.instance.NoteMiss();
        //         Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
        //         Destroy(gameObject);
        //     }
        // }
    }

    private void checkLifespan()
    {
        if (hitTime >= GameController.instance.songTimeMs + 500)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}