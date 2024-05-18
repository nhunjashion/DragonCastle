using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;



    private void Awake()
    {
        Time.timeScale = 1;
    }

    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = false;
    }

    public void Home()
    {
        // SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }



    public void Quit()
    {
        Application.Quit();
    }
}
