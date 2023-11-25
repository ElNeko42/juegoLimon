using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public float maxDragDistanceHorizontal = 200f;
    public float maxDragDistanceVertical = 100f;
    public float maxRotation = 10f;

    //private Renderer cardRenderer;

    private void Awake()
    {
        //cardRenderer = GetComponent<Renderer>();
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

        //CheckForLightEffect(newPosition.x); 
    }

        // Restablecer posición y rotación
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localRotation = Quaternion.identity;
    }

    private float CalculateRotation(float xPosition)
    {
        return (-xPosition / maxDragDistanceHorizontal) * maxRotation;
    }

    //private void CheckForLightEffect(float xPosition)
    //{
    //    // Calcula la intensidad de la luz basada en la posición
    //    float lightIntensity = Mathf.Abs(xPosition) / maxDragDistanceHorizontal;
    //    lightIntensity = Mathf.Clamp(lightIntensity, 0, 1);

    //    // Asigna la intensidad al shader
    //    cardRenderer.material.SetFloat("_LightIntensity", lightIntensity);
    //}
}
