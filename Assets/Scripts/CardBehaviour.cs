using UnityEngine;
using DG.Tweening;

namespace WWTEST
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CardBehaviour : MonoBehaviour
    {
        Sprite front { get { return GameManager.I.cardsData.Front; } }
        Sprite back { get { return GameManager.I.cardsData.Back; } }

        SpriteRenderer spriteRnd;
        BoxCollider2D boxColl;
        bool frontFaced;
        
        // Use this for initialization
        void Start()
        {
            spriteRnd = GetComponent<SpriteRenderer>();
            boxColl = GetComponent<BoxCollider2D>();
        }

        Tween tweenFlip;
        float flipTime = 0.1f;
        public void Flip()
        {
            frontFaced = !frontFaced;
            boxColl.enabled = false;
            //Tween that manage rotation and sprite change
            tweenFlip = transform.DORotate(new Vector3(0, 90, 0), flipTime/2).OnComplete(() =>
            {
                spriteRnd.sprite = frontFaced ? front : back;
                transform.DORotate(Vector3.zero, flipTime/2).OnComplete(()=> 
                {
                    boxColl.enabled = true;
                });
            });
        }

        private void OnMouseUpAsButton()
        {
            Flip();
        }
    }

    public struct CardValue
    {
        public byte Number;
        public byte Seed; 
    }
}
