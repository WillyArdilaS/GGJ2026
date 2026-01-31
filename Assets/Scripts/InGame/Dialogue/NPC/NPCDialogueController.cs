using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCDialogueController : AbstractDialogueController
{
    // === Phase 1 Validation ===
    private bool hasRecordedInteraction = false;

    // === NPC Sprite ===
    public enum PortraitSide { Left, Right }
    [SerializeField] private PortraitSide portraitSide;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        GameManager.instance.PhaseManager.Phase1Completed += () => currentDialogueIndex++;
    }

    void Update()
    {
        if (GameManager.instance.State != GameManager.GameState.ShowingDialogue) spriteRend.enabled = true;
    }

    // === Overridden Abstract Methods ===
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        NPCDialogueData npcDialogueData = dialogueData as NPCDialogueData;
        GameManager.instance.DialogueManager.StartNpcDialogue(this, portraitSide, npcDialogueData, currentDialogueIndex);

        spriteRend.enabled = false;
    }

    public override void UpdateDialogueIndex()
    {
        if (currentDialogueIndex == lastIndexPhase1 && !hasRecordedInteraction) RecordInteraction();

        if (GameManager.instance.PhaseManager.Phase == PhaseManager.CurrentPhase.Phase1 && currentDialogueIndex < lastIndexPhase1)
        {
            currentDialogueIndex++;
        }
        else if (GameManager.instance.PhaseManager.Phase == PhaseManager.CurrentPhase.Phase2 && currentDialogueIndex < dialogueData.Dialogues.Length - 1)
        {
            currentDialogueIndex++;
        }
    }

    private void RecordInteraction()
    {
        GameManager.instance.PhaseManager.UpdateInteractionsPhase1();
        hasRecordedInteraction = true;
    }
}