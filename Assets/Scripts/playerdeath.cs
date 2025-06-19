using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerdeath : MonoBehaviour
{
    public static bool IsRetryMenuActive { get; private set; }
    public GameObject retryMenuUI; // Assign your retry panel in the inspector
    public Button retryButton;      // Assign your retry button in the inspector

    [Header("Audio")]
    public AudioClip deathSound;    // Assign your death sound in the inspector
    private AudioSource audioSource;

    private bool isDead = false;

    private void Start()
    {
        if (retryMenuUI != null)
            retryMenuUI.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryLevel);

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Terrain"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (retryMenuUI != null)
            retryMenuUI.SetActive(true);

        IsRetryMenuActive = true;

        // Play death sound
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        IsRetryMenuActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
