using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UILanguageAdapter : MonoBehaviour
{
    // === UI Element ===
    private TextMeshProUGUI textElement;

    // === Texts ===
    [SerializeField] private string textEN;
    [SerializeField] private string textES;

    void Awake()
    {
        textElement = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textElement.text = GlobalManager.instance.LanguageManager.CurrentLanguage switch
        {
            LanguageManager.Language.English => textEN,
            LanguageManager.Language.Spanish => textES,
            _ => textES
        };
    }
}