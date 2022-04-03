using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Deck : MonoBehaviour
    {
        public GameObject cardPrefab;
        public List<Sprite> CardFaces;
        public Transform cardsObject;
        
        private static List<string> suits = new List<string>() {"C", "D", "H", "S"};
        private static List<string> values = new List<string>()
            {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

        private List<GameObject> deck;

        private void Start()
        {
            PlayDeck();
        }

        public List<GameObject> GetDeck()
        {
            return deck;
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
                    GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);
                    newCard.GetComponent<Card>().SetSuit(suit);
                    newCard.GetComponent<Card>().SetValue(value);
                    newCard.GetComponent<Card>().SetCardFace(CardFaces[index]);
                    newCard.transform.SetParent(cardsObject);
                    newCard.transform.localPosition = new Vector3(0, 0, 0);
                    newCard.transform.localScale = new Vector3(1, 1, 1);
                    deck.Add(newCard);
                    index++;
                }
            }
            
            Shuffle(deck);
            
            return deck;
        }

        // Tests the card generation
        public void PlayDeck()
        {
            deck = GenerateDeck();

            foreach (GameObject card in deck)
            {
                Debug.Log(card.GetComponent<Card>().toString());
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

