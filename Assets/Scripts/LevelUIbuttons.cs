using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class LevelUIButtons : MonoBehaviour
{
    public GameObject finishMenuUI;

    private void Awake()
    {
        // Don't cache finishMenuUI here if this object persists across scenes!
    }

    private void OnEnable()
    {
        // Always reacquire the reference after scene load
        FinishLine finishLine = FindObjectOfType<FinishLine>();
        if (finishLine != null)
            finishMenuUI = finishLine.finishMenuUI;
    }

    public void LoadNextScene()
    {
        // Reacquire reference in case scene changed
        if (finishMenuUI == null)
        {
            FinishLine finishLine = FindObjectOfType<FinishLine>();
            if (finishLine != null)
                finishMenuUI = finishLine.finishMenuUI;
        }

        if (finishMenuUI != null)
            finishMenuUI.SetActive(false);

        Time.timeScale = 1f;
        UIBlocker.IsBlockingUIOpen = false;
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
