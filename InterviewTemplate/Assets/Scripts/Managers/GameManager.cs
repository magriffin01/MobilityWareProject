using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		[HideInInspector] public StateDeal stateDeal;

		[HideInInspector] public StateDraw stateDraw;

		[HideInInspector] public StateWin stateWin;

		private GameState currentState;
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
			stateDeal = new StateDeal(this);
			stateDraw = new StateDraw(this);
			stateWin = new StateWin(this);
		}
		

		public void NewGameState(GameState newState)
		{
			currentState = newState;
			currentState.OnStateEntered();
		}
		
	}
}