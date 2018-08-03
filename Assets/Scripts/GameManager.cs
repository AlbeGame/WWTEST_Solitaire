using UnityEngine;

namespace WWTEST
{
    public class GameManager : MonoBehaviour
    {
        public DeckData cardsData;
        public static GameManager I { get; private set; }

        private void Awake()
        {
            if (GameManager.I != null)
                DestroyImmediate(this.gameObject);
            else
                I = this;
        }

        [System.Serializable]
        public struct DeckData
        {
            public Sprite Back;
            public Sprite Front;
        }
    }
}
