using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cyberspeed.Services;
using cyberspeed.MatchGame.UI;
using System.Collections;

namespace cyberspeed.MatchGame
{
    public class GameMode : IGameModeService
    {
        private const int MAXCARDSAVAILABLE = 20;
        private int rows, columns;
        private int[] cardArray;
        private List<UICard> uiCards = new List<UICard>();
        private UICard[] allCards;
        private SavedGameData savedGameData;

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
            if (savedGameData == null)
            {
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
            }
            else
            {
                for(int i = 0; i < cardArray.Length; i++)
                {
                    Debug.Log(savedGameData.cards[i].cardSpriteIndex);
                    cardArray[i] = savedGameData.cards[i].cardSpriteIndex;
                }
            }
            return cardArray;
        }

        public void CardOpened(UICard card)
        {
            Debug.Log($"Card opened : {card.pIndex}");
            ServiceLocator.Singleton.Get<IScoreService>().TurnTaken();
            uiCards.Add(card);
            if (uiCards.Count == 2)
            {
                if (uiCards[0].pIndex == uiCards[1].pIndex)
                {
                    CoroutineManager.Singleton.StartCoroutine(uiCards[0].HideCard(1));
                    CoroutineManager.Singleton.StartCoroutine(uiCards[1].HideCard(1));
                    ServiceLocator.Singleton.Get<IScoreService>().MatchSuccess();
                    CoroutineManager.Singleton.StartCoroutine(CheckForGameEnd(1.1f));
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

        public void FeedAllCard(UICard[] cards)
        {
            Debug.Log("feed all card called");
            allCards = cards;
            if (savedGameData != null)
            {
                for (int i = 0; i < allCards.Length; i++)
                {
                    if (savedGameData.cards[i].isCardClosed)
                        allCards[i].HideCardForSavedGame();
                    if (savedGameData.cards[i].isCardFaceDown == false && savedGameData.cards[i].isCardClosed == false)
                    {
                        allCards[i].MakeCardFaceUpForSavedGame();
                    }
                }
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

        private IEnumerator CheckForGameEnd(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            bool isGameEnded = true;
            for(int i=0;i<allCards.Length;i++)
            {
                if(allCards[i].pIsCardClosed == false)
                {
                    isGameEnded = false;
                    break;
                }
            }
            if (isGameEnded)
            {
                for (int i = 0; i < allCards.Length; i++)
                {
                    allCards[i].Reset();
                }
                ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.GameEnd.ToString());
            }
        }

        public UICard[] GetAllCardsUI()
        {
            return allCards;
        }

        public void SetSavedGameData(SavedGameData savedGameData)
        {
            this.savedGameData = savedGameData;
        }

        public SavedGameData GetSavedGameDataIfAny()
        {
            return savedGameData;
        }
    }
}