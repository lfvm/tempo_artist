using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UiController uiController;

    //Referencia a las cajas donde las notas deben de hacer score
    public GameObject leftBox;
    public GameObject rightBox;

    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    // Cuantos segundos han pasado desde que comenzo la cancion.
    public float dspSongTime;
    public AudioSource musicSource;

    void Start()
    {

        //Establecer el texto del score como 0
        uiController.score.text = "Score: 0";
        uiController.combo.text = "0";

        musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;
        
        dspSongTime = (float)AudioSettings.dspTime;
        
        // musicSource.play();
    }

    void Update()
    {
        // Cuantos segundos han pasado desde que comenzo la cancion
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        // Cuantos beats han pasado desde que comenzo la cancion;
        songPositionInBeats = songPosition / secPerBeat;
    }
}
