using UnityEngine;

public class PupilFollower : MonoBehaviour
{
    [SerializeField] private float maxDistance = 500f;
    
    private RectTransform pupilRect;
    private RectTransform eyeRect;

    private void Start()
    {
        pupilRect = GetComponent<RectTransform>();
        eyeRect = transform.parent?.GetComponent<RectTransform>();
        
        if (pupilRect == null || eyeRect == null)
        {
            Debug.LogError("PupilFollower: Necesita RectTransform en la pupila y en el padre (ojo)");
            enabled = false;
        }
    }

    private void Update()
    {
        if (pupilRect == null || eyeRect == null)
            return;

        // Obtener dirección del mouse respecto al ojo
        Vector2 dir = (Vector2)Input.mousePosition - (Vector2)eyeRect.position;
        dir = dir.normalized;
        
        // Mover la pupila hacia el mouse, limitado por maxDistance
        Vector2 targetPos = dir * maxDistance;
        pupilRect.localPosition = targetPos;
    }
}
