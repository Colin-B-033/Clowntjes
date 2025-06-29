using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingFixer : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DynamicGI.UpdateEnvironment(); // Forces a lighting environment refresh
    }
}
