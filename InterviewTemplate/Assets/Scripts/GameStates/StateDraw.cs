using UnityEngine;

namespace VideoPoker
{
    public class StateDraw : GameState
    {
        public StateDraw(GameManager gameManager) : base(gameManager){}

        public delegate void DrawStateEntered();
        public static event DrawStateEntered BeginDrawState;

        public override void OnStateEntered()
        {
            Debug.Log("Draw State Entered");

            if (BeginDrawState != null)
            {
                BeginDrawState();
            }
        }
    }
}