using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
	{
		public GameManager gameManager;
		public ScoreManager scoreManager;
		
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField] 
		private Text currentBetText;

		[SerializeField]
		private Text winningText = null;

		[SerializeField]
		private Button betButton = null;

		[SerializeField] 
		private Button dealButton = null;

		[SerializeField] 
		private Button drawButton = null;

		[SerializeField] 
		private Button resetBetButton;
		

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
			OnDealBegin();
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			betButton.onClick.AddListener(OnBetButtonPressed);
			dealButton.onClick.AddListener(OnDealButtonPressed);
			drawButton.onClick.AddListener(OnDrawButtonPressed);
			resetBetButton.onClick.AddListener(OnResetButtonPressed);
			drawButton.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			StateDeal.BeginDealState += OnDealBegin;
			StateWin.BeginWinState += OnWinBegin;
		}

		private void OnDisable()
		{
			StateDeal.BeginDealState -= OnDealBegin;
			StateWin.BeginWinState -= OnWinBegin;
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			scoreManager.Bet();
			UpdateBetText();
		}

		private void OnDealButtonPressed()
		{
			gameManager.NewGameState(gameManager.stateDeal);
			dealButton.gameObject.SetActive(false);
			drawButton.gameObject.SetActive(true);
		}

		private void OnDrawButtonPressed()
		{
			gameManager.NewGameState(gameManager.stateDraw);
			drawButton.gameObject.SetActive(false);
			dealButton.gameObject.SetActive(true);
		}

		private void OnResetButtonPressed()
		{
			scoreManager.ResetBet();
			UpdateBetText();
		}

		private void OnWinBegin()
		{
			UpdateWinningText();
			UpdateBetText();
			UpdateCurrentBalanceText();
		}

		private void OnDealBegin()
		{
			ResetWinningText();
			UpdateBetText();
			UpdateCurrentBalanceText();
		}

		private void ResetWinningText()
		{
			winningText.text = "";
		}
		
		private void UpdateWinningText()
		{
			winningText.text = scoreManager.GetWinType() + " You won " + scoreManager.GetWinningBalance() + " credits!";
		}

		private void UpdateCurrentBalanceText()
		{
			currentBalanceText.text = "Balance: " + scoreManager.GetCurrentBalance() + " Credits";
		}

		private void UpdateBetText()
		{
			currentBetText.text = "Bet: " + scoreManager.GetCurrentBet() + " Credits";
		}
	}
}