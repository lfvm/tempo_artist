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

    void Start()
    {

        //Establecer el texto del score como 0
        uiController.score.text = "Score: 0";
        uiController.combo.text = "0";
    }

    void Update()
    {
        
    }
}
