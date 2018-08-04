using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    /// <summary>
    /// Manage the cards position and their displayed status
    /// </summary>
    public class DeckController : MonoBehaviour
    {
        Stack<CardValue> deck = new Stack<CardValue>();

        public void Init(Stack<CardValue> _deck)
        {
            deck = _deck;
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            //Cast to list and shuffle
            List<CardValue> listedDeck = deck.ToList();
            int n = listedDeck.Count;
            while (n > 1)
            {
                n--;
                int k = GameManager.RNG.Next(n + 1);
                CardValue value = listedDeck[k];
                listedDeck[k] = listedDeck[n];
                listedDeck[n] = value;
            }

            //Cast to stack
            Stack<CardValue> shuffledDeck = new Stack<CardValue>();
            for (int i = 0; i < listedDeck.Count; i++)
            {
                shuffledDeck.Push(listedDeck[i]);
            }

            deck = shuffledDeck;
        }

        CardBehaviour cardBottom;
        CardBehaviour cardTop;
        public void DisplayDownFaced()
        {
            if (deck.Count <= 0)
                return;

            //Create graphic for the bottom deck card
            if (cardBottom == null)
            {
                cardBottom = Instantiate(GameManager.I.CardPrefab, transform).GetComponent<CardBehaviour>();
                cardBottom.SetInteractable(false);
            }

            //Graphic for the card on the top of the deck
            if(cardTop == null)
            {
                cardTop = Instantiate(GameManager.I.CardPrefab, transform).GetComponent<CardBehaviour>();
            }
        }
    }
}
