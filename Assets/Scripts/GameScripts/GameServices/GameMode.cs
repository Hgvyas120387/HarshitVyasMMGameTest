using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public class GameMode : IGameModeService
    {
        private const int MAXCARDSAVAILABLE = 20;
        private int rows, columns;
        private int[] cardArray;
        private List<UICard> uiCards = new List<UICard>();

        public int GetGridItemSize()
        {
            Debug.Log($"GetGridItemSize {rows} {columns}");
            return Resources.Load<RowColumnSizeMap>("RowColumnSizeMap").GetCellSize(rows, columns);
        }

        public int GetNumberOfColumns()
        {
            return columns;
        }

        public int GetNumberOfRows()
        {
            return rows;
        }

        public void SetGameGrid(int rows, int columns)
        {
            uiCards.Clear();
            ServiceLocator.Singleton.Get<IScoreService>().Reset();
            this.rows = rows;
            this.columns = columns;
        }

        public int[] GetCardArray()
        {
            cardArray = new int[rows * columns];
            int cardsNeeded = cardArray.Length / 2;
            int[] arrCards = GenerateRandomNumbers(MAXCARDSAVAILABLE, cardsNeeded);
            int index = 0;
            for (int i = 0; i < arrCards.Length; i++)
            {
                cardArray[index] = arrCards[i];
                index++;
                cardArray[index] = arrCards[i];
                index++;
            }
            cardArray.Shuffle();
            return cardArray;
        }

        public void CardOpened(UICard card)
        {
            ServiceLocator.Singleton.Get<IScoreService>().TurnTaken();
            uiCards.Add(card);
            if (uiCards.Count == 2)
            {
                if (uiCards[0].pIndex == uiCards[1].pIndex)
                {
                    CoroutineManager.Singleton.StartCoroutine(uiCards[0].HideCard(1));
                    CoroutineManager.Singleton.StartCoroutine(uiCards[1].HideCard(1));
                    ServiceLocator.Singleton.Get<IScoreService>().MatchSuccess();
                }
                else
                {
                    CoroutineManager.Singleton.StartCoroutine(uiCards[0].MakeCardFaceDown(1));
                    CoroutineManager.Singleton.StartCoroutine(uiCards[1].MakeCardFaceDown(1));
                    ServiceLocator.Singleton.Get<IScoreService>().MatchUnSuccess();
                }
                uiCards.Clear();
            }
        }

        private int[] GenerateRandomNumbers(int highestNum, int count)
        {
            if (highestNum < count)
            {
                throw new ArgumentException("Highest number must be greater than or equal to the count please verify and make sure you have enough sprites in UICard.cs");
            }

            HashSet<int> uniqueNumbers = new HashSet<int>();
            System.Random rand = new System.Random();

            while (uniqueNumbers.Count < count)
            {
                uniqueNumbers.Add(rand.Next(highestNum));
            }

            return uniqueNumbers.ToArray();
        }
    }
}