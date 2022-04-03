using System;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public Deck deck;
		public Hand hand;

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}

		private void OnEnable()
		{
			UIManager.DealCards += Deal;
		}

		private void OnDisable()
		{
			UIManager.DealCards -= Deal;
		}

		private void Deal()
		{
			if (hand.isFull())
			{
				foreach (GameObject card in hand.hand)
				{
					deck.AddCard(card);
				}
				hand.ResetHand();
			}
			for (int i = 0; i < 5; ++i)
			{
				GameObject card = deck.PlayTopCard();
				Debug.Log(card.GetComponent<Card>().toString());
				hand.AddCard(card);
			}
		}
	}
}