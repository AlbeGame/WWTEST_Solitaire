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
        public List<DeckController> DecksSeed = new List<DeckController>();
        [Header("Columns Decks")]
        public List<DeckController> DecksColumn = new List<DeckController>();

        public bool DrawThreeRule;
        public void SetDrawThreeRule(bool _value) { DrawThreeRule = _value; }
        public bool IsPaused;

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
            for (int i = 0; i < DecksSeed.Count; i++)
            {
                DecksSeed[i].Init(this, new List<CardValue>(), DeckController.DeckType.Seed);
            }
            //Init of the column decks
            for (int i = 0; i < DecksColumn.Count; i++)
            {
                DecksColumn[i].Init(this, new List<CardValue>(), DeckController.DeckType.Column);
                GiveStartingCards(DecksColumn[i], (short)(i+1));
            }
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
            if (currentDeckInputOver != null)
            {
                if (currentDeckInputOver.deckType == DeckController.DeckType.Column)
                {
                    //Check if the card added is one more greater than actual and if they are of the same seed
                    if (CheckIfCardMatch(currentDeckInputOver.GetTopCardValue(), draggedCard.GetValue(), true))
                    {
                        GameManager.I.MoveCtrl.RecordMove(0, draggedCard, draggedCardOriginalDeck, currentDeckInputOver);
                        //Assing 5 points from a card that come in column from outside
                        if (draggedCardOriginalDeck.deckType != DeckController.DeckType.Column)
                        {
                            AssignPoints(5);
                            GameManager.I.MoveCtrl.GetLastMove().PointsGiven = 5;
                        }

                        //Assign a move
                        AssignMove();

                        currentDeckInputOver.AddTopCard(draggedCard);
                        ReleaseDraggedCard(false);
                    }
                }
                else if (currentDeckInputOver.deckType == DeckController.DeckType.Seed)
                {
                    //Check if the card added is one less than actual and if they are of the same seed
                    if (CheckIfCardMatch(currentDeckInputOver.GetTopCardValue(), draggedCard.GetValue(), false))
                    {
                        //Assign 10 points for a card in the seed decks
                        AssignPoints(10);

                        GameManager.I.MoveCtrl.RecordMove(10, draggedCard, draggedCardOriginalDeck, currentDeckInputOver);

                        //Assign a move
                        AssignMove();

                        currentDeckInputOver.AddTopCard(draggedCard);
                        ReleaseDraggedCard(false);
                    }
                }
            }

            ReleaseDraggedCard(true);
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
            else if (draggedCardOriginalDeck.deckType == DeckController.DeckType.Column)
            {
                if (draggedCardOriginalDeck.IsTopCardFrontSide)                          //Add the card to the new deck and flip the top   
                    draggedCardOriginalDeck.DrawCard(true, false);                      //card of the original deck if it wasn't already flipped (front side)
                else
                {
                    AssignPoints(5);                                                    //Special case: assign 5 points if a card in column is reveald
                    draggedCardOriginalDeck.DrawCard(true, true);
                }
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
        /// Assign points to interface
        /// </summary>
        /// <param name="_amount"></param>
        private void AssignPoints(int _amount)
        {
            GameManager.I.InterfaceCtrl.AddPoints(_amount);
        }

        /// <summary>
        /// Add 1 move to interface
        /// </summary>
        private void AssignMove()
        {
            GameManager.I.InterfaceCtrl.AddMoves();
        }

        /// <summary>
        /// Check main rule of solitaire
        /// if doesn't _useColumnRules than
        /// Only high card on lower and only of the same seed
        /// Otherwise (_useColumnRules == true)
        /// Lower cards on higher and seeds doesn't matter
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="_addition"></param>
        /// <returns></returns>
        public bool CheckIfCardMatch(CardValue _base, CardValue _addition, bool _useColumnRules)
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
        /// Reation to button-like input
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputDonwAndUpRecived(DeckController _deck)
        {
            if (IsPaused)
                return;

            if (_deck.deckType == DeckController.DeckType.Main)
            {
                int amount = DrawThreeRule ? 3 : 1;
                for (int i = 0; i < amount; i++)
                {
                    _deck.DrawCard(false, true);
                    _deck.TransferTopCard(DeckDrawnCards);
                }
                //It stores a move of "draw" by the main deck. Not sure if requested
                //GameManager.I.MoveCtrl.RecordMove(0, DeckDrawnCards.GetTopCard(), _deck, DeckDrawnCards);
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
            if (IsPaused)
                return;

            if (_deck.deckType == DeckController.DeckType.DrawnCards || _deck.deckType == DeckController.DeckType.Column)
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
            if (IsPaused)
                return;

            currentDeckInputOver = _deck;
        }

        /// <summary>
        /// Reaction to pointer-like input enter recived (by deck controllers)
        /// </summary>
        /// <param name="_deck"></param>
        public void OnInputExit(DeckController _deck)
        {
            if (IsPaused)
                return;

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
            while (_amount > 1)
            {
                DeckMain.DrawCard(false, false);
                DeckMain.TransferTopCard(_dCtrl);
                _amount--;
            }

            if (_amount == 1)
            {
                DeckMain.DrawCard(false, true);
                DeckMain.TransferTopCard(_dCtrl);
                return;
            }

            return;
        }
    }
}