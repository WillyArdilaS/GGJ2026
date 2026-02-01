using UnityEngine;

public class PlayerDialogueController : AbstractDialogueController
{
    [SerializeField] private bool isBasedOnPhase;

    void Awake()
    {
        if (isBasedOnPhase) GameManager.instance.PhaseManager.Phase1Completed += () => currentDialogueIndex++;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.Playing) return;

        // Select the dialogue data according to the current language
        PlayerDialogueData playerDialogueData = GlobalManager.instance.LanguageManager.CurrentLanguage switch 
        {
            LanguageManager.Language.English => dialogueDataEN as PlayerDialogueData,
            LanguageManager.Language.Spanish => dialogueDataES as PlayerDialogueData,
            _ => dialogueDataES as PlayerDialogueData
        };

        GameManager.instance.DialogueManager.StartPlayerDialogue(this, playerDialogueData, currentDialogueIndex);
    }

    // === Overridden Abstract Methods ===
    public override void UpdateDialogueIndex()
    {
        if (isBasedOnPhase)
        {
            if (GameManager.instance.PhaseManager.CurrentPhase == PhaseManager.Phase.Phase1 && currentDialogueIndex < lastIndexPhase1)
            {
                currentDialogueIndex++;
            }
            else if (GameManager.instance.PhaseManager.CurrentPhase == PhaseManager.Phase.Phase2 && currentDialogueIndex < dialogueDataEN.Dialogues.Length - 1)
            {
                currentDialogueIndex++;
            }
        }
        else
        {
            if (currentDialogueIndex < dialogueDataEN.Dialogues.Length - 1) currentDialogueIndex++;
        }
    }
}