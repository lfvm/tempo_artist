using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    private Button playButton;
    private Button optionsButton;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
