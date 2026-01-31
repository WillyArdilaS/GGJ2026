using UnityEngine;

public class PlayerDialogueController : AbstractDialogueController
{
    // === Dialogue Index ===
    [SerializeField] private bool dependsOnPhase;

    // === Overridden Abstract Methods ===
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        PlayerDialogueData playerDialogueData = dialogueData as PlayerDialogueData;
        GameManager.instance.DialogueManager.StartPlayerDialogue(this, playerDialogueData, currentDialogueIndex);
    }

    public override void UpdateDialogueIndex()
    {
        if (dependsOnPhase)
        {
            if (GameManager.instance.PhaseManager.Phase == PhaseManager.CurrentPhase.Phase1 && currentDialogueIndex < lastIndexPhase1)
            {
                currentDialogueIndex++;
            }
            else if (GameManager.instance.PhaseManager.Phase == PhaseManager.CurrentPhase.Phase2 && currentDialogueIndex < dialogueData.Dialogues.Length - 1)
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