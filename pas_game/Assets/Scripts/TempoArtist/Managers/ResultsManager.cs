using System;
using TempoArtist.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Collections.Generic;




//Se deben crear clases para serializar los objetos a json, en este caso 
//Se creara una clase para los Scores

namespace TempoArtist.Managers
{
    public class ResultsManager : MonoBehaviour
    {
        //private string rankStr = "S";
        public static ResultsManager instance;

        private bool savedData = false;

        public MapResult mapResult { get; set; }
        private Score score;
        
        
        [SerializeField] private GameObject rank;
        [SerializeField] public TextMeshProUGUI scoreText,
            okHitsText,
            goodHitsText,
            perfectHitsText,
            missedHitsText,
            maxComboText,
            accuracyText;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        { 
            SetMapResultsText(); 
            Debug.Log("Song id: " + mapResult.mapId);

        }

        private void SetMapResultsText()
        {
            scoreText.text = mapResult.score.ToString();
            okHitsText.text = mapResult.okHits.ToString();
            goodHitsText.text = mapResult.goodHits.ToString();
            perfectHitsText.text = mapResult.perfectHits.ToString();
            missedHitsText.text = mapResult.missedHits.ToString();
            maxComboText.text = mapResult.maxCombo.ToString();
            accuracyText.text = mapResult.accuracy.ToString("0.00") + "%"; 
        }

    
        // private GameObject getRankIcon(string rankStr)
        // {
        //     
        // }
        private Score CreateScore(int user_id)
        {
            var score = new Score();
            score.user_id = user_id;
            score.level_id = mapResult.mapId;
            score.accuracy = mapResult.accuracy;
            score.perfect_hits = mapResult.perfectHits;
            score.good_hits = mapResult.goodHits;
            score.max_combo = mapResult.maxCombo;
            score.total_points = mapResult.score;
            return score;
        }

        
        //Funcion que sera llamada por el server
        public void sendSaveRequest(int id){

            if (savedData == false) {
                Score score = CreateScore(id);
                string json = JsonUtility.ToJson(score);
                string url = "https://tempo-artist.herokuapp.com/api/puntuaciones/nueva";
                StartCoroutine(Post(url, json));
            } else {
                savedData = true;
            }
        }


        //Funcion para hacer peticion http post al server
        IEnumerator Post(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            Debug.Log("Status Code: " + request.responseCode);
        }
    }
}
