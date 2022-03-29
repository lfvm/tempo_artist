using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UiController uiController;
    public BeatScroller beatScroller;

    public static GameController instance;
    
    public GameObject hitBox1;
    public GameObject hitBox2;
    public GameObject hitBox3;
    public GameObject hitBox4;

    public GameObject lane1;
    public GameObject lane2;
    public GameObject lane3;
    public GameObject lane4;

    public List<GameObject> lanes;
    public List<GameObject> notes;

    public GameObject NoteObject;

    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    // Cuantos segundos han pasado desde que comenzo la cancion.
    public float dspSongTime;
    public AudioSource musicSource;
    
    private float lastTime = 0f;
    private float deltaTime = 0f;
    private float timer = 0f;

    private bool paused;
    private bool hasStarted;

    private void Start()
    {
        instance = this;
        
        uiController.score.text = "0";
        uiController.combo.text = "0";

        //musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;
        
        dspSongTime = (float)AudioSettings.dspTime;
        
        lanes.Add(lane1);
        lanes.Add(lane2);
        lanes.Add(lane3);
        lanes.Add(lane4);
    }

    private void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                beatScroller.hasStarted = true;
                musicSource.Play();
            }
        }
        else
        {
            // Cuantos segundos han pasado desde que comenzo la cancion
            songPosition = (float) (AudioSettings.dspTime - dspSongTime);

            // Cuantos beats han pasado desde que comenzo la cancion;
            songPositionInBeats = songPosition / secPerBeat;
        
            deltaTime = musicSource.time - lastTime;
            timer += deltaTime;

            if (timer >= secPerBeat)
            {
                var rand = Random.Range(0, 4);
                // Creacion de las notas
                var note = Instantiate(NoteObject, new Vector3(lanes[rand].transform.position.x, 7, 0), Quaternion.identity);
                notes.Add(note);
                note.transform.parent = beatScroller.transform;
                timer -= secPerBeat;
            }
        }
        lastTime = musicSource.time;
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");
    }

    public void NoteMiss()
    {
        Debug.Log("Miss");
    }
}
