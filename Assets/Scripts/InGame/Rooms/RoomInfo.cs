using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField] private Vector2 position;

    // === Properties ===
    public Vector2 Position => position;

    void Awake()
    {
        position = transform.position;
    }
}