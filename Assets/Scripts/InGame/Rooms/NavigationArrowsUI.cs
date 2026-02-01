using UnityEngine;

[RequireComponent(typeof(RoomManager))]
public class NavigationArrowsUI : MonoBehaviour
{
    // === Manager ===
    private RoomManager roomManager;

    // === UI Elements ===
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup leftArrowGroup;
    [SerializeField] private CanvasGroup rightArrowGroup;

    // === Settings ===
    [Header("Settings")]
    [SerializeField, Tooltip("Width in pixels from screen edge to activate arrow")] private float activationMargin;
    [SerializeField] private float fadeSpeed;

    void Awake()
    {
        roomManager = GetComponent<RoomManager>();

        // Hide both arrow gorups
        leftArrowGroup.alpha = 0f;
        leftArrowGroup.interactable = false;
        leftArrowGroup.blocksRaycasts = false;

        rightArrowGroup.alpha = 0f;
        rightArrowGroup.interactable = false;
        rightArrowGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (GameManager.instance.CurrentGameState != GameManager.GameState.Playing) return;

        float mouseX = Input.mousePosition.x;
        float screenWidth = Screen.width;

        bool isOnLeftMargin = mouseX <= activationMargin && roomManager.CurrentRoomIndex > 0;
        bool isOnRightMargin = mouseX >= screenWidth - activationMargin && roomManager.CurrentRoomIndex < roomManager.Rooms.Length - 1;

        FadeGroup(leftArrowGroup, isOnLeftMargin);
        FadeGroup(rightArrowGroup, isOnRightMargin);
    }

    private void FadeGroup(CanvasGroup group, bool isShowing)
    {
        float targetAlpha = isShowing ? 1f : 0f;
        group.alpha = Mathf.Lerp(group.alpha, targetAlpha, fadeSpeed * Time.deltaTime);

        bool interactable = group.alpha > 0.9f;
        group.interactable = interactable;
        group.blocksRaycasts = interactable;
    }
}