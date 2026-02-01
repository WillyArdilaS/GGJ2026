using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject que contiene todas las notas de los 13 personajes
/// </summary>
[CreateAssetMenu(fileName = "CharacterNotesDatabase", menuName = "Game/Character Notes Database")]
public class CharacterNotesDatabase : ScriptableObject
{
    [SerializeField] private List<CharacterNote> characterNotes = new List<CharacterNote>();

    /// <summary>
    /// Obtiene la nota de un personaje por índice
    /// </summary>
    public CharacterNote GetCharacterNote(int index)
    {
        if (index >= 0 && index < characterNotes.Count)
            return characterNotes[index];
        
        Debug.LogError($"CharacterNotesDatabase: Índice {index} fuera de rango");
        return null;
    }

    /// <summary>
    /// Obtiene todas las notas
    /// </summary>
    public List<CharacterNote> GetAllNotes()
    {
        return characterNotes;
    }

    /// <summary>
    /// Obtiene la cantidad total de personajes
    /// </summary>
    public int GetCharacterCount()
    {
        return characterNotes.Count;
    }
}
