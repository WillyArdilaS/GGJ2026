using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotesPanel : MonoBehaviour
{
    [SerializeField] private GameObject notesPanelGameObject;
    [SerializeField] private GraphicRaycaster panelRaycaster; // Para bloquear raycast
    [SerializeField] private GameObject settingsPanel;

    

    private void Start()
    {
        if (panelRaycaster == null && notesPanelGameObject != null)
            panelRaycaster = notesPanelGameObject.GetComponent<GraphicRaycaster>();
    }
    public void TogglePanel()
    {
        if (settingsPanel == null) return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    private bool isPanelActive = false;
        public void ToggleNotesPanel()
    {
        isPanelActive = !isPanelActive;

        if (notesPanelGameObject != null)
        {
            notesPanelGameObject.SetActive(isPanelActive);
            BlockCharacterInteraction(isPanelActive);
        }
    }

    public void CloseNotesPanel()
    {
        isPanelActive = false;
        if (notesPanelGameObject != null)
        {
            notesPanelGameObject.SetActive(false);
            BlockCharacterInteraction(false);
        }
    }

    private void BlockCharacterInteraction(bool block)
    {
        if (panelRaycaster != null)
        {
            panelRaycaster.enabled = block;
        }
    }

    public bool IsPanelActive()
    {
        return isPanelActive;
    }
}
