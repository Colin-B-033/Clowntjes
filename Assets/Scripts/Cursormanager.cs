using UnityEngine;

public class FocusFixer : MonoBehaviour
{
    private static FocusFixer instance;
    
    void Start()
    {
        // Force focus back to the game window (may help in editor/testing)
        Application.focusChanged += OnFocusChanged;
    }

    void OnDestroy()
    {
        Application.focusChanged -= OnFocusChanged;
    }

    void OnFocusChanged(bool hasFocus)
    {
        if (!hasFocus)
        {
            // Log focus lost and optionally take other actions
            Debug.Log("Focus lost");
            // You cannot directly set Application.isFocused as it is read-only.
            // Consider using platform-specific APIs or user interaction to regain focus.
        }
    }
}
