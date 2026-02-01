using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour
{
    [SerializeField] private Color hoverColor = new(0.85f, 0.5f, 0.5f, 1f);
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        spriteRend.color = hoverColor;
    }

    void OnMouseExit()
    {
        spriteRend.color = Color.white;
    }

    void OnMouseDown()
    {
        spriteRend.color = Color.white;
    }
}