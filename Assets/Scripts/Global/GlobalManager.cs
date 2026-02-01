using UnityEngine;

[DefaultExecutionOrder(-2)]
public class GlobalManager : MonoBehaviour
{
    // === Singleton ===
    public static GlobalManager instance;

    // === Managers ===
    private LanguageManager languageManager;

    // === Properties ===
    public LanguageManager LanguageManager => languageManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        if (languageManager == null) languageManager = GetComponentInChildren<LanguageManager>();
    }
}