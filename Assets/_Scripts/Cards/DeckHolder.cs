using System.Collections.Generic;
using UnityEngine;

public class DeckHolder : MonoBehaviour
{
    [field: SerializeField] public Transform cardTableTr { get; private set; }
    [field: SerializeField] public CardHolder prefabCardHolder { get; private set; }
    [SerializeField] StartCard[] startCards;

    public Deck deck { get; private set; }

    public Deck Setup()
    {
        Deck deck = new Deck();
        CardHolder cardHolder = null;

        for (int i = 0; i < startCards.Length; i++)
        {
            cardHolder = Instantiate(prefabCardHolder, cardTableTr);
            cardHolder.Setup(startCards[i].GetCardInfo(), deck);
            deck.CardAdd(cardHolder);
        }

        return deck;
    }

    public List<Card> GetStartCards()
    {
        List<Card> cards = new List<Card>();
        foreach (var item in startCards)
        {
            cards.Add(item.GetCardInfo());
        }

        return cards;
    }
}

[System.Serializable]
class StartCard
{
    [SerializeField] SO_Card cardData;
    [SerializeField] CardStatus status;

    public Card GetCardInfo()
    {
        Card card = cardData.card.Clone();
        card.status = status;
        return card;
    }
}