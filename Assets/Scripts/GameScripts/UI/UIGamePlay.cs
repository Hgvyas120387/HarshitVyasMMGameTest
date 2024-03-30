using UnityEngine.UI;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public class UIGamePlay : MonoBehaviour
    {
        [SerializeField] private UICard pfUICard;
        [SerializeField] private GridLayoutGroup gridLayout;
        private int[] cardIndex;
        //called from editor
        public void OnBtnHomeClicked()
        {
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.MainMenu.ToString());
        }

        private void Start()
        {
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
                }
            }
            pfUICard.gameObject.SetActive(false);
        }
    }
}