using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace WWTEST
{
    /// <summary>
    /// Manage the cards position and their displayed status
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeckController : MonoBehaviour
    {
        DeckManager deckMng;

        List<CardValue> deck = new List<CardValue>();
        List<CardBehaviour> deckGraphic = new List<CardBehaviour>();
        public bool IsTopCardFrontSide {
            get
            {
                if(deckGraphic.Count > 0)
                {
                    return deckGraphic.Last().FrontFaced;
                }

                return false;
            }
        }
        public DeckType deckType { get; private set; }

        public void Init(DeckManager _mng, List<CardValue> _deck, DeckType _type)
        {
            deckMng = _mng;

            deck.Clear();
            deck = _deck;
            deckType = _type;

            if(deckGraphic.Count > 0)
                for (int i = 0; i < deckGraphic.Count; i++)
                {
                    Destroy(deckGraphic[i].gameObject);
                }

            deckGraphic.Clear();
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = GameManager.RNG.Next(n + 1);
                CardValue value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        [Tooltip("OffSet between cards when displayed horizontal")]
        public float HorizontalOffSet = .5f;
        [Tooltip("OffSet between cards when displayed vertical")]
        public float VerticalOffSet = .5f;
        /// <summary>
        /// Update the graphic displacement of the cards
        /// </summary>
        public void OrderCards()
        {
            //Behaviour specific for the drawned cards
            if(deckType == DeckType.DrawnCards)
            {
                if(deckGraphic.Count > 3)
                {
                    for (int i = 0; i < deckGraphic.Count - 3; i++)
                    {
                        Destroy(deckGraphic[i].gameObject);
                    }

                    deckGraphic = deckGraphic.Skip(deckGraphic.Count -3).ToList();
                }
                else if(deck.Count >= 3)
                {
                    DisplayCard(deck[deck.Count - 3], false);
                    deckGraphic.Last().Flip();
                    deckGraphic.Insert(0, deckGraphic.Last());
                    deckGraphic.RemoveAt(deckGraphic.Count - 1);
                    OrderCards();
                    return;
                }

                for (int i = 0; i < deckGraphic.Count; i++)
                {
                    deckGraphic[i].Move(transform.position + Vector3.right * i * HorizontalOffSet + Vector3.back * i * 0.01f);
                }
            }
            //Behaviour specific for the columned cards
            else if(deckType == DeckType.Column)
            {
                for (int i = 0; i < deckGraphic.Count; i++)
                {
                    deckGraphic[i].Move(transform.position + Vector3.down * i * VerticalOffSet + Vector3.back * i * 0.01f);
                }
            }
            else
            {
                for (int i = 0; i < deckGraphic.Count; i++)
                {
                    deckGraphic[i].Move(transform.position + Vector3.back * (i+1) * 0.01f);
                }
            }
        }

        /// <summary>
        /// Display a card on the top of the deck
        /// </summary>
        public void DisplayCard(CardValue _cardValue, bool _overrideLast)
        {
            if(_overrideLast && deckGraphic.Count > 0)
            {
                //Prevent override of nothing
                if (deckGraphic.Count == 0)
                {
                    DisplayCard(_cardValue, false);
                    return;
                }

                deckGraphic.Last().SetValue(_cardValue);
            }
            else
            {
                CardBehaviour cardFacedDown = Instantiate(GameManager.I.CardPrefab, transform).GetComponent<CardBehaviour>();
                cardFacedDown.Init(_cardValue);

                deckGraphic.Add(cardFacedDown);
            }
        }

        /// <summary>
        /// Display a card on the top of the deck
        /// If _overrideLast == true, _card will be destroyed
        /// </summary>
        /// <param name="_card"></param>
        /// <param name="_overrideLast"></param>
        public void DisplayCard(CardBehaviour _card, bool _overrideLast)
        {
            if (_overrideLast && deckGraphic.Count > 0)
            {
                //Prevent override of nothing
                if (deckGraphic.Count == 0)
                {
                    DisplayCard(_card, false);
                    return;
                }

                deckGraphic.Last().SetValue(_card.GetValue());
                Destroy(_card.gameObject);
            }
            else
            {
                deckGraphic.Add(_card);
            }
        }

        /// <summary>
        /// Draw a card from the top of the deck
        /// </summary>
        public void DrawCard(bool _overrideLast, bool _doFlip)
        {
            //Create graphic for the card to flip
            if(deck.Count > 0)
                DisplayCard(deck.Last(), _overrideLast);
            //Turn and show the card
            if(_doFlip && deckGraphic.Count > 0)
                deckGraphic.Last().Flip();
        }

        /// <summary>
        /// Transfer the card from this deck to another
        /// </summary>
        /// <param name="_newDeck"></param>
        public void TransferTopCard(DeckController _newDeck)
        {
            CardBehaviour card = RemoveTopCard();
            _newDeck.AddTopCard(card);
        }

        /// <summary>
        /// Add a card to the deck
        /// </summary>
        /// <param name="_newCard"></param>
        public void AddTopCard(CardBehaviour _newCard)
        {
            deck.Add(_newCard.GetValue());
            _newCard.transform.parent = transform;

            DisplayCard(_newCard, false);

            OrderCards();
        }

        /// <summary>
        /// Remove the card on the top of the deck
        /// Return null if there are none
        /// </summary>
        /// <returns></returns>
        public CardBehaviour RemoveTopCard()
        {
            if (deck.Count > 0)
                deck.RemoveAt(deck.Count-1);

            if (deckGraphic.Count > 0)
            {
                CardBehaviour card = deckGraphic.Last();
                deckGraphic.RemoveAt(deckGraphic.Count - 1);
                return card;
            }

            return null;
        }

        /// <summary>
        /// Return the value of the card on top
        /// </summary>
        /// <returns></returns>
        public CardValue GetTopCardValue()
        {
            if (deckGraphic.Count > 0)
                return deckGraphic.Last().GetValue();
            else
                return new CardValue();
        }

        /// <summary>
        /// Return the card on the top of the deck
        /// </summary>
        /// <returns></returns>
        public CardBehaviour GetTopCard()
        {
            if (deckGraphic.Count > 0)
                return deckGraphic.Last();
            else
                return null;
        }

        /// <summary>
        /// Return a list of CardBehaviour where care are front faced
        /// </summary>
        /// <returns></returns>
        public List<CardBehaviour> GetFrontSizedCards()
        {
            List<CardBehaviour> cardsFS = new List<CardBehaviour>();
            if (deckGraphic.Count <= 0)
                return cardsFS;

            cardsFS = deckGraphic.Where(c => c.FrontFaced == true).ToList();
            return cardsFS;
        }

        #region Collider based Input
        private void OnMouseUpAsButton()
        {
            deckMng.OnInputDonwAndUpRecived(this);
        }

        private void OnMouseDown()
        {
            deckMng.OnInputDownRecived(this);
        }

        private void OnMouseEnter()
        {
            deckMng.OnInputEnterRecived(this);
        }

        private void OnMouseExit()
        {
            deckMng.OnInputExit(this);
        }
        #endregion
        public enum DeckType
        {
            Main,
            DrawnCards,
            Column,
            Seed
        }
    }
}
