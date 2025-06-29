using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class LevelUIButtons : MonoBehaviour
{
    // Optionally assign in Inspector, or find at runtime
    public GameObject finishMenuUI;

    // Call this from your "Next Level" button
    public void LoadNextScene()
    {
        // Try to find the FinishLine if not assigned
        if (finishMenuUI == null)
        {
            FinishLine finishLine = FindObjectOfType<FinishLine>();
            if (finishLine != null)
                finishMenuUI = finishLine.finishMenuUI;
        }

        // Hide finish menu UI if open
        if (finishMenuUI != null)
            finishMenuUI.SetActive(false);

        // Reset time scale and UI blocker
        Time.timeScale = 1f;
        UIBlocker.IsBlockingUIOpen = false;

        // Optionally reset cursor state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            Debug.LogWarning("No next scene in build settings!");
    }

    // Call this from your "Retry" button
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
