using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fpsshower : MonoBehaviour
{
    public TMPro.TMP_Text fpsText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsText.text = "FPS: " + (1.0f / Time.deltaTime).ToString("F2");
    }
}
