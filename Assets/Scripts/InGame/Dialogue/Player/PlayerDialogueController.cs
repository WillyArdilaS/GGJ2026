using UnityEngine;

public class PlayerDialogueController : AbstractDialogueController
{
    [SerializeField] private bool isBasedOnPhase;

    void Awake()
    {
        if (isBasedOnPhase) GameManager.instance.PhaseManager.Phase1Completed += () => currentDialogueIndex++;
    }

    // === Overridden Abstract Methods ===
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        PlayerDialogueData playerDialogueData = dialogueData as PlayerDialogueData;
        GameManager.instance.DialogueManager.StartPlayerDialogue(this, playerDialogueData, currentDialogueIndex);
    }

    public override void UpdateDialogueIndex()
    {
        if (isBasedOnPhase)
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