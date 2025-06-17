using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSlimiter : MonoBehaviour
{
    public void ApplyGraphicsSettings(bool vsyncEnabled, int targetFps)
    {
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;

        if (vsyncEnabled)
        {
            Application.targetFrameRate = -1; // Let VSync control it
        }
        else
        {
            Application.targetFrameRate = targetFps; // Use your dropdown setting
        }
    }

}
