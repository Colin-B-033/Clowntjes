using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Needed for Button

public class extrauifunctions : MonoBehaviour
{
    public Button tmproButton; // Reference to a UI Button (can have TMP_Text as label)
    public Image Instructions;
    public Image Menu;
    public GameObject panel;
    void Start()
    {
        if (tmproButton != null)
        {
            tmproButton.onClick.AddListener(OnTMPROButtonClicked);
        }
    }

    public void OnTMPROButtonClicked()
    {
        Menu.gameObject.SetActive(false); // Hide the Menu canvas GameObject
        Instructions.gameObject.SetActive(true); // Show the Instructions canvas GameObject
    }
}
