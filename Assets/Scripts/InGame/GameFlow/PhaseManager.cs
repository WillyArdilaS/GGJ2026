using System;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // === Phases ===
    public enum Phase { Phase1, Phase2, FinalPhase }
    [SerializeField] private Phase currentPhase = Phase.Phase1;

    // === Phase 1 Management ===
    [Header("Phase 1")]
    [SerializeField] private int currentNPCInteractions = 0;
    [SerializeField] private int totalNPCInteractions;

    // === Events ===
    public event Action Phase1Completed;
    public event Action Phase2Completed;

    // === Properties ===
    public Phase CurrentPhase { get => currentPhase; set => currentPhase = value; }

    void Awake()
    {
        GameManager.instance.DialogueManager.YesButtonPressed += StartFinalPhase;
    }

    public void UpdateInteractionsPhase1()
    {
        currentNPCInteractions++;
        if (currentNPCInteractions >= totalNPCInteractions) StartPhase2();
    }

    private void StartPhase2()
    {
        Phase1Completed?.Invoke();
        currentPhase = Phase.Phase2;
    }

    private void StartFinalPhase()
    {
        Phase2Completed?.Invoke();
        currentPhase = Phase.FinalPhase;
    }
}