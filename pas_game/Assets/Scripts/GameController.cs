using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private UiController uiController;
    void Start()
    {
        // uiController = GameObject.Find("UiController");
        uiController.score.text = "0";
        uiController.combo.text = "0";
    }

    void Update()
    {
        
    }
}
