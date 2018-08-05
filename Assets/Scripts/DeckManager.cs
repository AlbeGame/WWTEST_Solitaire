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
            List<CardValue> deck = new List<CardValue>();

            for (uint i = 0; i < 4; i++)
                for (uint j = 1; j < 14; j++)
                    deck.Add(new CardValue(j, i));

            DeckMain.Init(this, deck, DeckController.DeckType.Main);
            DeckMain.Shuffle();
            DeckMain.DisplayCard(new CardValue(), false);

            DeckColumn1.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn1, 1);
            DeckColumn2.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn2, 2);
            DeckColumn3.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn3, 3);
            DeckColumn4.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn4, 4);
            DeckColumn5.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn5, 5);
            DeckColumn6.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn6, 6);
            DeckColumn7.Init(this, new List<CardValue>(), DeckController.DeckType.Column);
            GiveStartingCards(DeckColumn7, 7);
        }

        /// <summary>
        /// Give starting cards
        /// One front faced and the others down faced
        /// </summary>
        /// <param name="_dCtrl"></param>
        /// <param name="_amount"></param>
        public void GiveStartingCards(DeckController _dCtrl, short _amount)
        {
            while(_amount > 1)
            {
                DeckMain.DrawCard(false);
                DeckMain.TransferTopCard(_dCtrl);
                _amount--;
            }

            if(_amount == 1)
            {
                DeckMain.DrawCard(true);
                DeckMain.TransferTopCard(_dCtrl);
                return;
            }

            return;
        }
    }
}