using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class Card : MonoBehaviour
    {
        private string suit;
        private string value;
        public Image cardFace;

        //public CardFace cardFace;

        // Constructor for card
        public Card(string suit, string value)
        {
            this.suit = suit;
            this.value = value;
        }

        // Getter for suit
        public string GetSuit()
        {
            return suit;
        }

        // Getter for value
        public string GetValue()
        {
            return value;
        }

        // Setter for suit
        public void SetSuit(string suit)
        {
            this.suit = suit;
        }

        // Setter for value
        public void SetValue(string value)
        {
            this.value = value;
        }

        // Converts the card to a string for easy testing
        public string toString()
        {
            return suit + value;
        }

        // Pass card face sprite
        public void SetCardFace(Sprite cardFace)
        {
            this.cardFace.sprite = cardFace;
        }
    }
}
    
