using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class CardHolder : Button4Action
{
    [Space(20)]
    [SerializeField] protected Image imageIcon;
    [SerializeField] protected GameObject upgradedIndicator;
    [SerializeField] protected TextMeshProUGUI textName;
    //[SerializeField] protected TextMeshProUGUI textDescription;
    [SerializeField] protected TextMeshProUGUI textHelpInfo;

    public Card card { get; private set; }
    Deck currentDeck;

    public bool IsUpgraded
    {
        get
        {
            return card.status == CardStatus.Upgraded;
        }
    }

    public byte SellingCost
    {
        get
        {
            return card.status switch
            {
                CardStatus.Default => card.selling,
                CardStatus.Upgraded => card.sellingUpd,
                _ => 0
            };
        }
    }

    #region Abstract

    abstract public void ChangeVisual(CardStatus status);
    abstract public void MouseInteract(PlayerActionState status);

    #endregion

    #region Virtual

    virtual public void Setup(Card card, Deck deck)
    {
        currentDeck = deck;
        this.card = card;
        ChangeVisual(card.status);
    }

    virtual public void Upgrade()
    {
        currentDeck.CardUpgrade(this);
        card.status = CardStatus.Upgraded;
        ChangeVisual(card.status);
    }

    virtual public void Destroy()
    {
        currentDeck.CardRemove(this);
        Destroy(gameObject);
    }

    #endregion

}
