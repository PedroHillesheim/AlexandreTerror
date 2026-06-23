using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float movementAmount = 20f;

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f) * 2f;
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f) * 2f;

        Vector2 targetPos = startPos + new Vector2(
            -mouseX * movementAmount,
            -mouseY * movementAmount
        );

        rectTransform.anchoredPosition = Vector2.Lerp(
            rectTransform.anchoredPosition,
            targetPos,
            Time.deltaTime * 5f
        );
    }
}