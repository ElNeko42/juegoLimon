using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public float maxDragDistanceHorizontal = 200f;
    public float maxDragDistanceVertical = 100f;
    public float maxRotation = 10f;


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
        // Inclinaci�n basada en la posici�n
        float rotationAngle = CalculateRotation(newPosition.x);
        rectTransform.localRotation = Quaternion.Euler(0, 0, rotationAngle);

        
    }

    // Restablecer posici�n y rotaci�n
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(rectTransform.anchoredPosition.x) >= maxDragDistanceHorizontal)
        {
            bool nextCard = rectTransform.anchoredPosition.x > 0;
           // Debug.Log(nextCard); 
            FindObjectOfType<CardManager>().ChangeCard(nextCard);
        }
        else
        {
            // Restablecer posici�n y rotaci�n
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localRotation = Quaternion.identity;
        }
    }


    private float CalculateRotation(float xPosition)
    {
        return (-xPosition / maxDragDistanceHorizontal) * maxRotation;
    }

   
}
