
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExtendedEvent : UnityEvent { }

//扩展的滑条，在结束时候触发事件
public class ExtendedSlider : Slider, IEndDragHandler
{
    public ExtendedEvent DragEnd = new ExtendedEvent();

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnd?.Invoke();
    }
}