using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    /// <summary>
    /// Class to create cards and manage DeckControllers
    /// </summary>
    public class DeckManager : MonoBehaviour
    {
        [Header("Main Decks")]
        public DeckController DeckMain;
        public DeckController DeckDrawnCards;
        [Header("Seed Decks")]
        public DeckController DeckClubs;
        public DeckController DeckDiamonds;
        public DeckController DeckHearts;
        public DeckController DeckSpades;
        [Header("Columns Decks")]
        public DeckController DeckColumn1;
        public DeckController DeckColumn2;
        public DeckController DeckColumn3;
        public DeckController DeckColumn4;
        public DeckController DeckColumn5;
        public DeckController DeckColumn6;
        public DeckController DeckColumn7;

        /// <summary>
        /// Initialize the class by creating a new deck
        /// The deck it's also shuffled and all the other cards on the board removed.
        /// </summary>
        public void Init()
        {
            Stack<CardValue> deck = new Stack<CardValue>();

            for (uint i = 0; i < 4; i++)
                for (uint j = 1; j < 14; j++)
                    deck.Push(new CardValue(j, i));

            DeckMain.Init(this, deck, DeckController.DeckType.Main);
            DeckMain.Shuffle();
            DeckMain.DisplayDownFaced();
        }
    }
}