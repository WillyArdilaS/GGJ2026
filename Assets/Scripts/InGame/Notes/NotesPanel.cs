using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotesPanel : MonoBehaviour
{
    [SerializeField] private GameObject notesPanelGameObject;
    [SerializeField] private GraphicRaycaster panelRaycaster; // Para bloquear raycast
    
    private bool isPanelActive = false;

    private void Start()
    {
        if (panelRaycaster == null && notesPanelGameObject != null)
            panelRaycaster = notesPanelGameObject.GetComponent<GraphicRaycaster>();
    }

    /// <summary>
    /// Alterna entre activar y desactivar el panel de notas
    /// </summary>
    public void ToggleNotesPanel()
    {
        isPanelActive = !isPanelActive;

        if (notesPanelGameObject != null)
        {
            notesPanelGameObject.SetActive(isPanelActive);
            BlockCharacterInteraction(isPanelActive);
            
            if (isPanelActive)
            {
                Debug.Log("Panel de notas activado");
            }
            else
            {
                Debug.Log("Panel de notas desactivado");
            }
        }
    }

    /// <summary>
    /// Desactiva el panel de notas sin toggle
    /// </summary>
    public void CloseNotesPanel()
    {
        isPanelActive = false;
        if (notesPanelGameObject != null)
        {
            notesPanelGameObject.SetActive(false);
            BlockCharacterInteraction(false);
        }
    }

    /// <summary>
    /// Bloquea o permite la interacción con los personajes
    /// </summary>
    private void BlockCharacterInteraction(bool block)
    {
        if (panelRaycaster != null)
        {
            panelRaycaster.enabled = block;
        }
    }

    /// <summary>
    /// Obtiene el estado actual del panel
    /// </summary>
    public bool IsPanelActive()
    {
        return isPanelActive;
    }
}
