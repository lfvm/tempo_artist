using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UiController uiController;

    //Referencia a las cajas donde las notas deben de hacer score
    public GameObject leftHitBox;
    public GameObject rightHitBox;

    public GameObject leftLane;
    public GameObject rightLane;
    
    public GameObject noteHolder;

    public List<GameObject> lanes;
    public List<GameObject> notes;

    public GameObject NoteObject;

    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    // Cuantos segundos han pasado desde que comenzo la cancion.
    public float dspSongTime;
    public AudioSource musicSource;

    private float lastTime = 0f;
    private float deltaTime = 0f;
    private float timer = 0f;

    private bool paused;

    private void Start()
    {
        //Establecer el texto del score como 0
        uiController.score.text = "Score: 0";
        uiController.combo.text = "0";

        //musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / songBpm;
        
        dspSongTime = (float)AudioSettings.dspTime;
        
        lanes.Add(rightLane);
        lanes.Add(leftLane);
        
        // musicSource.play();

        // Crear Noytas
        // for(var i = 0; i<21; i++)
        // {
        //     //Crear una nueva nota con la funcion instantiate
        //     //Junto con las coordenadas donde vamos a colocar ese objeto
        //     //y la rotacion del objeto que se define con Quaternion
        //     //Y agregar la columna a la lista de columnas
        //     notes.Add( Instantiate( blueNote, new Vector2( -2.428089f, 5 + (i * 2) ), Quaternion.identity ) );
        // }
    }

    private void Update()
    {
        // Cuantos segundos han pasado desde que comenzo la cancion
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        // Cuantos beats han pasado desde que comenzo la cancion;
        songPositionInBeats = songPosition / secPerBeat;

        int rand = Random.Range(0, 2);
        deltaTime = musicSource.time - lastTime;
        timer += deltaTime;

        if (timer >= secPerBeat)
        {
            // Creacion de las notas
            GameObject note = Instantiate(NoteObject, new Vector2(lanes[rand].transform.position.x, 7f), Quaternion.identity);
            notes.Add(note);
            note.transform.parent = noteHolder.transform;
            timer -= secPerBeat;
        }

        lastTime = musicSource.time;

        // //Mover las notas en y
        // foreach (var note in notes)
        // {
        //     //Mueve cada nota hacia la abajo en una unidad cada update
        //     note.transform.position = note.transform.position + new Vector3(0,-1,0)  * Time.deltaTime * 2;   
        // }
    }
}
