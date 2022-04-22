using TempoArtist.Objects;
using TMPro;
using UnityEngine;

namespace TempoArtist.Managers
{
    public class ResultsManager : MonoBehaviour
    {
        //private string rankStr = "S";

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
            scoreText.text = MapResult.score.ToString();
            okHitsText.text = MapResult.okHits.ToString();
            goodHitsText.text = MapResult.goodHits.ToString();
            perfectHitsText.text = MapResult.perfectHits.ToString();
            missedHitsText.text = MapResult.missedHits.ToString();
            maxComboText.text = MapResult.maxCombo.ToString();
            accuracyText.text = MapResult.accuracy.ToString("0.00") + "%";
        }

        // private GameObject getRankIcon(string rankStr)
        // {
        //     
        // }
    }
}
