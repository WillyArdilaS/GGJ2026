using UnityEngine;

public class GameManager : MonoBehaviour
{
    // === Singleton ===
    public static GameManager instance;

    // === Managers ===
    private DialogueManager dialogueManager;

    // === Game Flow ===
    public enum GameState { Playing, ShowingAnimation, ShowingDialogue, InPause }
    public enum CurrentPhase { Phase1, Phase2, FinalDecision }
    [SerializeField] private GameState gameState = GameState.Playing;
    [SerializeField] private CurrentPhase currentPhase = CurrentPhase.Phase1;

    // === Properties ===
    public DialogueManager DialogueManager => dialogueManager;
    public GameState State { get => gameState; set => gameState = value; }
    public CurrentPhase Phase { get => currentPhase; set => currentPhase = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        if (dialogueManager == null) dialogueManager = GetComponentInChildren<DialogueManager>();
    }
}
