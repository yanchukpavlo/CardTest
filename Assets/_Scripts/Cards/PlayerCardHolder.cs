using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCardHolder : CardHolder
{
    static PlayerActionState previousState;

    #region Abstract

    override public void ChangeVisual(CardStatus status)
    {
        switch (status)
        {
            case CardStatus.Default:
                imageIcon.sprite = card.icon;
                textName.text = card.cardType.ToString();
                break;

            case CardStatus.Upgraded:
                imageIcon.sprite = card.iconUpd;
                textName.text = card.CardNameUpd;
                upgradedIndicator.gameObject.SetActive(true);
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), status);
                break;
        }
    }

    public override void MouseInteract(PlayerActionState status)
    {
        switch (status)
        {
            case PlayerActionState.None:
                Debug.Log("MouseInteract == none.");
                break;

            case PlayerActionState.Update:
                Upgrade();
                break;

            case PlayerActionState.Destroy:
                Destroy();
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), status);
                break;
        }
    }

    #endregion

    #region Pointer

    public override void OnPointerEnter(PointerEventData eventData)
    {
        previousState = Player.PlayerActionState;
        Player.OnChangeState += ListenerChangeAction;

        base.OnPointerEnter(eventData);
        Debug.LogFormat("Player mouse enter to {0}, card - {1}. Player current action - {2}", name, card.cardType, Player.PlayerActionState);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Player.OnChangeState -= ListenerChangeAction;

        base.OnPointerExit(eventData);
        Debug.LogFormat("Player mouse exit from {0}, card - {1}", name, card.cardType);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        MouseInteract(Player.PlayerActionState);
        Debug.LogFormat("Player mouse down over {0}, card - {1}. Action - {2}", name, card.cardType, previousState);
    }

    #endregion

    #region Virtual

    public override void Upgrade()
    {
        Player.OnChangeState -= ListenerChangeAction;
        Player.ChangeState(PlayerActionState.None);

        base.Upgrade();
    }

    public override void Destroy()
    {
        Player.OnChangeState -= ListenerChangeAction;
        Player.ChangeState(PlayerActionState.None);
        
        base.Destroy();
    }

    #endregion

    void ListenerChangeAction(PlayerActionState newState)
    {
        if (previousState != newState)
        {
            Debug.LogFormat("Change action over {0}, card - {1}. Action: previous {2} | new {3}", name, card.cardType, previousState, newState);
            MouseInteract(previousState);
            previousState = newState;
        }
    }
}
