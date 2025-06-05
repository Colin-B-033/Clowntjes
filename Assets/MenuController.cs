using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // ✅ Toegevoegd
using static UnityEditor.Progress;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Options;
    public GameObject LevelSelect;

    private bool isPaused = false;

    private void Awake()
    {
        // ✅ FPS limiter
        QualitySettings.vSyncCount = 0; // Zet VSync uit
        Application.targetFrameRate = 60; // Beperk framerate tot 60

        pauseMenu.SetActive(false);
        Options.SetActive(false);
        LevelSelect.SetActive(false);
    }

    void Update()
    {
        if ((Options.activeSelf || LevelSelect.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
        {
            if (Options.activeSelf) CloseOptions();
            else if (LevelSelect.activeSelf) CloseLevelSelect();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        if (isPaused)
        {
            Options.SetActive(false);
            LevelSelect.SetActive(false);
        }
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenOptions()
    {
        Options.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseOptions()
    {
        Options.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        LevelSelect.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        LevelSelect.SetActive(false);
        pauseMenu.SetActive(true);
    }

    // ✅ Scene laden op naam
    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    // ✅ Scene laden op index
    public void LoadSceneByIndex(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
}
