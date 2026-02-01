
using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    [SerializeField] private bool hasAChoice;
    [SerializeField, TextArea(2, 5)] private string[] lines;

    // === Properties ===
    public bool HasAChoice => hasAChoice;
    public string[] Lines => lines;
}

public class DialogueData : ScriptableObject
{
    // === Data Fields ===
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private Dialogue[] dialogues;

    // === Properties ===
    public float TypingSpeed => typingSpeed;
    public Dialogue[] Dialogues => dialogues;
}