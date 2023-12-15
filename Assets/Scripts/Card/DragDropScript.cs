using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public float maxDragDistanceHorizontal = 200f;
    public float maxDragDistanceVertical = 100f;
    public float maxRotation = 10f;

    public delegate void DragAction(float positionX);
    public static event DragAction OnCardDrag;


    private void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta;
        newPosition.x = Mathf.Clamp(newPosition.x, -maxDragDistanceHorizontal, maxDragDistanceHorizontal);
        rectTransform.anchoredPosition = newPosition;
        newPosition.y = Mathf.Clamp(newPosition.y, -maxDragDistanceVertical, maxDragDistanceVertical);
        rectTransform.anchoredPosition = newPosition;
        // Inclinación basada en la posición
        float rotationAngle = CalculateRotation(newPosition.x);
        rectTransform.localRotation = Quaternion.Euler(0, 0, rotationAngle);

        CardManager cardManager = FindObjectOfType<CardManager>();
        if (cardManager != null)
        {
            cardManager.UpdateOptionVisibility(rectTransform.anchoredPosition.x);
        }
        OnCardDrag?.Invoke(rectTransform.anchoredPosition.x);


    }

    // Restablecer posición y rotación
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(rectTransform.anchoredPosition.x) >= maxDragDistanceHorizontal)
        {
            bool nextCard = rectTransform.anchoredPosition.x > 0;
            FindObjectOfType<CardManager>().ChangeCard(nextCard);
        }
        else
        {
            // Restablecer posición y rotación
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localRotation = Quaternion.identity;
            FindObjectOfType<CardManager>().MakeOptionsInvisible();
            ;
        }
    }


    private float CalculateRotation(float xPosition)
    {
        return (-xPosition / maxDragDistanceHorizontal) * maxRotation;
    }

   
}
