using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class Deck
{
    List<CardHolder> deck;

    public Deck()
    {
        deck = new List<CardHolder>();
    }

    public Deck(IEnumerable<CardHolder> cards) : this()
    {
        deck.Clear();
        CardAdd(cards);
        DeckShuffle();
    }

    void DeckClear()
    {
        deck.Clear();
    }

    void DeckShuffle()
    {
        System.Random r = new System.Random();

        for (int n = deck.Count - 1; n > 0; --n)
        {
            int k = r.Next(n + 1);
            var temp = deck[n];
            deck[n] = deck[k];
            deck[k] = temp;
        }
    }

    public void CardRandom(out CardHolder card)
    {
        card = null;
        if (deck.Count > 0)
        {
            card = deck[Random.Range(0, deck.Count)];
            deck.Remove(card);
        }
    }

    public void CardAdd(IEnumerable<CardHolder> card)
    {
        deck.AddRange(card);
    }

    public void CardAdd(CardHolder card)
    {
        deck.Add(card);
    }

    public void CardRemove(CardHolder card)
    {
        deck.Remove(card);
    }
}
