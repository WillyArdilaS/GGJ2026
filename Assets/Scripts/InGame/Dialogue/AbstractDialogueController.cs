using UnityEngine;

public abstract class AbstractDialogueController : MonoBehaviour
{
    // === Dialogue Data ===
    [SerializeField] protected DialogueData dialogueData;

    // === Dialogue Index ===
    [SerializeField] protected int currentDialogueIndex = 0;
    [SerializeField, Tooltip("Index of the last dialogue available in phase 1. Only necessary if it depends on the phase")] protected int lastIndexPhase1;

    // === Properties ===
    public int LastIndexPhase1 => lastIndexPhase1;

    // === Abstract Methods ===
    public abstract void UpdateDialogueIndex();

    protected virtual void Awake()
    {
        GameManager.instance.PhaseManager.Phase1Completed += () => currentDialogueIndex++;
    }

    protected virtual void OnMouseDown()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;
    }
}