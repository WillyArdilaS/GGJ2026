using UnityEngine;

public abstract class AbstractDialogueController : MonoBehaviour
{
    // === Dialogue Data ===
    [SerializeField] protected DialogueData dialogueData;

    // === Dialogue Index ===
    [SerializeField] protected int currentDialogueIndex = 0;
    [SerializeField, Tooltip("Index of the last dialogue available in phase 1. Only necessary if it depends on the phase")] protected int lastIndexPhase1;

    // === Abstract Methods ===
    public abstract void UpdateDialogueIndex();
}