using System;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // === Phases ===
    public enum CurrentPhase { Phase1, Phase2, FinalPhase }
    [SerializeField] private CurrentPhase currentPhase = CurrentPhase.Phase1;

    // === Phase 1 Management ===
    [Header("Phase 1")]
    [SerializeField] private int currentNPCInteractions = 0;
    [SerializeField] private int totalNPCInteractions;

    // === Events ===
    public event Action Phase1Completed;
    public event Action Phase2Completed;

    // === Properties ===
    public CurrentPhase Phase { get => currentPhase; set => currentPhase = value; }

    public void UpdateInteractionsPhase1()
    {
        currentNPCInteractions++;
        if (currentNPCInteractions >= totalNPCInteractions) StartPhase2();
    }

    private void StartPhase2()
    {
        Phase1Completed?.Invoke();
        currentPhase = CurrentPhase.Phase2;
    }
}