using UnityEngine;
using DG.Tweening;

namespace WWTEST
{
    public class CardBehaviour : MonoBehaviour
    {
        Sprite front { get { return GameManager.I.CardsData.Front; } }
        Sprite back { get { return GameManager.I.CardsData.Back; } }

        public Color ColorRed = Color.red;
        public Color ColorBlack = Color.black;
        public SpriteRenderer SpriteRndBase;
        public SpriteRenderer SpriteRndCenter;
        public SpriteRenderer SpriteRndTopRight;
        public SpriteRenderer SpriteRndTopLeft;
        public bool FrontFaced { get; private set; }

        CardValue value;

        public void Init(CardValue _value)
        {
            SetValue(_value);
        }

        Tween tweenFlip;
        float flipTime = 0.1f;
        /// <summary>
        /// Flip the card and show the front or the rear either
        /// </summary>
        public void Flip()
        {
            FrontFaced = !FrontFaced;

            if (tweenFlip != null && tweenFlip.IsPlaying())
                tweenFlip.Complete();

            //Tween that manage rotation and sprite change
            tweenFlip = transform.DORotate(new Vector3(0, 90, 0), flipTime/2).OnComplete(() =>
            {
                if (!FrontFaced)
                {
                    SpriteRndBase.sprite = back;
                    SpriteRndCenter.enabled = false;
                    SpriteRndTopRight.enabled = false;
                    SpriteRndTopLeft.enabled = false;
                }
                else
                {
                    SpriteRndBase.sprite = front;
                    SpriteRndCenter.enabled = true;
                    SpriteRndTopRight.enabled = true;
                    SpriteRndTopLeft.enabled = true;
                }

                transform.DORotate(Vector3.zero, flipTime / 2);
            });
        }

        Tween tweenMove;
        float moveTime = 0.2f;
        /// <summary>
        /// Place the card at _targetPos
        /// </summary>
        /// <param name="_targetPos"></param>
        public void Move(Vector3 _targetPos)
        {
            if (tweenMove != null && tweenMove.IsPlaying())
                tweenMove.Complete();

            tweenMove = transform.DOMove(_targetPos, moveTime);
        }

        /// <summary>
        /// Cange the value of the card
        /// and the graphic as well
        /// </summary>
        /// <param name="_newValue"></param>
        public void SetValue(CardValue _newValue)
        {
            value = _newValue;

            if (value.Number == 0)
                return;

            //Assign TopLeft sprite
            SetTopLeftSprite(value);

            //Assign TopRight sprite
            SetTopRightSprite(value);

            //Assign Center sprite
            SetCenterSprite(value);
        }

        /// <summary>
        /// Return the value of the card
        /// </summary>
        /// <returns></returns>
        public CardValue GetValue()
        {
            return value;
        }

        /// <summary>
        /// Get the proper sprite from the datas (in GameManager)
        /// and assign it to the renderer
        /// </summary>
        /// <param name="_value"></param>
        void SetTopLeftSprite(CardValue _value)
        {
            switch (_value.Number)
            {
                case 1:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Ace;
                    SpriteRndTopLeft.color =  _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 2:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Two;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 3:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Three;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 4:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Four;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 5:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Five;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 6:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Six;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 7:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Seven;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 8:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Eight;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 9:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Nine;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 10:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Ten;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 11:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Jack;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 12:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Queen;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                case 13:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.King;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
                default:
                    SpriteRndTopLeft.sprite = GameManager.I.CardsData.Ace;
                    SpriteRndTopLeft.color = _value.Color == CardColor.Red ? ColorRed : ColorBlack;
                    break;
            }
        }
        /// <summary>
        /// Get the proper sprite from the datas (in GameManager)
        /// and assign it to the renderer
        /// </summary>
        /// <param name="_value"></param>
        void SetTopRightSprite(CardValue _value)
        {
            switch (_value.Seed)
            {
                case 0:
                    SpriteRndTopRight.sprite = GameManager.I.CardsData.Clubs;
                    break;
                case 1:
                    SpriteRndTopRight.sprite = GameManager.I.CardsData.Diamonds;
                    break;
                case 2:
                    SpriteRndTopRight.sprite = GameManager.I.CardsData.Hearts;
                    break;
                case 3:
                    SpriteRndTopRight.sprite = GameManager.I.CardsData.Spades;
                    break;
                default:
                    SpriteRndTopRight.sprite = GameManager.I.CardsData.Clubs;
                    break;
            }
        }
        /// <summary>
        /// Get the proper sprite from the datas (in GameManager)
        /// and assign it to the renderer
        /// </summary>
        /// <param name="_value"></param>
        void SetCenterSprite(CardValue _value)
        {
            switch (_value.Number)
            {
                case 11:
                    if(_value.Color == CardColor.Red)
                        SpriteRndCenter.sprite = GameManager.I.CardsData.JackRed;
                    else
                        SpriteRndCenter.sprite = GameManager.I.CardsData.JackBlack;
                    break;
                case 12:
                    if (_value.Color == CardColor.Red)
                        SpriteRndCenter.sprite = GameManager.I.CardsData.QueenRed;
                    else
                        SpriteRndCenter.sprite = GameManager.I.CardsData.QueenBlack;
                    break;
                case 13:
                    if (_value.Color == CardColor.Red)
                        SpriteRndCenter.sprite = GameManager.I.CardsData.KingRed;
                    else
                        SpriteRndCenter.sprite = GameManager.I.CardsData.KingBlack;
                    break;
                default:
                    switch (_value.Seed)
                    {
                        case 0:
                            SpriteRndCenter.sprite = GameManager.I.CardsData.Clubs;
                            break;
                        case 1:
                            SpriteRndCenter.sprite = GameManager.I.CardsData.Diamonds;
                            break;
                        case 2:
                            SpriteRndCenter.sprite = GameManager.I.CardsData.Hearts;
                            break;
                        case 3:
                            SpriteRndCenter.sprite = GameManager.I.CardsData.Spades;
                            break;
                        default:
                            break;
                    }
                    break;
            }
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
        public CardColor Color { get { return (Seed == 1 || Seed == 2) ? CardColor.Red : CardColor.Black; } }

        public CardValue(uint _num, uint _seed)
        {
            Number = _num;
            Seed = _seed;
        }
    }

    public enum CardColor
    {
        Black,
        Red
    }
}
