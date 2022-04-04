using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Deck : MonoBehaviour
    {
        public GameObject CardPrefab;
        public Transform DeckObject;
        public List<Sprite> CardFaces;
        
        private static List<string> suits = new List<string>() {"C", "D", "H", "S"};
        private static List<string> values = new List<string>()
            {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"};

        public List<GameObject> deck;
        private int cardsInDeck;

        private void Awake()
        {
            deck = GenerateDeck();
        }

        // Generates a deck of cards by looping through the suits and values and making new cards
        public List<GameObject> GenerateDeck()
        {
            List<GameObject> deck = new List<GameObject>();
            int index = 0;

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    // Instantiate cardObject and set it's corresponding suit, value, and cardFace. Change it's parent, position, and scale
                    GameObject cardObject = Instantiate(CardPrefab, transform.position, Quaternion.identity);
                    Card newCard = cardObject.GetComponent<Card>();
                    newCard.SetSuit(suit);
                    newCard.SetValue(value);
                    newCard.SetCardFace(CardFaces[index]);
                    cardObject.transform.SetParent(DeckObject);
                    cardObject.name = newCard.toString();
                    cardObject.SetActive(false);
                    deck.Add(cardObject);
                    index++;
                    cardsInDeck++;
                }
            }
            Debug.Log("Cards in deck: " + cardsInDeck);
            Shuffle(deck);

            return deck;
        }

        // Removes card deck
        public GameObject PlayTopCard()
        {
            GameObject cardToPlay = deck[0];

            deck.Remove(cardToPlay);
            --cardsInDeck;
            return cardToPlay;
        }

        // Adds a card to the deck
        public void AddCard(GameObject card)
        {
            card.transform.SetParent(DeckObject);
            card.SetActive(false);

            if (card.GetComponent<Card>().IsHolding())
            {
                card.GetComponent<Card>().SetHoldText();
            }
            
            deck.Add(card);
            ++cardsInDeck;
        }

        private void OnEnable()
        {
            StateDeal.BeginDealState += Reshuffle;
        }

        private void OnDisable()
        {
            StateDeal.BeginDealState -= Reshuffle;
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

        private void Reshuffle()
        {
            Shuffle(deck);
        }
    }
}

