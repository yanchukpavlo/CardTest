using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Button4Action : Button2Action
{
    [FormerlySerializedAs("onEnter")] [SerializeField]
    private ButtonClickedEvent m_OnEnter = new ButtonClickedEvent();

    [FormerlySerializedAs("onExit")] [SerializeField]
    private ButtonClickedEvent m_OnExit = new ButtonClickedEvent();

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        m_OnExit?.Invoke();
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        m_OnEnter?.Invoke();
    }
}
