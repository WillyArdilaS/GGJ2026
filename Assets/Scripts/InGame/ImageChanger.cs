using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ImageChanger : MonoBehaviour
{
    // === Sprite ===
    [SerializeField] private Sprite imagePhase2;
    private SpriteRenderer spriteRend;

    // === Position ===
    [SerializeField] private Vector2 positionInPhase2;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        GameManager.instance.PhaseManager.Phase1Completed += UpdateImagePhase2;
    }

    private void UpdateImagePhase2()
    {
        spriteRend.sprite = imagePhase2;
        transform.localPosition = positionInPhase2;
    }
}