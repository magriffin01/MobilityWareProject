using System;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class ScoreManager : MonoBehaviour
    {
        public Hand hand;

        public int BetAmount = 5;
        public int StartBalance = 500;
        
        private int balance;
        private int winningBalance;
        private int currentBet;
        private List<Card> cardsInHand;
        private string winType;

        private void Awake()
        {
            balance = StartBalance;
        }

        private void OnEnable()
        {
            StateDeal.BeginDealState += SubtractBet;
        }

        private void OnDisable()
        {
            StateDeal.BeginDealState -= SubtractBet;
        }

        private void SubtractBet()
        {
            balance -= currentBet;
        }

        public void Bet()
        {
            currentBet += BetAmount;
        }

        public string GetWinType()
        {
            return winType;
        }

        public int GetWinningBalance()
        {
            return winningBalance;
        }

        public int GetCurrentBalance()
        {
            return balance;
        }

        public int GetCurrentBet()
        {
            return currentBet;
        }

        public void ResetBet()
        {
            currentBet = 0;
        }
        
        public bool CheckForWin()
        {
            bool isWin = false;
            cardsInHand = new List<Card>();
            
            foreach (GameObject card in hand.hand)
            {
                cardsInHand.Add(card.GetComponent<Card>());
            }
            
            // Check every win condition
            if (IsRoyalFlush())
            {
                winningBalance = currentBet * 800;
                winType = "Royal Flush!";
                isWin = true;
            }
            else if (IsStraightFlush())
            {
                winningBalance = currentBet * 50;
                winType = "Straight Flush!";
                isWin = true;
            }
            else if (IsFourOfAKind())
            {
                winningBalance = currentBet * 25;
                winType = "Four of a Kind!";
                isWin = true;
            }
            else if (IsFullHouse())
            {
                winningBalance = currentBet * 9;
                winType = "Full House!";
                isWin = true;
            }
            else if (IsFlush())
            {
                winningBalance = currentBet * 6;
                winType = "Flush!";
                isWin = true;
            }
            else if (IsStraight())
            {
                winningBalance = currentBet * 4;
                winType = "Straight!";
                isWin = true;
            }
            else if (IsThreeOfAKind())
            {
                winningBalance = currentBet * 3;
                winType = "Three of a Kind!";
                isWin = true;
            }
            else if (IsTwoPair())
            {
                winningBalance = currentBet * 2;
                winType = "Two Pair!";
                isWin = true;
            }
            else if (IsJacksOrBetter())
            {
                winningBalance = currentBet * 1;
                winType = "Jacks or Better!";
                isWin = true;
            }
            else
            {
                winningBalance = 0;
                winType = "";
            }

            balance += winningBalance;

            return isWin;
        }

        private bool IsRoyalFlush()
        {
            HashSet<string> valuesInHand = new HashSet<string>();

            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(card.GetValue());
            }

            if (valuesInHand.Count != 5)
            {
                return false;
            }

            if (valuesInHand.Contains("10") && valuesInHand.Contains("11") && valuesInHand.Contains("12") &&
                valuesInHand.Contains("13") && valuesInHand.Contains("1"))
            {
                return true;
            }

            return false;
        }

        private bool IsStraightFlush()
        {
            List<int> valuesInHand = new List<int>();
            HashSet<string> suitsInHand = new HashSet<string>();

            foreach (Card card in cardsInHand)
            {
                suitsInHand.Add(card.GetSuit());
                valuesInHand.Add(Int32.Parse(card.GetValue()));
            }

            if (suitsInHand.Count != 1)
            {
                return false;
            }
            
            valuesInHand.Sort();

            // Account for ace
            if (valuesInHand[0] == 1)
            {
                int listIndex = 2;
                for (int i = 2; i < valuesInHand.Count; ++i)
                {
                    if (valuesInHand[listIndex] - i != valuesInHand[1])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                if (valuesInHand[1] == 2 || valuesInHand[valuesInHand.Count - 1] == 13)
                {
                    return true;
                }

                return false;
            }
            else
            {
                int listIndex = 1;
                for (int i = 1; i < valuesInHand.Count; ++i)
                {
                    if (valuesInHand[listIndex] - i != valuesInHand[0])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                return true;
            }
        }

        private bool IsFourOfAKind()
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] = 0;
            }
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] += 1;
            }

            foreach (KeyValuePair<string, int> entry in occurrences)
            {
                if (entry.Value == 4)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFullHouse()
        {
            HashSet<string> valuesInHand = new HashSet<string>();
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(card.GetValue());
                occurrences[card.GetValue()] = 0;
            }

            if (valuesInHand.Count != 2)
            {
                return false;
            }
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] += 1;
            }
            
            foreach (KeyValuePair<string, int> entry in occurrences)
            {
                if (entry.Value != 3 || entry.Value != 2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFlush()
        {
            HashSet<string> suitsInHand = new HashSet<string>();

            foreach (Card card in cardsInHand)
            {
                suitsInHand.Add(card.GetSuit());
            }

            if (suitsInHand.Count != 1)
            {
                return false;
            }

            return true;
        }

        private bool IsStraight()
        {
            List<int> valuesInHand = new List<int>();

            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(Int32.Parse(card.GetValue()));
            }

            valuesInHand.Sort();

            // Account for ace
            if (valuesInHand[0] == 1)
            {
                int listIndex = 2;
                for (int i = 2; i < valuesInHand.Count; ++i)
                {
                    if (valuesInHand[listIndex] - i != valuesInHand[1])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                if (valuesInHand[1] == 2 || valuesInHand[valuesInHand.Count - 1] == 13)
                {
                    return true;
                }

                return false;
            }
            else
            {
                int listIndex = 1;
                for (int i = 1; i < valuesInHand.Count; ++i)
                {
                    if (valuesInHand[listIndex] - i != valuesInHand[0])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                return true;
            }
        }

        private bool IsThreeOfAKind()
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] = 0;
            }

            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()]++;
            }

            foreach (KeyValuePair<string, int> entry in occurrences)
            {
                if (entry.Value == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTwoPair()
        {
            HashSet<string> valuesInHand = new HashSet<string>();
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(card.GetValue());
                occurrences[card.GetValue()] = 0;
            }

            if (valuesInHand.Count != 3)
            {
                return false;
            }
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] += 1;
            }

            int numPairs = 0;
            
            foreach (KeyValuePair<string, int> entry in occurrences)
            {
                if (entry.Value == 2)
                {
                    numPairs++;
                }
            }

            return numPairs == 2;
        }

        private bool IsJacksOrBetter()
        {
            HashSet<string> valuesInHand = new HashSet<string>();
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(card.GetValue());
                occurrences[card.GetValue()] = 0;
            }
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] += 1;
            }
            
            foreach (KeyValuePair<string, int> entry in occurrences)
            {
                if (entry.Value == 2 && (entry.Key == "11" || entry.Key == "12" || entry.Key == "13" || entry.Key == "1"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

