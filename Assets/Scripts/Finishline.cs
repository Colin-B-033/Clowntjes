using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public GameObject finishMenuUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText; // Add this in your UI and assign in Inspector
    public bool isFinished = false;

    private string HighscoreKey => $"Highscore_{SceneManager.GetActiveScene().name}";

    private void Start()
    {
        isFinished = false;
        if (finishMenuUI != null)
            finishMenuUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFinished)
        {
            LevelTimer.Instance.StopTimer();

            float finalTime = LevelTimer.Instance.GetTime();
            int score = CalculateScore(finalTime);

            timeText.text = $"Time: {finalTime:F2} seconds";
            scoreText.text = $"Score: {score}";

            // Highscore logic
            int highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt(HighscoreKey, highscore);
                PlayerPrefs.Save();
            }
            if (highscoreText != null)
                highscoreText.text = $"Highscore: {highscore}";

            finishMenuUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isFinished = true;
            UIBlocker.IsBlockingUIOpen = true;
        }
    }

    private int CalculateScore(float time)
    {
        float baseScore = 1000f;
        float timePenalty = time * 10f;
        int finalScore = Mathf.Max(0, Mathf.RoundToInt(baseScore - timePenalty));
        return finalScore;
    }
}
