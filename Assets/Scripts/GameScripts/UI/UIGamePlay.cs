using UnityEngine.UI;
using UnityEngine;
using cyberspeed.Services;
using System.Collections.Generic;

namespace cyberspeed.MatchGame.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        [SerializeField] private UICard pfUICard;
        [SerializeField] private GridLayoutGroup gridLayout;
        private List<UICard> allCards = new List<UICard>();
        //called from editor
        public void OnBtnHomeClicked()
        {
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.MainMenu.ToString());
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
                    UICard card = Instantiate<UICard>(pfUICard, pfUICard.transform.parent);
                    card.SetData(cards[index]);
                    index++;
                    card.gameObject.SetActive(true);
                    allCards.Add(card);
                }
            }
            pfUICard.gameObject.SetActive(false);
            ServiceLocator.Singleton.Get<IGameModeService>().FeedAllCard(allCards.ToArray());
        }
    }
}