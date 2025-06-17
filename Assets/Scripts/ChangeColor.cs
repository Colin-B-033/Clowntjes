using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Button button; // Assign in Inspector
    public Image image;   // Assign in Inspector

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(ChangeImageColor);
        }
    }

    private void ChangeImageColor()
    {
        if (image != null)
        {
            image.color = new Color(Random.value, Random.value, Random.value);
        }
    }

}
