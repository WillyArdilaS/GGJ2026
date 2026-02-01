using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCDialogueController : AbstractDialogueController
{
    // === Phase 1 Validation ===
    private bool hasRecordedInteraction = false;

    // === NPC Portrait ===
    public enum PortraitSide { Left, Center, Right }
    [Header("Portrait")]
    [SerializeField] private PortraitSide currentPortraitSide;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        GameManager.instance.PhaseManager.Phase1Completed += () => currentDialogueIndex++;
    }

    void Update()
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.ShowingDialogue) spriteRend.enabled = true;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.Playing) return;

        // Select the dialog data according to the current language
        NPCDialogueData npcDialogueData = GlobalManager.instance.LanguageManager.CurrentLanguage switch
        {
            LanguageManager.Language.English => dialogueDataEN as NPCDialogueData,
            LanguageManager.Language.Spanish => dialogueDataES as NPCDialogueData,
            _ => dialogueDataES as NPCDialogueData
        };
        
        GameManager.instance.DialogueManager.StartNpcDialogue(this, currentPortraitSide, npcDialogueData, currentDialogueIndex);

        spriteRend.enabled = false;
    }

    // === Overridden Abstract Methods ===
    public override void UpdateDialogueIndex()
    {
        if (currentDialogueIndex == lastIndexPhase1 && !hasRecordedInteraction) RecordInteraction();

        if (GameManager.instance.PhaseManager.CurrentPhase == PhaseManager.Phase.Phase1 && currentDialogueIndex < lastIndexPhase1)
        {
            currentDialogueIndex++;
        }
        else if (GameManager.instance.PhaseManager.CurrentPhase == PhaseManager.Phase.Phase2 && currentDialogueIndex < dialogueDataEN.Dialogues.Length - 1)
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