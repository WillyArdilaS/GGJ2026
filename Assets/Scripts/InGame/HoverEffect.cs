using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour
{
    [SerializeField] private Color hoverColor = new(0.57f, 0.24f, 0.2f, 1);
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.Playing) return;

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