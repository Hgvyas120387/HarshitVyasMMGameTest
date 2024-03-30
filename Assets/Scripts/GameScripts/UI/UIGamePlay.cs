using UnityEngine.UI;
using UnityEngine;
using cyberspeed.Services;
using System.Collections.Generic;
using cyberspeed.Pooling;
using System;

namespace cyberspeed.MatchGame.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        [SerializeField] private string pfUICardTag = string.Empty;
        [SerializeField] private GridLayoutGroup gridLayout = null;
        private List<UICard> allCards = new List<UICard>();
        //called from editor
        public void OnBtnHomeClicked()
        {
            //since user is quitting delete game so on next launch we can start a fresh
            ServiceLocator.Singleton.Get<IGameSaveLoadService>().DeleteSavedGame();
            for(int i = 0; i < allCards.Count; i++)
            {
                allCards[i].Reset();
            }
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.MainMenu.ToString());
        }
        //called from editor
        public void OnBtnSaveGameClicked()
        {
            //save the game so on next launch we can start from same place
            ServiceLocator.Singleton.Get<IGameSaveLoadService>().SaveGame();
            ServiceLocator.Singleton.Get<IPopUpService>().ShowPopUp("Game Saved", "Game has been saved you can quit now on next launch game will resume from here if you don't press home button nor complete the game", "Ok", OnPopUpButtonClicked);
        }

        private void OnPopUpButtonClicked()
        {
            ServiceLocator.Singleton.Get<IPopUpService>().HidePopUp();
        }

        private void Start()
        {
            allCards.Clear();
            float size = ServiceLocator.Singleton.Get<IGameModeService>().GetGridItemSize();
            Debug.Log($"Size : {size}");
            gridLayout.cellSize = new Vector2(size,size);
            int rows = ServiceLocator.Singleton.Get<IGameModeService>().GetNumberOfRows();
            int columns = ServiceLocator.Singleton.Get<IGameModeService>().GetNumberOfColumns();
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
            int[] cards = ServiceLocator.Singleton.Get<IGameModeService>().GetCardArray();
            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    UICard card = ServiceLocator.Singleton.Get<IPoolService>().Instantiate<UICard>(pfUICardTag);
                    card.transform.SetParent(gridLayout.transform);
                    card.SetData(cards[index]);
                    index++;
                    card.gameObject.SetActive(true);
                    allCards.Add(card);
                }
            }
            
            ServiceLocator.Singleton.Get<IGameModeService>().FeedAllCard(allCards.ToArray());
        }
    }
}