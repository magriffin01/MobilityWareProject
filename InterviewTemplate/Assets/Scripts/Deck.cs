using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Deck : MonoBehaviour
    {
        private static List<string> suits = new List<string>() {"C", "D", "H", "S"};
        private static List<string> values = new List<string>()
            {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

        private List<Card> deck;

        private void Start()
        {
            PlayDeck();
        }

        // Generates a deck of cards by looping through the suits and values and making new cards
        public List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    Card card = new Card(suit, value);
                    deck.Add(card);
                }
            }

            return deck;
        }

        // Tests the card generation
        public void PlayDeck()
        {
            deck = GenerateDeck();
            Shuffle(deck);

            foreach (Card card in deck)
            {
                Debug.Log(card.toString());
            }
        }

        // Shuffles the deck of cards (randomizes order of list), attributed from https://stackoverflow.com/questions/273313/randomize-a-listt
        private void Shuffle<T>(List<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n);
                --n;
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}

