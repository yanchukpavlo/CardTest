using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Button2Action : Button
{
    [FormerlySerializedAs("onDown")] [SerializeField]
    private ButtonClickedEvent m_OnDown = new ButtonClickedEvent();

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        m_OnDown?.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        InstantClearState();

        base.OnDeselect(eventData);
    }
}
