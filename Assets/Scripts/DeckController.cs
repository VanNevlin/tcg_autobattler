using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public int maxDeckSize = 15;

    private void Start()
    {
        ShuffleDeck();
    }

    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            ReshuffleDiscardPile();
        }

        if (deck.Count > 0)
        {
            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            return drawnCard;
        }

        return null; // No cards left.
    }

    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void ReshuffleDiscardPile()
    {
        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }
}
