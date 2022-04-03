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
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;

		[SerializeField]
		private Button betButton = null;

		[SerializeField] 
		private Button dealButton = null;

		public delegate void DealButtonPressed();
		public static event DealButtonPressed DealCards;

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			betButton.onClick.AddListener(OnBetButtonPressed);
			dealButton.onClick.AddListener(OnDealButtonPressed);
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			Debug.Log("Bet pressed");
		}

		private void OnDealButtonPressed()
		{
			Debug.Log("Deal pressed");
			if (DealCards != null)
			{
				DealCards();
			}
		}
	}
}