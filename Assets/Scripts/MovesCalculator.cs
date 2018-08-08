using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WWTEST
{
    public static class MovesCalculator
    {
        static List<DeckController> decksToEvaluate = new List<DeckController>();

        public static void Init(List<DeckController> _decksToEvaluate)
        {
            decksToEvaluate = _decksToEvaluate;
        }

        /// <summary>
        /// Return the first avaiable move
        /// </summary>
        /// <returns></returns>
        public static MovesController.Move GetHint()
        {
            DeckManager deckMng = GameManager.I.DeckMng;
            CardValue evaluatingCard;
            bool useColumnRule;
            
            for (int i = 0; i < decksToEvaluate.Count; i++)
            {
                //Get the top card of each deck but the seed
                if(decksToEvaluate[i].deckType != DeckController.DeckType.Seed)
                {
                    evaluatingCard = decksToEvaluate[i].GetTopCardValue();
                    //Compare it with each card of other decks but drawn cards
                    for (int j = 0; j < decksToEvaluate.Count; j++)
                    {
                        if(decksToEvaluate[j].deckType != DeckController.DeckType.DrawnCards)
                        {
                            //Adapt evaluation rule to destination deck
                            if (decksToEvaluate[j].deckType == DeckController.DeckType.Column)
                                useColumnRule = true;
                            else
                                useColumnRule = false;

                            //If the move is possible, returns it
                            if (deckMng.CheckIfCardMatch(decksToEvaluate[j].GetTopCardValue(), evaluatingCard, useColumnRule))
                            {
                                return new MovesController.Move(0, decksToEvaluate[i].GetTopCard(), decksToEvaluate[i], decksToEvaluate[j]);
                            }
                        }
                    }
                }
            }

            //Return null if there are no avaiable moves
            return null;
        }
    }
}
