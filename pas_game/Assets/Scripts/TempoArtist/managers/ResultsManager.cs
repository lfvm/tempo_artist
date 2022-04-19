using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TempoArtist;

public class ResultsManager : MonoBehaviour
{
    private string rankStr = "S";
    
    private float accuracy = 100;
    
    [SerializeField] private GameObject rank;
    [SerializeField] public TextMeshProUGUI scoreText,
        okHitsText,
        goodHitsText,
        perfectHitsText,
        missedHitsText,
        maxComboText,
        accuracyText;

    private void Start()
    {
        var totalHits = MapResult.okHits + MapResult.goodHits + MapResult.perfectHits;
        accuracy = (totalHits / MapResult.totalNotes) * 100f;
        
        scoreText.text = MapResult.score.ToString();
        okHitsText.text = MapResult.okHits.ToString();
        goodHitsText.text = MapResult.goodHits.ToString();
        perfectHitsText.text = MapResult.perfectHits.ToString();
        missedHitsText.text = MapResult.missedHits.ToString();
        maxComboText.text = MapResult.maxCombo.ToString();
        accuracyText.text = accuracy.ToString("F1") + "%";
    }

    private GameObject getRankIcon(string rankStr)
    {
        return null;
    }
}
