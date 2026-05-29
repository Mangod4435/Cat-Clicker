using UnityEngine;
using UnityEngine.EventSystems;

public class PressedEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool holding { get; private set; } = false;

    public void OnPointerDown(PointerEventData e) => holding = true;

    public void OnPointerUp(PointerEventData e) => holding = false;
}
