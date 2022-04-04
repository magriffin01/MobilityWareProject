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
        private int winningMultiplier;
        private int currentBet;
        private List<Card> cardsInHand;
        private string winType;

        private HashSet<string> valuesInHand;
        private HashSet<string> suitsInHand;
        private List<int> valuesInHandInt;
        private Dictionary<string, int> occurrences;
        

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
            
            GetValuesAndSuits();

            // Check every win condition
            if (IsRoyalFlush())
            {
                winningMultiplier = 800;
                winType = "Royal Flush!";
                isWin = true;
            }
            else if (IsStraightFlush())
            {
                winningMultiplier = 50;
                winType = "Straight Flush!";
                isWin = true;
            }
            else if (IsFourOfAKind())
            {
                winningMultiplier = 25;
                winType = "Four of a Kind!";
                isWin = true;
            }
            else if (IsFullHouse())
            {
                winningMultiplier = 9;
                winType = "Full House!";
                isWin = true;
            }
            else if (IsFlush())
            {
                winningMultiplier = 6;
                winType = "Flush!";
                isWin = true;
            }
            else if (IsStraight())
            {
                winningMultiplier = 4;
                winType = "Straight!";
                isWin = true;
            }
            else if (IsThreeOfAKind())
            {
                winningMultiplier = 3;
                winType = "Three of a Kind!";
                isWin = true;
            }
            else if (IsTwoPair())
            {
                winningMultiplier = 2;
                winType = "Two Pair!";
                isWin = true;
            }
            else if (IsJacksOrBetter())
            {
                winningMultiplier = 1;
                winType = "Jacks or Better!";
                isWin = true;
            }
            else
            {
                winningMultiplier = 0;
                winType = "";
            }

            winningBalance = currentBet * winningMultiplier;
            balance += winningBalance;

            return isWin;
        }

        private void GetValuesAndSuits()
        {
            cardsInHand = new List<Card>();
            
            foreach (GameObject card in hand.hand)
            {
                cardsInHand.Add(card.GetComponent<Card>());
            }
            
            valuesInHand = new HashSet<string>();
            suitsInHand = new HashSet<string>();
            valuesInHandInt = new List<int>();
            occurrences = new Dictionary<string, int>();
            
            foreach (Card card in cardsInHand)
            {
                valuesInHand.Add(card.GetValue());
                suitsInHand.Add(card.GetSuit());
                valuesInHandInt.Add(Int32.Parse(card.GetValue()));
                occurrences[card.GetValue()] = 0;
            }
            
            foreach (Card card in cardsInHand)
            {
                occurrences[card.GetValue()] += 1;
            }
            
            valuesInHandInt.Sort();
        }

        private bool IsRoyalFlush()
        {
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
            if (suitsInHand.Count != 1)
            {
                return false;
            }

            // Account for ace
            if (valuesInHandInt[0] == 1)
            {
                int listIndex = 2;
                for (int i = 2; i < valuesInHandInt.Count; ++i)
                {
                    if (valuesInHandInt[listIndex] - i != valuesInHandInt[1])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                if (valuesInHandInt[1] == 2 || valuesInHandInt[valuesInHandInt.Count - 1] == 13)
                {
                    return true;
                }

                return false;
            }
            else
            {
                int listIndex = 1;
                for (int i = 1; i < valuesInHandInt.Count; ++i)
                {
                    if (valuesInHandInt[listIndex] - i != valuesInHandInt[0])
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
            if (valuesInHand.Count != 2)
            {
                return false;
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
           if (suitsInHand.Count != 1)
            {
                return false;
            }

            return true;
        }

        private bool IsStraight()
        {
            // Account for ace
            if (valuesInHandInt[0] == 1)
            {
                int listIndex = 2;
                for (int i = 2; i < valuesInHandInt.Count; ++i)
                {
                    if (valuesInHandInt[listIndex] - i != valuesInHandInt[1])
                    {
                        return false;
                    }

                    ++listIndex;
                }

                if (valuesInHandInt[1] == 2 || valuesInHandInt[valuesInHandInt.Count - 1] == 13)
                {
                    return true;
                }

                return false;
            }
            else
            {
                int listIndex = 1;
                for (int i = 1; i < valuesInHandInt.Count; ++i)
                {
                    if (valuesInHandInt[listIndex] - i != valuesInHandInt[0])
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
            if (valuesInHand.Count != 3)
            {
                return false;
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

