using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Dealer : MonoBehaviour
    {
        public Deck deck;
        public Hand hand;

        private void OnEnable()
        {
            StateDeal.BeginDealState += Deal;
            StateDraw.BeginDrawState += Draw;
        }

        private void OnDisable()
        {
            StateDeal.BeginDealState -= Deal;
            StateDraw.BeginDrawState -= Draw;
        }
        
        // Deal 5 cards to the hand
        private void Deal()
        {
            // Check if full, is so add the cards in hand back to the deck and reset the hand
            if (hand.isFull())
            {
                foreach (GameObject card in hand.hand)
                {
                    deck.AddCard(card);
                }
                hand.ResetHand();
            }
            // Add five top cards to the hand
            for (int i = 0; i < 5; ++i)
            {
                GameObject card = deck.PlayTopCard();
                hand.AddCard(card);
            }
        }

        // Replaces the cards that are not being held
        private void Draw()
        {
            List<GameObject> cardsToReplace = new List<GameObject>();
            
            // Add cards being not being held to be replaced
            foreach (GameObject card in hand.hand)
            {
                if (!card.GetComponent<Card>().IsHolding())
                {
                    cardsToReplace.Add(card);
                }
            }

            // Replace the cards not being held
            foreach (GameObject card in cardsToReplace)
            {
                deck.AddCard(card);
                hand.ReplaceCard(deck.PlayTopCard(), card);
            }
        }
    }
}

