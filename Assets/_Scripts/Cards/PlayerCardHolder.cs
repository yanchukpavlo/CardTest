using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerCardHolder : CardHolder
{
    static ActionState previousState;
    CanvasGroup canvasGroup;

    override protected void Start()
    {
        base.Start();
        Player.OnChangeState += ChangeInteraction;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    override protected void OnDestroy()
    {
        base.OnDestroy();
        Player.OnChangeState -= ChangeInteraction;
    }

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

    public override void MouseInteract(ActionState status)
    {
        switch (status)
        {
            case ActionState.None:
                Debug.Log("MouseInteract == none.");
                break;

            case ActionState.Update:
                Upgrade();
                break;

            case ActionState.Destroy:
                Destroy();
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), status);
                break;
        }
    }

    #endregion

    #region Pointer

    public override void OnSelect(BaseEventData eventData)
    {
        previousState = Player.ActionState;
        Player.OnChangeState += ListenerChangeAction;
        InfoText(previousState);

        base.OnSelect(eventData);
        Debug.LogFormat("Player mouse enter to {0}, card - {1}. Player current action - {2}", name, card.cardType, Player.ActionState);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        Player.OnChangeState -= ListenerChangeAction;
        InfoText(ActionState.None);

        base.OnDeselect(eventData);
        Debug.LogFormat("Player mouse exit from {0}, card - {1}", name, card.cardType);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        MouseInteract(Player.ActionState);
        Debug.LogFormat("Player mouse down over {0}, card - {1}. Action - {2}", name, card.cardType, previousState);
    }

    #endregion

    #region Virtual

    public override void Upgrade()
    {
        Player.OnChangeState -= ListenerChangeAction;
        base.Upgrade();
        InfoText(ActionState.None);
    }

    public override void Destroy()
    {
        Player.OnChangeState -= ListenerChangeAction;
        // var neighbor = FindSelectable(Vector3.zero);
        // if (neighbor) EventSystem.current.SetSelectedGameObject(neighbor.gameObject);

        base.Destroy();
    }

    #endregion

    void InfoText(ActionState status)
    {
        switch (status)
        {
            case ActionState.None:
                textHelpInfo.gameObject.SetActive(false);
                break;

            case ActionState.Update:
                if (IsUpgraded) return;
                textHelpInfo.text = $"-{card.updatedCost}";
                textHelpInfo.gameObject.SetActive(true);
                break;

            case ActionState.Destroy:
                textHelpInfo.text = $"+{SellingCost}";
                textHelpInfo.gameObject.SetActive(true);
                break;

            default:
                Debug.LogWarningFormat("WTF state in {0}, with - {1}.", this.GetType(), status);
                break;
        }
    }

    void ListenerChangeAction(ActionState newState)
    {
        if (previousState != newState)
        {
            Debug.LogFormat("Change action over {0}, card - {1}. Action: previous {2} | new {3}", name, card.cardType, previousState, newState);
            MouseInteract(previousState);
            previousState = newState;
        }
    }

    void ChangeInteraction(ActionState newState)
    {
        if (newState == ActionState.Update && Player.Currency < card.updatedCost)
        {
            canvasGroup.interactable = false;
        }
        else
        {
            canvasGroup.interactable = true;
        }
    }
}
