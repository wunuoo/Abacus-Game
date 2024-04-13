
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExtendedEvent : UnityEvent { }

//��չ�Ļ������ڽ���ʱ�򴥷��¼�
public class ExtendedSlider : Slider, IEndDragHandler
{
    public ExtendedEvent DragEnd = new ExtendedEvent();

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnd?.Invoke();
    }
}