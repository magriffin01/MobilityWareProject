using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public ScoreManager scoreManager;
		
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

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
			if (currentState == stateDraw)
			{
				if (scoreManager.CheckForWin())
				{
					NewGameState(stateWin);
				}
			}
		}

		public void NewGameState(GameState newState)
		{
			if (currentState != null)
			{
				currentState.OnStateExit();
			}

			currentState = newState;
			currentState.OnStateEntered();
		}
		
	}
}