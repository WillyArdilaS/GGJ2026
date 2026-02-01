using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public enum Language { English, Spanish }
    [SerializeField] private Language currentLanguage = Language.English;

    // === Properties ===
    public Language CurrentLanguage { get => currentLanguage; set => currentLanguage = value; }
}