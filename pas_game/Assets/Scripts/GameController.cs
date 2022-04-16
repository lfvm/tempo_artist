using System;
using System.Collections.Generic;
using System.Collections;
using diag = System.Diagnostics;
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
    public float songPositionMs;
    public float songPositionInBeats;
    public float dspSongTime;
    public float gameTimeMS;
    public float timer;
    
    public float AR;
    public float OD;
    public float offset;
    
    public Text scoreText;
    public Text comboText;
    public Text msText;
    
    private diag.Stopwatch SongStopWatch;
    private diag.Stopwatch gameStopwatch;
    private Beatmap beatmap;

    private int currentCombo;
    private int currentScore;
    //private int noteListIndex;
    
    private const int scorePerOkNote = 50;
    private const int scorePerGoodNote = 100;
    private const int socrePerPerfectNote = 300;

    private float deltaTime;
    private float lastTime;

    private bool hasStarted;
    
    private void Start()
    {
        instance = this;
        SongStopWatch = new diag.Stopwatch();
        gameStopwatch = new diag.Stopwatch();

        scoreText.text = "0";
        comboText.text = "0";
        msText.text = "0";

        secPerBeat = 60f / songBpm;
 
        //noteListIndex = 0;

        lanes.Add(lane1);
        lanes.Add(lane2);
        lanes.Add(lane3);
        lanes.Add(lane4);

        var JsonParser = new JsonParser();
        beatmap = JsonParser.JsonToBeatmap("Assets/Beatmaps/blue zenith/Blue Zenith.json");
        
        createNotesFromBeatmap();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            if(Input.GetKeyDown("="))
                IncreaseOffset();
            if(Input.GetKeyDown("-"))
                DecreaseOffset();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                musicSource.Play();
                gameStopwatch.Start();
                SongStopWatch.Start();
                hasStarted = true;
                foreach (var hitObject in notes)
                {
                    hitObject.SetActive();
                }
            }
        }
        else
        {
            gameTimeMS = gameStopwatch.ElapsedMilliseconds;
            dspSongTime = (float) AudioSettings.dspTime;
            songPosition = (float) (AudioSettings.dspTime - dspSongTime);
            songPositionMs = (int)GetElapsedSongTime();
            msText.text = songPositionMs.ToString();
            songPositionInBeats = songPosition / secPerBeat;
            deltaTime = musicSource.time - lastTime;

            timer += deltaTime;
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

    private void createNotesFromBeatmap()
    {
        foreach (var hitObject in beatmap.hitObjects)
        {
            var x = float.Parse(hitObject.x, CultureInfo.InvariantCulture.NumberFormat);
            var y = (float.Parse(hitObject.time) * AR + offset) / 1000;
            var hitTime = Int32.Parse(hitObject.time);
            
            float newX = 0;
            
            newX = x switch
            {
                64 => -1.5f,
                192 => -0.5f,
                320 => 0.5f,
                448 => 1.5f,
                _ => newX
            };
            
            var note = Instantiate(noteObject, new Vector3(newX, 7,0), Quaternion.identity);
            note.x = newX;
            note.y = y;
            note.hitTime = hitTime;
            note.AR = AR;
            notes.Add(note);
        }
    }
    
    public float GetAR()
    {
        return AR;
    }
    
    private float GetElapsedSongTime()
    {
        return SongStopWatch.ElapsedMilliseconds;
    }

    public float GetOD()
    {
        return OD;
    }

    private void IncreaseOffset()
    {
        offset += 5;
    }

    private void DecreaseOffset()
    {
        offset -= 5;
    }

    public bool gameHasStarted()
    {
        return hasStarted;
    }
}