using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // public UiController uiController;
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

    public Text scoreText;
    public Text comboText;
    
    private float lastTime = 0f;
    private float deltaTime = 0f;
    private float timer = 0f;

    private bool paused;
    private bool hasStarted;

    private int currentScore;
    
    private const int scorePerOkNote = 50;
    private const int scorePerGoodNote = 100;
    private const int socrePerPerfectNote = 300;

    private int currentCombo;

    private void Start()
    {
        instance = this;
        
        scoreText.text = "0";
        comboText.text = "0";

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
        currentCombo++;
        scoreText.text = currentScore.ToString();
        comboText.text = currentCombo.ToString();
    }

    public void Okhit()
    {
        currentScore += scorePerOkNote * currentCombo;
        NoteHit();
    }

    public void GoodHit()
    {
        NoteHit(); 
        currentScore += scorePerGoodNote * currentCombo;
    }

    public void PerfectHit()
    {
        NoteHit();
        currentScore += socrePerPerfectNote * currentCombo;
    }

    public void NoteMiss()
    {
        currentCombo = 0;
        comboText.text = currentCombo.ToString();
    }
}
