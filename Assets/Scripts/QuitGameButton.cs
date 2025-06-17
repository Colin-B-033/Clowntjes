using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    private void Start()
    {
        Button btn = GetComponent<Button>();
        Debug.Log("QuitGameButton Start called");
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(QuitGame);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game called");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
