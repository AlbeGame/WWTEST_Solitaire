using UnityEngine;
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
            //Init of the Main Deck
            DeckMain.Init(this, deck, DeckController.DeckType.Main);
            //Shuffle 10 times. Why not?
            for (int i = 0; i < 10; i++) 
            {
                DeckMain.Shuffle();
            }
            DeckMain.DisplayCard(new CardValue(), false);
            //Init of the drawned cards deck
            DeckDrawnCards.Init(this, new List<CardValue>(), DeckController.DeckType.DrawnCards);
            //Init of the seed decks
            DeckClubs.Init(this, new List<CardValue>(), DeckController.DeckType.Seed);
            DeckDiamonds.Init(this, new List<CardValue>(), DeckController.DeckType.Seed);
            DeckHearts.Init(this, new List<CardValue>(), DeckController.DeckType.Seed);
            DeckSpades.Init(this, new List<CardValue>(), DeckController.DeckType.Seed);
            //Init of the column decks
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

        bool isDragging;
        CardBehaviour draggedCard;
        DeckController draggedCardOriginalDeck;
        DeckController currentDeckInputOver;

        public void LateUpdate()
        {
            if (isDragging && draggedCard != null)
            {
                draggedCard.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedCard.transform.position = new Vector3(draggedCard.transform.position.x, draggedCard.transform.position.y, -Camera.main.nearClipPlane - 0.01f);

                if (Input.GetMouseButtonUp(0))
                    OnInputRelease();
            }
        }

        /// <summary>
        /// Reaction to be called when pointer-like input is released
        /// </summary>
        private void OnInputRelease()
        {
            if(currentDeckInputOver != null)
            {
                if (currentDeckInputOver.deckType == DeckController.DeckType.Column)
                {
                    //Check if the card added is one more greater than actual and if they are of the same seed
                    if (CheckIfCardMatch(currentDeckInputOver.GetTopCardValue(), draggedCard.GetValue(), true))
                    {
                        currentDeckInputOver.AddTopCard(draggedCard);
                        ReleaseDraggedCard(false);
                    }
                }
                else if(currentDeckInputOver.deckType == DeckController.DeckType.Seed)
                {
                    //Check if the card added is one less than actual and if they are of the same seed
                    if (CheckIfCardMatch(currentDeckInputOver.GetTopCardValue(), draggedCard.GetValue(), false))
                    {
                        currentDeckInputOver.AddTopCard(draggedCard);
                        ReleaseDraggedCard(false);
                    }
                }
            }

            ReleaseDraggedCard(true);
        }

        /// <summary>
        /// Check main rule of solitaire
        /// Only high card on lower
        /// Only if of the same seed
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="_addition"></param>
        /// <returns></returns>
        private bool CheckIfCardMatch(CardValue _base, CardValue _addition, bool _useColumnRules)
        {
            //False if backsided card
            if (_addition.Number == 0)
                return false;

            if (_useColumnRules)
            {
                //Always allow a card drop on a empty column
                if (_base.Number == 0)
                    return true;

                if (_base.Number != _addition.Number + 1)
                    return false;
            }
            else
            {
                //Ace exception
                if (_base.Number == 0 && _addition.Number == 1)
                    return true;

                //Cards have to be of the same seed only for the seed decks
                if (_base.Seed != _addition.Seed)
                    return false;

                if (_base.Number != _addition.Number - 1)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Return the card to original deck
        /// </summary>
        private void ReleaseDraggedCard(bool _giveBackToOriginalDeck)
        {
            if (draggedCard == null)
                return;

            if (_giveBackToOriginalDeck)
                draggedCardOriginalDeck.AddTopCard(draggedCard);                        //Return the card to original Deck
            else if(draggedCardOriginalDeck.deckType == DeckController.DeckType.Column)
            {
                if(draggedCardOriginalDeck.IsTopCardFrontSide)                          //Add the card to the new deck and flip the top   
                    draggedCardOriginalDeck.DrawCard(true, false);                      //card of the original deck if it wasn't already flipped (front side)
                else
                    draggedCardOriginalDeck.DrawCard(true, true);
            }
            else if (draggedCardOriginalDeck.deckType == DeckController.DeckType.DrawnCards)
            {
                draggedCardOriginalDeck.DrawCard(true, false);                          //Never flip the drawn cards (they are already fron side)
                draggedCardOriginalDeck.OrderCards();                                   //Always update their displacement
            }

            draggedCard = null;
            isDragging = false;
        }

        /// <summary>
        /// Reation to button-like input
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputDonwAndUpRecived(DeckController _deck)
        {
            if (_deck.deckType == DeckController.DeckType.Main)
            {
                _deck.DrawCard(false, true);
                _deck.TransferTopCard(DeckDrawnCards);
            }
            //Previous system where player has to flip the card by himself
            //if(_deck.deckType == DeckController.DeckType.Column)
            //{
            //    if(!_deck.IsTopCardFrontSide)
            //        _deck.DrawCard(true, true);
            //}
        }

        /// <summary>
        /// Reaction to pointer-like input down recived (by deck controllers)
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputDownRecived(DeckController _deck)
        {
            if(_deck.deckType == DeckController.DeckType.DrawnCards || _deck.deckType == DeckController.DeckType.Column)
            {
                draggedCard = _deck.RemoveTopCard();
                draggedCardOriginalDeck = _deck;
                isDragging = true;
            }
        }

        /// <summary>
        /// Reaction to pointer-like input enter recived (by deck controllers)
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputEnterRecived(DeckController _deck)
        {
            currentDeckInputOver = _deck;
        }

        /// <summary>
        /// Reaction to pointer-like input enter recived (by deck controllers)
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputExit(DeckController _deck)
        {
            currentDeckInputOver = null;
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
                DeckMain.DrawCard(false, false);
                DeckMain.TransferTopCard(_dCtrl);
                _amount--;
            }

            if(_amount == 1)
            {
                DeckMain.DrawCard(false, true);
                DeckMain.TransferTopCard(_dCtrl);
                return;
            }

            return;
        }
    }
}