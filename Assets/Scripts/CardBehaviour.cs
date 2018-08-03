using UnityEngine;
using System.Collections;

namespace WWTEST
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CardBehaviour : MonoBehaviour
    {
        public Sprite Front;
        public Sprite Back;

        SpriteRenderer spriteRnd;
        bool frontFaced;
        
        // Use this for initialization
        void Start()
        {
            spriteRnd = GetComponent<SpriteRenderer>();
        }

        public void Flip()
        {
            frontFaced = !frontFaced;

        }
    }

    public struct CardValue
    {
        public byte Number;
        public byte Seed; 
    }
}
