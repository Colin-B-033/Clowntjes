using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class FPSscript : MonoBehaviour
{
    public TMP_Dropdown fpsDropdown;
    public Toggle vsyncToggle;
    private readonly int defaultIndex = 1;

    // Matches the dropdown options order
    private readonly List<int> fpsOptions = new List<int> { 30, 60, 120, 144, 240, -1 };

    void Start()
    {
        fpsDropdown.value = defaultIndex;

        // Optional: force update UI visually (sometimes needed)
        fpsDropdown.RefreshShownValue();

        ApplyGraphicsSettings();
    }

    public void ApplyGraphicsSettings()
    {
        bool vsyncEnabled = vsyncToggle != null && vsyncToggle.isOn;
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;

        if (vsyncEnabled)
        {
            Application.targetFrameRate = 0;
        }
        else
        {
            int selectedIndex = fpsDropdown.value;
            Application.targetFrameRate = fpsOptions[selectedIndex];
        }

        Debug.Log($"VSync: {vsyncEnabled}, FPS Cap: {Application.targetFrameRate}");
    }
}
