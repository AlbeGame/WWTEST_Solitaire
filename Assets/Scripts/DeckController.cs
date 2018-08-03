using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    public class DeckController : MonoBehaviour
    {
        private static System.Random rng = new System.Random();

        Stack<CardValue> deck = new Stack<CardValue>();
        Stack<CardValue> drawnCards = new Stack<CardValue>();

        public void Init()
        {
            //Create a new deck
            deck.Clear();

            for (uint i = 0; i < 4; i++)
                for (uint j = 1; j < 14; j++)
                    deck.Push(new CardValue(j, i));

            //Shuffle the deck
            Shuffle();
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
                int k = rng.Next(n + 1);
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
    }
}