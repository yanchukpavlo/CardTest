using System;
using UnityEngine;

public enum CardStatus : byte
{
    Default,
    Upgraded
}

public enum CardType : ushort
{
    Stalker,
    Orbling,
    Seedling,
    Warden,
    Maiden
}

[System.Serializable]
public class Card
{
    [field: SerializeField] public CardType cardType { get; private set; }
    [SerializeField] string cardNameUpd = "Elder {0}";
    [field: SerializeField] public Sprite icon { get; private set; }
    [field: SerializeField] public Sprite iconUpd { get; private set; }

    [field: SerializeField] public byte costUpdate { get; private set; }
    [field: SerializeField] public byte selling { get; private set; }
    [field: SerializeField] public byte sellingUpd { get; private set; }

    public CardStatus status { get; set; } = CardStatus.Default;
    public string CardNameUpd { get { return String.Format(cardNameUpd, cardType.ToString()); } }

    public Card Clone()
    {
        Card clon = new Card();

        clon.cardType = cardType;
        clon.cardNameUpd = cardNameUpd;
        clon.icon = icon;
        clon.iconUpd = iconUpd;
        clon.costUpdate = costUpdate;
        clon.selling = selling;
        clon.sellingUpd = sellingUpd;

        return clon;
    }

    public bool IsUpgraded()
    {
        return status == CardStatus.Upgraded;
    }
}
