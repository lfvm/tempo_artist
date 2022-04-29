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
            Debug.Log("Song id: " + MapResult.mapId);

        }

        private void SetMapResultsText()
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
        private Score CreateScore(int user_id)
        {
            var score = new Score();
            score.user_id = user_id;
            score.level_id = MapResult.mapId;
            score.accuracy = MapResult.accuracy;
            score.perfect_hits = MapResult.perfectHits;
            score.good_hits = MapResult.goodHits;
            score.max_combo = MapResult.maxCombo;
            score.total_points = MapResult.score;
            return score;
        }

        
        //Funcion que sera llamada por el server
        public void sendSaveRequest(int id){

            if (savedData == false) {
                Score score = CreateScore(id);
                string json = JsonUtility.ToJson(score);
                string url = "https://tempo-artist.herokuapp.com/api/puntuaciones/nueva";
                StartCoroutine(Post(url, json));
                Debug.Log("Posted request");               
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
            Debug.Log("Response Code: " + request.responseCode);
        }
    }
}
