using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour { 

    [Header("Game Over!!")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Menu")]
    [SerializeField] private GameObject menuScreen;

    [Header("About")]
    [SerializeField] private GameObject aboutScreen;

    [Header("Complete Level")]
    [SerializeField] private GameObject completeScreen;

    [SerializeField] private GameObject player;



    private bool jeKoniec;
    private void Start()
    {
        menuScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        aboutScreen.SetActive(false);
        completeScreen.SetActive(false);
        jeKoniec = false;
    }

    private void Update()
    {
        if (!jeKoniec)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (pauseScreen.activeInHierarchy)
                    PauseGame(false);
                else
                    PauseGame(true);
            }
        }
        else
        {
            completeScreen.SetActive(true);
            player.SetActive(false);
        }
    }
    public void Gameover()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseGame(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        PauseGame(false);
    }

    public void Quit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }


    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    
    public void Resume()
    {
        PauseGame(false);
    }


    public void About()
    {
        aboutScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void Back()
    {
        aboutScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
        jeKoniec = false;
    }

    public void setKoniec()
    {
        jeKoniec = true;
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
