using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCDialogueController : AbstractDialogueController
{
    // === NPC Sprite ===
    public enum PortraitSide { Left, Right }
    [SerializeField] private PortraitSide portraitSide;
    private SpriteRenderer spriteRend;

    // === Overridden Abstract Methods ===
    protected override void Awake()
    {
        base.Awake();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        NPCDialogueData npcDialogueData = dialogueData as NPCDialogueData;
        GameManager.instance.DialogueManager.StartNpcDialogue(this, portraitSide, npcDialogueData, currentDialogueIndex);

        spriteRend.enabled = false;
    }

    public override void UpdateDialogueIndex()
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

    void Update()
    {
        if (GameManager.instance.State != GameManager.GameState.ShowingDialogue) spriteRend.enabled = true;
    }
}