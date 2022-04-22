using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TempoArtist.Beatmaps;
using TempoArtist.Managers;
using UnityEngine.UI;

public class BeatmapCard : MonoBehaviour
{
    public GameObject cardTitle { get; set; }
    public GameObject cardArtist { get; set; }
    public Beatmap Beatmap { get; set; }

    private Button button;
    
    private SongSelectManager songSelectManager;
    private void Awake()
    {
        button = transform.GetComponent<Button>();
        cardTitle = transform.GetChild(0).gameObject;
        cardArtist = transform.GetChild(1).gameObject;
    }

    private void Start()
    { 
        songSelectManager = SongSelectManager.Instance;
        button.onClick.AddListener(delegate{songSelectManager.SetSelectedBeatmapCard(this); }); 
    }
}