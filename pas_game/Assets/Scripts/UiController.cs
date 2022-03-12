using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    private Button playButton;
    private Button optionsButton;
    public Text score;
    public Text combo;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
