using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ImageChanger : MonoBehaviour
{
    [SerializeField] private Sprite imagePhase2;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        GameManager.instance.PhaseManager.Phase1Completed += UpdateImagePhase2;
    }

    private void UpdateImagePhase2()
    {
        spriteRend.sprite = imagePhase2;
    }
}