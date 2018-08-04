using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    /// <summary>
    /// Manage the cards position and their displayed status
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeckController : MonoBehaviour
    {
        BoxCollider2D inputCollider;
        DeckManager deckMng;

        Stack<CardValue> deck = new Stack<CardValue>();
        DeckType deckType;

        public void Init(DeckManager _mng, Stack<CardValue> _deck, DeckType _type)
        {
            inputCollider = GetComponent<BoxCollider2D>();
            deckMng = _mng;

            deck = _deck;
            deckType = _type;
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
                cardBottom.Init(this, new CardValue());
            }

            //Graphic for the card on the top of the deck
            ShowTopCard();
        }

        public void DrawCard()
        {
            cardTop.Flip();
        }

        public void RemoveTopCard()
        {
            if (deck.Count <= 0)
                return;

            deck.Pop();
            ShowTopCard();
        }

        /// <summary>
        /// Adjust graphic and values for the card on the top
        /// </summary>
        public void ShowTopCard()
        {
            if (cardTop == null)
            {
                cardTop = Instantiate(GameManager.I.CardPrefab, transform).GetComponent<CardBehaviour>();
                cardTop.Init(this, deck.Peek());
            }
            else
                cardTop.SetValue(deck.Peek());
        }

        public enum DeckType
        {
            Main,
            DrawnCards,
            Column,
            Seed
        }
    }
}
