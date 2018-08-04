using UnityEngine;
using DG.Tweening;

namespace WWTEST
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CardBehaviour : MonoBehaviour
    {
        Sprite front { get { return GameManager.I.CardsData.Front; } }
        Sprite back { get { return GameManager.I.CardsData.Back; } }

        DeckController deckController;
        SpriteRenderer spriteRnd;
        BoxCollider2D boxColl;
        bool frontFaced;

        CardValue value;
        
        public void Init()
        {
            spriteRnd = GetComponent<SpriteRenderer>();
            boxColl = GetComponent<BoxCollider2D>();
        }

        public void Init(DeckController _dCtrl, CardValue _value)
        {
            deckController = _dCtrl;
            value = _value;
            Init();
        }

        Tween tweenFlip;
        float flipTime = 0.1f;
        public void Flip()
        {
            frontFaced = !frontFaced;

            SetInteractable(false);

            //Tween that manage rotation and sprite change
            tweenFlip = transform.DORotate(new Vector3(0, 90, 0), flipTime/2).OnComplete(() =>
            {
                spriteRnd.sprite = frontFaced ? front : back;
                transform.DORotate(Vector3.zero, flipTime/2).OnComplete(()=> 
                {
                    SetInteractable(true);
                });
            });
        }

        /// <summary>
        /// If _interactable = true, than the card is sensible to click and touch
        /// </summary>
        /// <param name="_interactable"></param>
        public void SetInteractable(bool _interactable)
        {
            if (_interactable)
                boxColl.enabled = true;
            else
                boxColl.enabled = false;
        }

        private void OnMouseUpAsButton()
        {
        }
    }

    /// <summary>
    /// A value that a card could have
    /// Number = 0 means that the card has no proper value
    /// (i.e. is used to show the backside for graphic pourpose only)
    /// Seeds: 0 = clubs, 1 = diamonds, 2 = hearts, 3 = spades
    /// </summary>
    public struct CardValue
    {
        public uint Number;
        public uint Seed; 

        public CardValue(uint _num, uint _seed)
        {
            Number = _num;
            Seed = _seed;
        }
    }
}
