using UnityEngine;

namespace WWTEST
{
    public class GameManager : MonoBehaviour
    {
        public GameObject CardPrefab;
        public DeckData CardsData;
        public static GameManager I { get; private set; }
        public static System.Random RNG { get; private set; }
        public DeckManager DeckCtrl { get; private set; }
        public MovesController MoveCtrl { get; private set; }
        public InterfaceController InterfaceCtrl { get; private set; }

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
            StartGame();
        }

        public void StartGame()
        {
            //Initialize the deck controller
            DeckCtrl = GetComponent<DeckManager>();
            if (DeckCtrl == null)
                DeckCtrl = gameObject.AddComponent<DeckManager>();

            DeckCtrl.Init();

            //Initialize the move controller
            MoveCtrl = GetComponent<MovesController>();
            if (MoveCtrl == null)
                MoveCtrl = gameObject.AddComponent<MovesController>();

            MoveCtrl.Init();

            //Initialize the interface controller
            InterfaceCtrl = FindObjectOfType<InterfaceController>();
            InterfaceCtrl.Init();
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
            public Sprite JackRed;
            public Sprite JackBlack;
            public Sprite QueenRed;
            public Sprite QueenBlack;
            public Sprite KingRed;
            public Sprite KingBlack;
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
