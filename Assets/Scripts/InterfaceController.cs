using UnityEngine;
using UnityEngine.UI;

namespace WWTEST
{
    /// <summary>
    /// Class that controls the main interface
    /// </summary>
    public class InterfaceController : MonoBehaviour
    {
        public Text PointsText;
        int points;
        public Text MovesText;
        int moves;

        public GameObject PauseMenu;
        public Text Options;

        /// <summary>
        /// Initialize the class
        /// Set points and moves to 0 by default
        /// </summary>
        /// <param name="_points"></param>
        /// <param name="_moves"></param>
        public void Init(int _points = 0, int _moves = 0)
        {
            SetPoints(_points);
            SetMoves(_moves);
            ToggleOptionMenu(false);

            Options.text = "OPZIONI";
        }

        public void DisplayVictoryMenu()
        {
            Options.text = "VITTORIA";
            ToggleOptionMenu(true);
        }

        public void DisplayDefeatMenu()
        {
            Options.text = "SCONFITTA";
            ToggleOptionMenu(true);
        }

        /// <summary>
        /// If _open == true, it opens the option menu and pause the game
        /// </summary>
        /// <param name="_open"></param>
        public void ToggleOptionMenu(bool _open)
        {
            PauseMenu.SetActive(_open);
            GameManager.I.DeckMng.IsPaused = _open;
            GameManager.I.MoveCtrl.IsPaused = _open;
        }

        /// <summary>
        /// Display a hint if avaiable
        /// </summary>
        public void ShowHint()
        {
            MovesController.Move moveToShow = MovesCalculator.GetHint();
            if (moveToShow == null)
            {
                if (GameManager.I.DeckMng.DeckMain.GetTopCard() == null)
                    DisplayDefeatMenu();

                return;
            }

            moveToShow.Card.BlinkForHint();
            //Also show destination... maybe too much int
            //CardBehaviour otherCard = moveToShow.EndingDeck.GetTopCard();
            //if(otherCard != null)
            //    otherCard.BlinkForHint();
        }

        /// <summary>
        /// Set and display _points on interface
        /// </summary>
        /// <param name="_points"></param>
        public void SetPoints(int _points)
        {
            points = _points;
            PointsText.text = points.ToString();
        }

        /// <summary>
        /// Sum _points to actual amount
        /// and display in interface
        /// </summary>
        /// <param name="_points"></param>
        public void AddPoints(int _points)
        {
            SetPoints(points + _points);
        }

        /// <summary>
        /// Set and display _moves on interface
        /// </summary>
        /// <param name="_points"></param>
        public void SetMoves(int _moves)
        {
            moves = _moves;
            MovesText.text = moves.ToString();
        }

        /// <summary>
        /// Sum _moves to actual amount of moves
        /// Add 1 by default
        /// </summary>
        /// <param name="_moves"></param>
        public void AddMoves(int _moves = 1)
        {
            SetMoves(moves + _moves);
        }
    }
}
