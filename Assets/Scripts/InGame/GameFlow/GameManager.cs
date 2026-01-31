using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    // === Singleton ===
    public static GameManager instance;

    // === Managers ===
    private PhaseManager phaseManager;
    private DialogueManager dialogueManager;

    // === States ===
    public enum GameState { Playing, ShowingAnimation, ShowingDialogue, ShowingClues, InPause }
    [SerializeField] private GameState gameState = GameState.Playing;

    // === Properties ===
    public PhaseManager PhaseManager => phaseManager;
    public DialogueManager DialogueManager => dialogueManager;
    public GameState State { get => gameState; set => gameState = value; }

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
        if (phaseManager == null) phaseManager = GetComponentInChildren<PhaseManager>();
        if (dialogueManager == null) dialogueManager = GetComponentInChildren<DialogueManager>();
    }
}
