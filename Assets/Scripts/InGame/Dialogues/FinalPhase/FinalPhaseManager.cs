using System.Collections;
using UnityEngine;

public class FinalPhaseManager : MonoBehaviour
{
    // === Dialogue Datas ===
    [SerializeField] private PlayerDialogueData dialogueDataEN;
    [SerializeField] private PlayerDialogueData dialogueDataES;

    void Awake()
    {
        GameManager.instance.PhaseManager.Phase2Completed += StartFinalPhase;
    }

    private void StartFinalPhase()
    {
        StopAllCoroutines();
        StartCoroutine(ShowFinalDialogue());
    }

    private IEnumerator ShowFinalDialogue()
    {
        yield return new WaitForSeconds(0.5f);

        switch (GlobalManager.instance.LanguageManager.CurrentLanguage)
        {
            case LanguageManager.Language.English:
                GameManager.instance.DialogueManager.StartFinalPhaseDialogue(dialogueDataEN, 0);
                break;
            case LanguageManager.Language.Spanish:
                GameManager.instance.DialogueManager.StartFinalPhaseDialogue(dialogueDataES, 0);
                break;
            default:
                GameManager.instance.DialogueManager.StartFinalPhaseDialogue(dialogueDataES, 0);
                break;
        }
    }

    public void MakeFinalAccusation(string npcName, float typingSpeed)
    {
        GameManager.instance.DialogueManager.ShowAccusedName(npcName, typingSpeed);
    }
}