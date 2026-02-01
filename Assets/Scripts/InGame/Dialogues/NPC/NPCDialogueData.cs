using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "Scriptable Objects/NPC Dialogues")]
public class NPCDialogueData : DialogueData
{
    // === Data Fields ===
    [SerializeField] private string npcName;
    [SerializeField] private Sprite npcPortraitPhase1;
    [SerializeField] private Sprite npcPortraitPhase2;

    // === Properties ===
    public string Name => npcName;
    public Sprite PortraitPhase1 => npcPortraitPhase1;
    public Sprite PortraitPhase2 => npcPortraitPhase2;
}