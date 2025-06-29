using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timescalefixer : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null; // Waits 1 frame

        LateStart();
    }
    void LateStart()
    {
        Time.timeScale = 1f; // Resume game speed
    }

}
