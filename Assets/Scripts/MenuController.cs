using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Options;
    public GameObject LevelSelect;
    public Canvas PlayerUI;

    private static bool isPaused = false;
    public static bool IsPaused => isPaused;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        pauseMenu.SetActive(false);
        Options.SetActive(false);
        LevelSelect.SetActive(false);
        PlayerUI.enabled = true;
        UpdateCursorState();
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
            PlayerUI.enabled = false;
        }
        else
        {
            PlayerUI.enabled = true;
        }
        Time.timeScale = isPaused ? 0 : 1;
        UpdateCursorState();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        PlayerUI.enabled = true;
        Time.timeScale = 1;
        StartCoroutine(DelayedCursorLock());
    }

    private IEnumerator DelayedCursorLock()
    {
        yield return null; // Wait one frame
        UpdateCursorState();
    }


    public void OpenOptions()
    {
        Options.SetActive(true);
        pauseMenu.SetActive(false);
        UpdateCursorState();
    }

    public void CloseOptions()
    {
        Options.SetActive(false);
        pauseMenu.SetActive(true);
        UpdateCursorState();
    }

    public void OpenLevelSelect()
    {
        LevelSelect.SetActive(true);
        pauseMenu.SetActive(false);
        UpdateCursorState();
    }

    public void CloseLevelSelect()
    {
        LevelSelect.SetActive(false);
        pauseMenu.SetActive(true);
        UpdateCursorState();
    }

    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    private void UpdateCursorState()
    {
        // Always lock and hide the cursor when not paused and no submenus are open
        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Cursor locked and hidden: " + Cursor.lockState + ", visible: " + Cursor.visible);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Cursor unlocked and visible: " + Cursor.lockState + ", visible: " + Cursor.visible);
        }
    }
}
