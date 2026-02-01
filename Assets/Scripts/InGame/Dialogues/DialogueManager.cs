using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // === UI Elements ===
    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject portrait;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject choiceButtons;
    private TextMeshProUGUI nameText;

    // === Portrait ===
    [Header("Portrait Position")]
    [SerializeField] private float leftPortraitXPos;
    [SerializeField] private float centerPortraitXPos;
    [SerializeField] private float rightPortraitXPos;
    private Image portraitImg;
    private RectTransform portraitRectTransform;

    // === Dialogue ===
    private AbstractDialogueController currentDialogueController;
    private DialogueData currentDialogueData;
    private int dialogueIndex = -1;
    private int lineIndex = -1;
    private bool isTyping = false;

    // === Coroutines ===
    private Coroutine typeLineRoutine;

    // === Events ===
    public event Action YesButtonPressed;
    public event Action NoButtonPressed;

    void Awake()
    {
        nameText = nameBox.GetComponentInChildren<TextMeshProUGUI>();
        portraitImg = portrait.GetComponent<Image>();
        portraitRectTransform = portrait.GetComponent<RectTransform>();
    }

    // === Dialogue Methods ===
    public void StartPlayerDialogue(AbstractDialogueController dialogueController, PlayerDialogueData dialogueData, int dialogueIndex)
    {
        if (dialogueData.Dialogues.Length == 0 || dialogueIndex >= dialogueData.Dialogues.Length) return;
        if (dialogueData.Dialogues[dialogueIndex].Lines.Length == 0) return;

        // Initialize global dialogue variables
        currentDialogueController = dialogueController;
        currentDialogueData = dialogueData;
        this.dialogueIndex = dialogueIndex;
        lineIndex = 0;

        // Display dialogue data in UI        
        portrait.SetActive(false);
        nameBox.SetActive(false);

        // If the dialog has a choice and only one line, display the choice buttons
        ShowButtons(currentDialogueData.Dialogues[dialogueIndex].HasAChoice && dialogueData.Dialogues[dialogueIndex].Lines.Length == 1);

        dialoguePanel.SetActive(true);

        StartTypingAnimation();
        GameManager.instance.CurrentGameState = GameManager.GameState.ShowingDialogue;
    }

    public void StartNpcDialogue(AbstractDialogueController dialogueController, NPCDialogueController.PortraitSide portraitSide, NPCDialogueData dialogueData,
    int dialogueIndex)
    {
        if (dialogueData.Dialogues.Length == 0 || dialogueIndex >= dialogueData.Dialogues.Length) return;
        if (dialogueData.Dialogues[dialogueIndex].Lines.Length == 0) return;

        // Initialize global dialogue variables
        currentDialogueController = dialogueController;
        currentDialogueData = dialogueData;
        this.dialogueIndex = dialogueIndex;
        lineIndex = 0;

        // Display dialogue data in UI
        Vector2 portraitPosition = portraitSide switch
        {
            NPCDialogueController.PortraitSide.Left => new(leftPortraitXPos, portraitRectTransform.anchoredPosition.y),
            NPCDialogueController.PortraitSide.Center => new(centerPortraitXPos, portraitRectTransform.anchoredPosition.y),
            NPCDialogueController.PortraitSide.Right => new(rightPortraitXPos, portraitRectTransform.anchoredPosition.y),
            _ => new(centerPortraitXPos, portraitRectTransform.anchoredPosition.y),
        };
        
        portraitRectTransform.anchoredPosition = portraitPosition;
        portraitImg.sprite = (GameManager.instance.PhaseManager.CurrentPhase == PhaseManager.Phase.Phase1) ? dialogueData.PortraitPhase1 : dialogueData.PortraitPhase2;
        portrait.SetActive(portraitImg.sprite != null);

        nameBox.SetActive(true);
        nameText.text = dialogueData.Name;

        // If the dialog has a choice and only one line, display the choice buttons
        ShowButtons(currentDialogueData.Dialogues[dialogueIndex].HasAChoice && dialogueData.Dialogues[dialogueIndex].Lines.Length == 1);

        dialoguePanel.SetActive(true);

        StartTypingAnimation();
        GameManager.instance.CurrentGameState = GameManager.GameState.ShowingDialogue;
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            // Cancel the typing animation and display the complete line
            StopCoroutine(typeLineRoutine);
            dialogueText.text = currentDialogueData.Dialogues[dialogueIndex].Lines[lineIndex];
            isTyping = false;
        }
        else if (++lineIndex < currentDialogueData.Dialogues[dialogueIndex].Lines.Length)
        {
            // If the dialogue has a choice and it is the last line, display the choice buttons
            bool isLastLine = lineIndex == currentDialogueData.Dialogues[dialogueIndex].Lines.Length - 1;
            ShowButtons(currentDialogueData.Dialogues[dialogueIndex].HasAChoice && isLastLine);

            StartTypingAnimation();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        StopCoroutine(typeLineRoutine);

        currentDialogueController.UpdateDialogueIndex();
        GameManager.instance.CurrentGameState = GameManager.GameState.Playing;

        // Reset dialogue and UI variables
        currentDialogueData = null;
        dialogueIndex = -1;
        lineIndex = -1;

        dialoguePanel.SetActive(false);
        portraitImg.sprite = null;
        nameText.text = "";
        dialogueText.text = "";
    }

    // === Button Methods ===
    private void ShowButtons(bool hasAChoice)
    {
        choiceButtons.SetActive(hasAChoice);
        nextButton.SetActive(!hasAChoice);
    }

    public void SelectChoice(bool choice)
    {
        if (isTyping)
        {
            // Cancel the typing animation and display the complete line
            StopCoroutine(typeLineRoutine);
            dialogueText.text = currentDialogueData.Dialogues[dialogueIndex].Lines[lineIndex];
            isTyping = false;
        }
        else
        {
            if (choice == true) YesButtonPressed?.Invoke(); else NoButtonPressed?.Invoke();
            EndDialogue();
        }
    }

    // === Animation Methods ===
    private void StartTypingAnimation()
    {
        // Type the first line of the current dialogue
        string nextLine = currentDialogueData.Dialogues[dialogueIndex].Lines[lineIndex];

        if (typeLineRoutine != null) StopCoroutine(typeLineRoutine);
        typeLineRoutine = StartCoroutine(TypeLine(nextLine));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogueData.TypingSpeed);
        }

        isTyping = false;
    }
}