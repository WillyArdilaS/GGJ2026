using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCDialogueController : MonoBehaviour
{
    // === Dialogue Data ===
    [SerializeField] private NPCDialogueData npcDialogueData;

    // === Dialogue Index ===
    [SerializeField, Tooltip("Index of the last dialogue available in phase 1")] private int lastIndexPhase1;
    private int currentDialogueIndex = 0;

    // === NPC Sprite ===
    public enum PortraitSide { Left, Right }
    [SerializeField] private PortraitSide portraitSide;
    private SpriteRenderer spriteRend;

    // === Properties ===
    public int CurrentDialogueIndex => currentDialogueIndex;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.instance.State != GameManager.GameState.ShowingDialogue) spriteRend.enabled = true;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        if (GameManager.instance.DialogueManager.StartNpcDialogue(portraitSide, npcDialogueData, currentDialogueIndex))
        {
            spriteRend.enabled = false;
            UpdateDialogueIndex();
        }
    }

    private void UpdateDialogueIndex()
    {
        if (GameManager.instance.Phase == GameManager.CurrentPhase.Phase1 && currentDialogueIndex < lastIndexPhase1)
        {
            currentDialogueIndex++;
        }
        else if (GameManager.instance.Phase == GameManager.CurrentPhase.Phase2 && currentDialogueIndex < npcDialogueData.Dialogues.Length - 1)
        {
            currentDialogueIndex++;
        }
    }
}