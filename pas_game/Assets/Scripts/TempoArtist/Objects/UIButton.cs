using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TempoArtist.Managers;

namespace TempoArtist.Objects
{
    public class UIButton : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            UIManager.Instance.LoadScene(sceneName);
        }

        public bool IsClicked()
        {
            return true;
        }
    }
}

