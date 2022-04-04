using UnityEngine;

namespace VideoPoker
{
    public class StateDeal : GameState
    {
        public StateDeal(GameManager gameManager) : base(gameManager){}
        
        public delegate void DealStateEntered();
        public static event DealStateEntered BeginDealState;

        public override void OnStateEntered()
        {
            Debug.Log("Entered StateDeal");
            if (BeginDealState != null)
            {
                BeginDealState();
            }
        }
    }
}

