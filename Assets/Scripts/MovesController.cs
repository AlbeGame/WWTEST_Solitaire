using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace WWTEST
{
    /// <summary>
    /// Class that stores the player moves
    /// and "undo" the last one.
    /// </summary>
    public class MovesController : MonoBehaviour {

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
            public DeckController InitialDeck;
            public DeckController EndingDeck;

            public Move(int _points, CardBehaviour _card, DeckController _initialDeck, DeckController _endingDeck)
            {
                PointsGiven = _points;
                Card = _card;
                InitialDeck = _initialDeck;
                EndingDeck = _endingDeck;
            }
        }
    }
}
