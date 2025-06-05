using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FovSliderDisplay : MonoBehaviour
{
    public Slider fovSlider;
    public TextMeshProUGUI fovValueText;
    public Camera mainCamera;

    private const string fovKey = "FOV"; // Sleutel voor PlayerPrefs

    private void Start()
    {
        // Haal opgeslagen FOV op of gebruik standaardwaarde
        float savedFov = PlayerPrefs.GetFloat(fovKey, 90f);
        fovSlider.value = savedFov;
        UpdateFovValue(savedFov);

        fovSlider.onValueChanged.AddListener(UpdateFovValue);
    }

    private void UpdateFovValue(float value)
    {
        int fov = Mathf.RoundToInt(value);
        fovValueText.text = fov + "° FOV";

        // Pas de FOV van de camera aan
        if (mainCamera != null)
            mainCamera.fieldOfView = fov;

        // Sla de FOV op
        PlayerPrefs.SetFloat(fovKey, value);
        PlayerPrefs.Save();
    }
}
