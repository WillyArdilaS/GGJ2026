using UnityEngine;

public class AccusedNameRevealer : MonoBehaviour
{
    // === Manager ===
    [SerializeField] private FinalPhaseManager finalPhaseManager;

    // === Dialogue Datas ===
    [SerializeField] private NPCDialogueData dialogueDataEN;
    [SerializeField] private NPCDialogueData dialogueDataES;

    void OnMouseDown()
    {
        if(GameManager.instance.CurrentGameState != GameManager.GameState.Playing) return;
        
        switch (GlobalManager.instance.LanguageManager.CurrentLanguage)
        {
            case LanguageManager.Language.English:
                finalPhaseManager.MakeFinalAccusation($"{dialogueDataEN.Name.ToUpper()}!", dialogueDataEN.TypingSpeed);
                break;
            case LanguageManager.Language.Spanish:
                finalPhaseManager.MakeFinalAccusation($"¡{dialogueDataES.Name.ToUpper()}!", dialogueDataES.TypingSpeed);
                break;
            default:
                finalPhaseManager.MakeFinalAccusation($"¡{dialogueDataES.Name.ToUpper()}!", dialogueDataES.TypingSpeed);
                break;
        }
    }
}