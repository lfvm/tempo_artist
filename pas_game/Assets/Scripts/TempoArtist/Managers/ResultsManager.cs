using TempoArtist.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Collections.Generic;




//Se deben crear clases para serializar los objetos a json, en este caso 
//Se creara una clase para los Scores
[System.Serializable]
public class Score
{
    public int user_id;
    public int level_id;
    public int total_points;
    public int perfect_hits;
    public int good_hits;
    public int max_combo;
    public int accuracy;

}


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

            //Crear objeto del score, y obtener el json string del mismo
            Score newScore = new Score();
            newScore.user_id = 184;
            newScore.level_id = 4;
            newScore.total_points = 523;
            newScore.perfect_hits = 100;
            newScore.good_hits = 50;
            newScore.max_combo = 2730;
            newScore.accuracy = 80;
            string json = JsonUtility.ToJson(newScore);

            string url = "https://tempo-artist.herokuapp.com/api/puntuaciones/nueva";
            //Crear el request
            StartCoroutine(Post(url, json));


        }

        // private GameObject getRankIcon(string rankStr)
        // {
        //     
        // }


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
