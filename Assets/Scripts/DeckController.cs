using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    /// <summary>
    /// Class to create and controll the cards and their position (logical)
    /// </summary>
    public class DeckController : MonoBehaviour
    {
        private static System.Random rng = new System.Random();

        Stack<CardValue> deck = new Stack<CardValue>();             //actual deck in use
        Stack<CardValue> drawnCards = new Stack<CardValue>();       //already drawn cards

        [Header("Deck Places")]
        public Transform DeckPos;
        public Transform DrawnCardsPos;

        [Header("Seeds Places")]
        public Transform ClubsPos;
        public Transform DiamondsPos;
        public Transform HeartsPos;
        public Transform SpadesPos;

        [Header("Columns Places")]
        public Transform Column1Pos;
        public Transform Column2Pos;
        public Transform Column3Pos;
        public Transform Column4Pos;
        public Transform Column5Pos;
        public Transform Column6Pos;
        public Transform Column7Pos;


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