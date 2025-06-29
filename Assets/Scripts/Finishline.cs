using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
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
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isFinished = true;
            UIBlocker.IsBlockingUIOpen = true; // Block other UI
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
