using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SealCarouselSystem : MonoBehaviour
{
    [SerializeField] private List<Sprite> sealSprites = new List<Sprite>(); // 13 sprites
    [SerializeField] private Transform[] sealSlots = new Transform[3]; // 3 imágenes visibles
    [SerializeField] private float animationSpeed = 0.3f;
    [SerializeField] private RectTransform carouselContainer;
    [SerializeField] private Image stampPrefabImage; // Referencia al componente Image del prefab de sello
    [SerializeField] private Button nextButton; // Botón para siguiente sello
    
    private int currentStartIndex = 0; // Índice del primer sello visible
    private int selectedSealIndex = -1; // -1 = ninguno seleccionado
    private bool isAnimating = false;

    private void Start()
    {
        // Validar que hay suficientes sprites
        if (sealSprites.Count != 13)
        {
            Debug.LogError("SealCarouselSystem: Se necesitan exactamente 13 sprites");
            return;
        }

        // Inicializar slots si no están asignados
        if (sealSlots[0] == null)
        {
            InitializeSlotsAutomatically();
        }

        // Conectar botones
        if (nextButton != null)
            nextButton.onClick.AddListener(() => RotateCarousel(1));
        UpdateCarouselDisplay();
    }

    private void Update()
    {
        // Sin lógica de movimiento del mouse
    }

    /// <summary>
    /// Rota el carrusel hacia adelante (1) o hacia atrás (-1)
    /// </summary>
    public void RotateCarousel(int direction)
    {
        if (isAnimating) return;

        StartCoroutine(AnimateRotation(direction));
    }

    private System.Collections.IEnumerator AnimateRotation(int direction)
    {
        isAnimating = true;

        // Calcular nuevo índice (circular)
        currentStartIndex = (currentStartIndex + direction + 13) % 13;

        // Actualizar visualmente con animación
        yield return StartCoroutine(AnimateSlotChange());

        isAnimating = false;
    }

    private System.Collections.IEnumerator AnimateSlotChange()
    {
        float elapsed = 0f;

        while (elapsed < animationSpeed)
        {
            elapsed += Time.deltaTime;
            UpdateCarouselDisplay();
            yield return null;
        }

        UpdateCarouselDisplay();
    }

    /// <summary>
    /// Actualiza qué sellos se muestran en los 3 slots
    /// </summary>
    private void UpdateCarouselDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            int sealIndex = (currentStartIndex + i) % 13;
            Image slotImage = sealSlots[i].GetComponent<Image>();
            
            if (slotImage != null)
            {
                slotImage.sprite = sealSprites[sealIndex];
            }

            // Agregar listener de click si no tiene
            Button slotButton = sealSlots[i].GetComponent<Button>();
            if (slotButton == null)
            {
                slotButton = sealSlots[i].gameObject.AddComponent<Button>();
            }

            int sealIndexCopy = sealIndex; // Capturar para el closure
            slotButton.onClick.AddListener(() => SelectSeal(sealIndexCopy));
        }
    }

    /// <summary>
    /// Selecciona un sello
    /// </summary>
    public void SelectSeal(int sealIndex)
    {
        if (sealIndex < 0 || sealIndex >= 13)
        {
            Debug.LogError("SealCarouselSystem: Índice de sello inválido");
            return;
        }

        selectedSealIndex = sealIndex;
        
        // Actualizar el sprite del prefab de sello
        if (stampPrefabImage != null)
        {
            stampPrefabImage.sprite = sealSprites[sealIndex];
        }
        
        Debug.Log($"Sello seleccionado: {sealIndex}");
    }

    /// <summary>
    /// Obtiene el sprite de un sello por su índice
    /// </summary>
    public Sprite GetSealSprite(int index)
    {
        if (index >= 0 && index < sealSprites.Count)
            return sealSprites[index];
        return null;
    }

    /// <summary>
    /// Obtiene el índice del sello seleccionado actualmente
    /// </summary>
    public int GetSelectedSealIndex()
    {
        return selectedSealIndex;
    }

    /// <summary>
    /// Obtiene el sprite del sello seleccionado
    /// </summary>
    public Sprite GetSelectedSeal()
    {
        if (selectedSealIndex >= 0)
            return sealSprites[selectedSealIndex];
        return null;
    }

    /// <summary>
    /// Inicializa los slots automáticamente buscando children con Image
    /// </summary>
    private void InitializeSlotsAutomatically()
    {
        Image[] childImages = carouselContainer.GetComponentsInChildren<Image>();
        
        for (int i = 0; i < Mathf.Min(3, childImages.Length); i++)
        {
            sealSlots[i] = childImages[i].transform;
        }

        Debug.Log("SealCarouselSystem: Slots inicializados automáticamente");
    }
}