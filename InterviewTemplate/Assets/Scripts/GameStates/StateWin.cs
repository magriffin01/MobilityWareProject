using UnityEngine;

namespace VideoPoker
{
    public class StateWin : GameState
    {
        public StateWin(GameManager gameManager) : base(gameManager){}
        
        public delegate void WinStateEntered();
        public static event WinStateEntered BeginWinState;

        public override void OnStateEntered()
        {
            Debug.Log("Win State Entered");

            if (BeginWinState != null)
            {
                BeginWinState();
            }
        }
    }
}
