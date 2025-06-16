using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsSliderDisplay : MonoBehaviour
{
    public Slider fpsSlider;
    public TextMeshProUGUI fpsValueText;

    private const string FpsPrefKey = "PreferredFPS";

    void Start()
    {
        // FPS laden uit PlayerPrefs (standaard 60 als niks is opgeslagen)
        int savedFps = PlayerPrefs.GetInt(FpsPrefKey, 60);
        fpsSlider.value = savedFps;

        SetFrameRate(savedFps);

        // Koppel event
        fpsSlider.onValueChanged.AddListener(SetFrameRate);
    }

    void SetFrameRate(float value)
    {
        int fps = Mathf.RoundToInt(value);
        fpsValueText.text = fps + " FPS";
        Application.targetFrameRate = fps;

        // FPS opslaan in PlayerPrefs
        PlayerPrefs.SetInt(FpsPrefKey, fps);
        PlayerPrefs.Save(); // Opslaan naar disk
    }
}
