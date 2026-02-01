using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaxSealTool : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject stampPrefab;
    [SerializeField] private RectTransform noteRectTransform; // RectTransform de la nota/hoja
    [SerializeField] private RectTransform[] spaceSealRectTransforms = new RectTransform[3]; // 3 espacios donde se puede poner el sello
    [SerializeField] private Canvas canvas; // Canvas padre
    [SerializeField] private SealCarouselSystem sealCarousel; // Referencia al carrusel de sellos
    
    private GameObject[] placedStamps = new GameObject[3]; // Sellos colocados en cada espacio
    private int[] placedSealIndices = new int[3]; // Índices de los sellos colocados (para guardar sprites)
    
    private RectTransform rectTransform;
    private Vector3 originalScale = new Vector3(2, 2, 2);
    private Vector3 activeScale = new Vector3(3, 3, 3);
    private bool isActive = false;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    private void Update()
    {
        if (isActive && rectTransform != null)
        {
            Vector2 mousePos = Input.mousePosition;
            rectTransform.position = mousePos;

            // Escalar según si el ratón está cerca/dentro del panel de notas
            if (noteRectTransform != null)
            {
                bool isNearNote = RectTransformUtility.RectangleContainsScreenPoint(noteRectTransform, mousePos, null);
                
                if (isNearNote)
                {
                    rectTransform.localScale = activeScale;
                }
                else
                {
                    rectTransform.localScale = new Vector3(2, 2, 2);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Si no está activa, activarla
        if (!isActive)
        {
            ToggleTool();
        }
        else
        {
            // Si está activa, comprobar si el click fue en la nota
            CheckStampNote(eventData);
        }
    }

    private void ToggleTool()
    {
        isActive = !isActive;

        if (isActive)
        {

            rectTransform.localScale = originalScale;
            rectTransform.pivot = new Vector2(0, 0);
            Cursor.visible = false;
        }
        else
        {
            rectTransform.localScale = originalScale;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            // Repositionar a la posición inicial
            rectTransform.anchoredPosition = new Vector2(423, -63);
            
            Cursor.visible = true;
        }
    }

    private void CheckStampNote(PointerEventData eventData)
    {
        if (noteRectTransform == null || !isActive)
            return;

        // Comprobar cuál espacio de sello fue clickeado
        int spaceSealIndex = -1;
        for (int i = 0; i < spaceSealRectTransforms.Length; i++)
        {
            if (spaceSealRectTransforms[i] != null && 
                RectTransformUtility.RectangleContainsScreenPoint(spaceSealRectTransforms[i], Input.mousePosition, null))
            {
                spaceSealIndex = i;
                break;
            }
        }

        if (spaceSealIndex == -1)
            return; // No está en ninguno de los espacios de sello

        if (RectTransformUtility.RectangleContainsScreenPoint(noteRectTransform, Input.mousePosition, null))
        {
            StampNote(Input.mousePosition, spaceSealIndex);
        }
    }

    private void StampNote(Vector2 stampScreenPos, int spaceSealIndex)
    {
        GameObject newStamp = Instantiate(stampPrefab, noteRectTransform);
        RectTransform stampRT = newStamp.GetComponent<RectTransform>();

        // Determinar cámara correcta para la conversión (null para ScreenSpaceOverlay)
        Camera cam = null;
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera;
        
        // Convertir la posición de pantalla a posición local en el RectTransform de la nota
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            noteRectTransform,
            stampScreenPos,
            cam,
            out Vector2 localPos))
        {
            if (stampRT != null)
            {
                // Asegurar que la estampilla se centra exactamente en el punto de click
                stampRT.pivot = new Vector2(0.5f, 0.5f);
                stampRT.anchoredPosition = localPos;
            }
            else
            {
                // Fallback: posicionar en mundo si no es UI
                Vector3 worldPos = Camera.main != null ? Camera.main.ScreenToWorldPoint(stampScreenPos) : Vector3.zero;
                worldPos.z = 0f;
                newStamp.transform.position = worldPos;
            }
        }
        
        // Habilitar el GameObject hijo "Delete" del SpaceSeal
        if (spaceSealRectTransforms[spaceSealIndex] != null)
        {
            Transform deleteButton = spaceSealRectTransforms[spaceSealIndex].Find("Delete");
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(true);
                
                // Agregar componente Button si no tiene
                Button deleteButtonComponent = deleteButton.GetComponent<Button>();
                if (deleteButtonComponent == null)
                {
                    deleteButtonComponent = deleteButton.gameObject.AddComponent<Button>();
                }
                
                // Limpiar listeners previos para evitar duplicados
                deleteButtonComponent.onClick.RemoveAllListeners();
                
                // Guardar referencia del sello y el botón Delete
                placedStamps[spaceSealIndex] = newStamp;
                
                // Guardar el índice del sello seleccionado en el carrusel
                if (sealCarousel != null)
                {
                    placedSealIndices[spaceSealIndex] = sealCarousel.GetSelectedSealIndex();
                }
                else
                {
                    placedSealIndices[spaceSealIndex] = 0;
                }
                
                // Agregar listener para eliminar el sello
                deleteButtonComponent.onClick.AddListener(() => DeleteStamp(spaceSealIndex, deleteButton.gameObject));
            }
        }
        
        ToggleTool();
    }

    /// <summary>
    /// Elimina el sello de un espacio específico y desactiva su botón Delete
    /// </summary>
    private void DeleteStamp(int spaceSealIndex, GameObject deleteButtonGameObject)
    {
        // Eliminar el sello
        if (placedStamps[spaceSealIndex] != null)
        {
            Destroy(placedStamps[spaceSealIndex]);
            placedStamps[spaceSealIndex] = null;
        }
        
        // Desactivar el botón Delete
        deleteButtonGameObject.SetActive(false);
    }

    /// <summary>
    /// Obtiene los datos de los sellos colocados actualmente
    /// </summary>
    public StampData[] GetPlacedStampDatas()
    {
        StampData[] stampDatas = new StampData[placedStamps.Length];
        
        for (int i = 0; i < placedStamps.Length; i++)
        {
            if (placedStamps[i] != null)
            {
                stampDatas[i] = new StampData
                {
                    isPlaced = true,
                    sealIndex = placedSealIndices[i],
                    position = placedStamps[i].GetComponent<RectTransform>().anchoredPosition
                };
            }
            else
            {
                stampDatas[i] = new StampData { isPlaced = false, sealIndex = 0 };
            }
        }
        
        return stampDatas;
    }

    /// <summary>
    /// Restaura los sellos colocados desde datos guardados
    /// </summary>
    public void RestorePlacedStamps(StampData[] stampDatas)
    {
        if (stampDatas == null)
            return;

        // Limpiar sellos actuales
        for (int i = 0; i < placedStamps.Length; i++)
        {
            if (placedStamps[i] != null)
            {
                Destroy(placedStamps[i]);
                placedStamps[i] = null;
            }

            // Desactivar botones Delete
            if (spaceSealRectTransforms[i] != null)
            {
                Transform deleteButton = spaceSealRectTransforms[i].Find("Delete");
                if (deleteButton != null)
                {
                    deleteButton.gameObject.SetActive(false);
                }
            }
        }

        // Restaurar sellos guardados
        for (int i = 0; i < stampDatas.Length && i < placedStamps.Length; i++)
        {
            if (stampDatas[i].isPlaced)
            {
                // Instanciar sello en la posición guardada
                GameObject newStamp = Instantiate(stampPrefab, noteRectTransform);
                RectTransform stampRT = newStamp.GetComponent<RectTransform>();
                Image stampImage = newStamp.GetComponent<Image>();
                
                if (stampRT != null)
                {
                    stampRT.pivot = new Vector2(0.5f, 0.5f);
                    stampRT.anchoredPosition = stampDatas[i].position;
                }
                
                // Restaurar el sprite guardado
                if (stampImage != null && sealCarousel != null)
                {
                    Sprite savedSprite = sealCarousel.GetSealSprite(stampDatas[i].sealIndex);
                    if (savedSprite != null)
                    {
                        stampImage.sprite = savedSprite;
                    }
                }
                
                placedStamps[i] = newStamp;
                placedSealIndices[i] = stampDatas[i].sealIndex;

                // Activar y configurar botón Delete
                if (spaceSealRectTransforms[i] != null)
                {
                    Transform deleteButton = spaceSealRectTransforms[i].Find("Delete");
                    if (deleteButton != null)
                    {
                        deleteButton.gameObject.SetActive(true);
                        Button deleteButtonComponent = deleteButton.GetComponent<Button>();
                        if (deleteButtonComponent == null)
                        {
                            deleteButtonComponent = deleteButton.gameObject.AddComponent<Button>();
                        }
                        deleteButtonComponent.onClick.RemoveAllListeners();
                        int index = i;
                        deleteButtonComponent.onClick.AddListener(() => DeleteStamp(index, deleteButton.gameObject));
                    }
                }
            }
        }
    }
}
