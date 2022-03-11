using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;

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
