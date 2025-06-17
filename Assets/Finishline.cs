using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject finishMenuUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public bool isFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelTimer.Instance.StopTimer();

            float finalTime = LevelTimer.Instance.GetTime();
            int score = CalculateScore(finalTime);

            timeText.text = $"Time: {finalTime:F2} seconds";
            scoreText.text = $"Score: {score}";

            finishMenuUI.SetActive(true);
            Time.timeScale = 0f; // Optional: Pause game
            Cursor.lockState = CursorLockMode.None; // Unlock cursor when finishing the level
            Cursor.visible = true; // Make cursor visible
            isFinished = true;
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
