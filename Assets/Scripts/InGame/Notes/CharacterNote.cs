using UnityEngine;

/// <summary>
/// Estructura para guardar datos de un sello colocado
/// </summary>
[System.Serializable]
public class StampData
{
    public int sealIndex; // Índice del sello (0-12)
    public Vector2 position; // Posición donde se colocó
    public bool isPlaced; // Si está colocado o no
}

/// <summary>
/// Estructura para guardar la información de una nota de un personaje
/// </summary>
[System.Serializable]
public class CharacterNote
{
    public string familyName;
    [TextArea(2, 5)]
    public string description;
    
    // Array para guardar los datos de los sellos en los 3 espacios
    public StampData[] stampDatas = new StampData[3];
    
    public CharacterNote()
    {
        familyName = "";
        description = "";
        stampDatas = new StampData[3];
        for (int i = 0; i < 3; i++)
        {
            stampDatas[i] = new StampData { isPlaced = false };
        }
    }
}
