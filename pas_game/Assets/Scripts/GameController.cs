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

<<<<<<< Updated upstream
    void Start()
=======
    //Ref al objeto de notas 
    public GameObject blueNote;
    //Lista de Notas
    public List<GameObject> notes;

    private void Start()
>>>>>>> Stashed changes
    {
        //Establecer el texto del score como 0
        uiController.score.text = "Score: 0";
        uiController.combo.text = "0";

        musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;
        
        dspSongTime = (float)AudioSettings.dspTime;
        
        // musicSource.play();
<<<<<<< Updated upstream
=======
        
        // Crear Noytas
        for(var i = 0; i<21; i++)
        {
            //Crear una nueva nota con la funcion instantiate
            //Junto con las coordenadas donde vamos a colocar ese objeto
            //y la rotacion del objeto que se define con Quaternion
            //Y agregar la columna a la lista de columnas
            notes.Add( Instantiate( blueNote, new Vector2( -2.428089f, 5 + (i * 2) ), Quaternion.identity ) );
        }
>>>>>>> Stashed changes
    }

    private void Update()
    {
        // Cuantos segundos han pasado desde que comenzo la cancion
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        // Cuantos beats han pasado desde que comenzo la cancion;
        songPositionInBeats = songPosition / secPerBeat;
<<<<<<< Updated upstream
=======
        
        //Mover las notas en y
        foreach (var note in notes)
        {
            //Mueve cada nota hacia la abajo en una unidad cada update
            note.transform.position = note.transform.position + new Vector3(0,-1,0)  * Time.deltaTime * 2;   
        }
>>>>>>> Stashed changes
    }
}
