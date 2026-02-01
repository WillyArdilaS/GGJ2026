using UnityEngine;

public abstract class AbstractDialogueController : MonoBehaviour
{
    // === Dialogue Datas ===
    [Header("Datas")]
    [SerializeField] protected DialogueData dialogueDataEN;
    [SerializeField] protected DialogueData dialogueDataES;

    // === Dialogue Index ===
    [Header("Index")]
    [SerializeField] protected int currentDialogueIndex = 0;
    [SerializeField, Tooltip("Index of the last dialogue available in phase 1. Only necessary if it depends on the phase")] protected int lastIndexPhase1;

    // === Abstract Methods ===
    public abstract void UpdateDialogueIndex();
}