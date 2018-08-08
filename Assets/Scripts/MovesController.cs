using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace WWTEST
{
    /// <summary>
    /// Class that stores the player moves
    /// and "undo" the last one.
    /// </summary>
    public class MovesController : MonoBehaviour
    {
        public bool IsPaused;

        List<Move> moves = new List<Move>();

        public void Init()
        {
            moves.Clear();
        }

        public void RecordMove(int _points, CardBehaviour _card, DeckController _startingDeck, DeckController _endingDeck)
        {
            RecordMove(new Move(_points, _card, _startingDeck, _endingDeck));
        }

        public void RecordMove(Move _move)
        {
            moves.Add(_move);
        }

        public void UndoLastMove()
        {
            if (IsPaused || moves.Count <= 0)
                return;

            Move lastMove = moves.Last();
            //Subtract eventual points given
            GameManager.I.InterfaceCtrl.AddPoints(-lastMove.PointsGiven);

            CardBehaviour card = lastMove.EndingDeck.RemoveTopCard();
            if (lastMove.InitialDeck.deckType == DeckController.DeckType.Column)
            {
                List<CardBehaviour> initiaDeckCardsFF = lastMove.InitialDeck.GetFrontSizedCards();
                if (initiaDeckCardsFF.Count == 1)
                {
                    lastMove.InitialDeck.GetTopCard().Flip();
                    GameManager.I.InterfaceCtrl.AddPoints(-5);
                }
            }

            lastMove.InitialDeck.AddTopCard(card);

            moves.RemoveAt(moves.Count - 1);
        }

        public Move GetLastMove()
        {
            if (moves.Count <= 0)
                return null;
            else
                return moves.Last();
        }

        /// <summary>
        /// Main information about the move
        /// </summary>
        public class Move
        {
            public int PointsGiven;
            public CardBehaviour Card;
            //Safe mesure in case the Card instance get lost
            public CardValue CardVal;
            public DeckController InitialDeck;
            public DeckController EndingDeck;

            public Move(int _points, CardBehaviour _card, DeckController _initialDeck, DeckController _endingDeck)
            {
                PointsGiven = _points;
                Card = _card;
                CardVal = _card.GetValue();
                InitialDeck = _initialDeck;
                EndingDeck = _endingDeck;
            }
        }
    }
}
