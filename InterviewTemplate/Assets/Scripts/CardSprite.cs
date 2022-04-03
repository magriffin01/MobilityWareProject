using System;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class CardSprite : MonoBehaviour
    {
        public Card ThisCard;

        private Image cardImage;
        private string suit;
        private string value;

        private void Start()
        {
            suit = ThisCard.GetSuit();
            value = ThisCard.GetValue();
        }
    }
}

