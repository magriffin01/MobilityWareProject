using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Hand : MonoBehaviour
    {
        public Transform HandObject;

        public List<GameObject> hand;
        private int cardsInHand;

        private void Start()
        {
            // Set first five cards into hand
        }

        // Adds a card to the hand
        public void AddCard(GameObject card, int indexOfCard = 0)
        {
            if (!isFull())
            {
                card.transform.SetParent(HandObject);
                card.transform.localPosition = new Vector3(0, 0, 0);
                card.transform.localScale = new Vector3(1, 1, 1);
                card.SetActive(true);
                hand.Insert(indexOfCard, card);
                ++cardsInHand;
            }
            else
            {
                Debug.Log("Attempting to deal cards to a full hand!");
            }
        }

        public void ResetHand()
        {
            hand.Clear();
            foreach (GameObject card in hand)
            {
                RemoveCard(card);
            }
        }

        public bool isFull()
        {
            return hand.Count == 5;
        }

        // Removes a card from the hand
        public int RemoveCard(GameObject card)
        {
            int indexOfCard = hand.IndexOf(card);
            hand.Remove(card);
            cardsInHand--;
            return indexOfCard;
        }

        // Replaces the card in the hand with another
        public void ReplaceCard(GameObject cardToAdd, GameObject cardToRemove)
        {
            AddCard(cardToAdd, RemoveCard(cardToRemove));
        }
    }
}

