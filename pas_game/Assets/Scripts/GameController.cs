using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using BeatmapConverter;
using BeatmapConverter.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public AudioSource musicSource;
    public NoteObject noteObject;
    
    public GameObject hitBox1;
    public GameObject hitBox2;
    public GameObject hitBox3;
    public GameObject hitBox4;

    public GameObject lane1;
    public GameObject lane2;
    public GameObject lane3;
    public GameObject lane4;

    public List<GameObject> lanes;
    public List<NoteObject> notes;

    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float songTimeMs;
    public float dspSongTime;
    public float noteSpeed;
    public float timer;

    public Text scoreText;
    public Text comboText;
    
    private Stopwatch stopWatch;
    private Beatmap beatmap;

    private int currentCombo;
    private int currentScore;
    private int noteListIndex;
    
    private const int scorePerOkNote = 50;
    private const int scorePerGoodNote = 100;
    private const int socrePerPerfectNote = 300;

    private float deltaTime;
    private float lastTime;
    
    private bool hasStarted;
    private bool paused;
    
    private void Start()
    {
        instance = this;
        stopWatch = new Stopwatch();
        stopWatch.Start();
        
        scoreText.text = "0";
        comboText.text = "0";
        
        dspSongTime = (float)AudioSettings.dspTime;

        //musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;

        dspSongTime = (float) AudioSettings.dspTime;

        noteListIndex = 0;

        lanes.Add(lane1);
        lanes.Add(lane2);
        lanes.Add(lane3);
        lanes.Add(lane4);

        var JsonParser = new JsonParser();
        beatmap = JsonParser.JsonToBeatmap("Assets/Beatmaps/Holdin' On (Skrillex and Nero Remix) (Cut Ver.).json");
        
        for (var i = 0; i < beatmap.hitObjects.Count; i++) createNote();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                musicSource.Play();
            }
        }
        else
        {
            songPosition = (float) (AudioSettings.dspTime - dspSongTime);
            songTimeMs = getElapsedTime();
            songPositionInBeats = songPosition / secPerBeat;
            deltaTime = musicSource.time - lastTime;

            timer += deltaTime;

            if (timer >= secPerBeat)
            {
                notes[noteListIndex].SetActive();
                noteListIndex++;
                timer -= secPerBeat;
            }
        }

        lastTime = musicSource.time;
    }

    private void NoteHit()
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

    private void createNote()
    {
        foreach (var hitObject in beatmap.hitObjects)
        {
            var x = float.Parse(hitObject.x, CultureInfo.InvariantCulture.NumberFormat);
            float newX = 0;
            
            newX = x switch
            {
                64 => -1.5f,
                192 => -0.5f,
                320 => 0.5f,
                448 => 1.5f,
                _ => newX
            };
            
            var time = float.Parse(hitObject.time, CultureInfo.InvariantCulture.NumberFormat);
            
            var note = Instantiate(noteObject, new Vector3(newX, 7,0), Quaternion.identity);
            notes.Add(note);
            note.SetInactive();
        }
    }
    
    public float getNoteSpeed()
    {
        return noteSpeed;
    }
    
    public float getElapsedTime()
    {
        return stopWatch.ElapsedMilliseconds;
    }
}