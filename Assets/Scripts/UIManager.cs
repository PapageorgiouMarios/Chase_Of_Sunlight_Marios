using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseScreen.activeInHierarchy) 
            {
                PauseGame(false);
            }
            else 
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over Screen
    public void GameOver() 
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
    #endregion

    #region Pause Game
    public void PauseGame(bool status) 
    {
        pauseScreen.SetActive(status);
        if(status) 
        {
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    public void SoundVolume() 
    {

    }

    public void MusicVolume()
    {

    }

    #endregion

}
