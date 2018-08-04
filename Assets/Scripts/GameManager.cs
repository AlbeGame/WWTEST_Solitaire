using UnityEngine;

namespace WWTEST
{
    public class GameManager : MonoBehaviour
    {
        public GameObject CardPrefab;
        public DeckData CardsData;
        public static GameManager I { get; private set; }
        public static System.Random RNG { get; private set; }
        DeckManager deckCtrl;

        private void Awake()
        {
            if (GameManager.I != null)
                DestroyImmediate(this.gameObject);
            else
            {
                I = this;
                RNG = new System.Random();
            }
        }

        private void Start()
        {
            deckCtrl = GetComponent<DeckManager>();
            if (deckCtrl == null)
                deckCtrl = gameObject.AddComponent<DeckManager>();

            deckCtrl.Init();
        }

        [System.Serializable]
        public struct DeckData
        {
            [Header ("Base")]
            public Sprite Back;
            public Sprite Front;
            [Header ("Seeds")]
            public Sprite Clubs;
            public Sprite Diamonds;
            public Sprite Hearts;
            public Sprite Spades;
            [Header("Numbers")]
            public Sprite Ace;
            public Sprite Two;
            public Sprite Three;
            public Sprite Four;
            public Sprite Five;
            public Sprite Six;
            public Sprite Seven;
            public Sprite Eight;
            public Sprite Nine;
            public Sprite Ten;
            public Sprite Jack;
            public Sprite Queen;
            public Sprite King;
        }
    }
}
