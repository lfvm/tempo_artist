using TempoArtist.Managers;
using UnityEngine;

namespace TempoArtist.Objects
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;

        [SerializeField] private GameObject pauseMenuUI;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (gameIsPaused)
            {
                Resume();
            }
            Pause();
        }

        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
        

        public void ExitToSongSelect()
        {   
            Resume();
            UIManager.Instance.LoadScene("SongSelect");
        }
    }
}
