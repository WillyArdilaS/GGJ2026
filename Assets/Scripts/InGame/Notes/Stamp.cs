using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
    private void Start()
    {
        // Configurar el color de la estampilla (magenta)
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = new Color(1, 0, 1, 1); // Magenta
        }

        // Desactivar la interacción de la estampilla (no es interactiva)
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
        }
    }
}
