using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Truck;
    public GameObject mainButtons;
    public GameObject Options;
    public GameObject LevelSelect;
    public Canvas PlayerUI;

    private static bool isPaused = false;
    public static bool IsPaused => isPaused;

    private bool pendingCursorLock = false;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        pauseMenu.SetActive(false);
        Options.SetActive(false);
        LevelSelect.SetActive(false);
        mainButtons.SetActive(false); // Hide main buttons at start

        if (Truck != null)
            Truck.SetActive(false);

        PlayerUI.enabled = true;

        UpdateCursorState();
    }
    void Update()
    {
        // Toggle submenus
        if ((Options.activeSelf || LevelSelect.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
        {
            if (Options.activeSelf) CloseOptions();
            else if (LevelSelect.activeSelf) CloseLevelSelect();
            return;
        }

        // Toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        FinishLine finishLine = FindObjectOfType<FinishLine>();
        if (!isPaused && Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0) && finishLine != null && finishLine.isFinished == false)
        {
            LockCursor();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (Truck != null)
            Truck.SetActive(isPaused);

        if (isPaused)
        {
            Options.SetActive(false);
            LevelSelect.SetActive(false);
            PlayerUI.enabled = false;
            mainButtons.SetActive(true); // Show main buttons when paused
        }
        else
        {
            PlayerUI.enabled = true;
            mainButtons.SetActive(false); // Hide main buttons when unpaused
        }

        Time.timeScale = isPaused ? 0 : 1;
        UpdateCursorState();
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        if (Truck != null)
            Truck.SetActive(false);

        PlayerUI.enabled = true;
        Time.timeScale = 1;

        if (!Application.isFocused)
        {
            pendingCursorLock = true; // wait for focus
        }
        else
        {
            StartCoroutine(DelayedCursorLock()); // lock after UI deactivates
        }
    }
    private IEnumerator DelayedCursorLock()
    {
        yield return null; // wait one frame
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Cursor locked manually.");
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Cursor unlocked.");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && pendingCursorLock)
        {
            LockCursor();
            pendingCursorLock = false;
        }
    }

    public void OpenOptions()
    {
        Options.SetActive(true);
        mainButtons.SetActive(false);
        UpdateCursorState();
    }
    public void CloseOptions()
    {
        Options.SetActive(false);
        mainButtons.SetActive(true);
        
        UpdateCursorState();
    }

    public void OpenLevelSelect()
    {
        LevelSelect.SetActive(true);
        mainButtons.SetActive(false);   
        UpdateCursorState();
    }

    public void CloseLevelSelect()
    {
        LevelSelect.SetActive(false);
        mainButtons.SetActive(true);
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
        TogglePause();
    }

    private void UpdateCursorState()
    {
        if (!isPaused)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }
}
