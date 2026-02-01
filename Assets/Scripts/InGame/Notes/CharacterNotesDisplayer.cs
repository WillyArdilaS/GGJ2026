using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Script que controla la visualización de las notas de personajes en el UI
/// </summary>
public class CharacterNotesDisplayer : MonoBehaviour
{
    [SerializeField] private CharacterNotesDatabase notesDatabase;
    [SerializeField] private TextMeshProUGUI characterNameText; // TMP Text para el nombre
    [SerializeField] private TextMeshProUGUI characterDescriptionText; // TMP Text para la descripción
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private WaxSealTool waxSealTool; // Referencia a WaxSealTool para sincronizar sellos
    
    private int currentCharacterIndex = 0;
    private int totalCharacters = 0;

    private void Start()
    {
        if (notesDatabase == null)
        {
            Debug.LogError("CharacterNotesDisplayer: No hay CharacterNotesDatabase asignado");
            return;
        }

        totalCharacters = notesDatabase.GetCharacterCount();

        if (totalCharacters == 0)
        {
            Debug.LogError("CharacterNotesDisplayer: CharacterNotesDatabase está vacío");
            return;
        }

        // Conectar botones
        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNextCharacter);
        else
            Debug.LogWarning("CharacterNotesDisplayer: No hay Next Button asignado");
        
        if (previousButton != null)
            previousButton.onClick.AddListener(ShowPreviousCharacter);
        else
            Debug.LogWarning("CharacterNotesDisplayer: No hay Previous Button asignado");

        // Mostrar el primer personaje
        UpdateDisplay();
    }

    /// <summary>
    /// Muestra el siguiente personaje
    /// </summary>
    public void ShowNextCharacter()
    {
        // Guardar los sellos de la nota actual antes de cambiar
        SaveCurrentCharacterStamps();
        
        currentCharacterIndex = (currentCharacterIndex + 1) % totalCharacters;
        UpdateDisplay();
    }

    /// <summary>
    /// Muestra el personaje anterior
    /// </summary>
    public void ShowPreviousCharacter()
    {
        // Guardar los sellos de la nota actual antes de cambiar
        SaveCurrentCharacterStamps();
        
        currentCharacterIndex = (currentCharacterIndex - 1 + totalCharacters) % totalCharacters;
        UpdateDisplay();
    }

    /// <summary>
    /// Actualiza la visualización con el personaje actual
    /// </summary>
    private void UpdateDisplay()
    {
        CharacterNote currentNote = notesDatabase.GetCharacterNote(currentCharacterIndex);

        if (currentNote == null)
            return;

        if (characterNameText != null)
            characterNameText.text = currentNote.familyName;

        if (characterDescriptionText != null)
            characterDescriptionText.text = currentNote.description;

        // Restaurar los sellos de esta nota
        LoadCharacterStamps(currentNote);
        
        Debug.Log($"Mostrando nota de: {currentNote.familyName}");
    }

    /// <summary>
    /// Guarda los sellos colocados en la nota actual
    /// </summary>
    private void SaveCurrentCharacterStamps()
    {
        CharacterNote currentNote = notesDatabase.GetCharacterNote(currentCharacterIndex);
        
        if (currentNote != null && waxSealTool != null)
        {
            StampData[] stampDatas = waxSealTool.GetPlacedStampDatas();
            currentNote.stampDatas = stampDatas;
            Debug.Log($"Guardados sellos de: {currentNote.familyName}");
        }
    }

    /// <summary>
    /// Carga los sellos de una nota específica
    /// </summary>
    private void LoadCharacterStamps(CharacterNote note)
    {
        if (waxSealTool != null && note != null)
        {
            waxSealTool.RestorePlacedStamps(note.stampDatas);
            Debug.Log($"Restaurados sellos de: {note.familyName}");
        }
    }

    /// <summary>
    /// Obtiene el índice del personaje actual
    /// </summary>
    public int GetCurrentCharacterIndex()
    {
        return currentCharacterIndex;
    }
}
