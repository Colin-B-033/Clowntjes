using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;
    private float timeElapsed;
    private bool isRunning = true;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isRunning)
            timeElapsed += Time.deltaTime;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTime()
    {
        return timeElapsed;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
