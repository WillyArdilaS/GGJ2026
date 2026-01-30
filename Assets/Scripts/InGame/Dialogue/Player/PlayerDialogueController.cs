using UnityEngine;

public class PlayerDialogueController : MonoBehaviour
{
    // === Dialogue Data ===
    [SerializeField] private PlayerDialogueData dialogueData;

    // === Dialogue Index ===
    [SerializeField] private bool dependsOnPhase;
    [SerializeField, Tooltip("Index of the last dialogue available in phase 1. Only necessary if it depends on the phase")] private int lastIndexPhase1;
    private int currentDialogueIndex = 0;

    // === Properties ===
    public int CurrentDialogueIndex => currentDialogueIndex;

    void OnMouseDown()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        if (GameManager.instance.DialogueManager.StartPlayerDialogue(dialogueData, currentDialogueIndex)) UpdateDialogueIndex();
    }

    private void UpdateDialogueIndex()
    {
        if (dependsOnPhase)
        {
            if (GameManager.instance.Phase == GameManager.CurrentPhase.Phase1 && currentDialogueIndex < lastIndexPhase1)
            {
                currentDialogueIndex++;
            }
            else if (GameManager.instance.Phase == GameManager.CurrentPhase.Phase2 && currentDialogueIndex < dialogueData.Dialogues.Length - 1)
            {
                currentDialogueIndex++;
            }
        }
        else
        {
            if (currentDialogueIndex < dialogueData.Dialogues.Length - 1) currentDialogueIndex++;
        }
    }
}