using cyberspeed.MatchGame.UI;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public class GameSaveLoadService : IGameSaveLoadService
    {
        private const string KEYFORSAVEDGAME = "savedGameData";
        private SavedGameData data = new SavedGameData();

        public void DeleteSavedGame()
        {
            ServiceLocator.Singleton.Get<IGameModeService>().SetSavedGameData(null);
            PlayerPrefs.DeleteKey(KEYFORSAVEDGAME);
            PlayerPrefs.Save();
        }

        public SavedGameData LoadGameIfAny()
        {
            string json = PlayerPrefs.GetString(KEYFORSAVEDGAME, string.Empty);
            if (string.IsNullOrEmpty(json))
                return null;
            else
                return JsonUtility.FromJson<SavedGameData>(json);
        }

        public void SaveGame()
        {
            UICard[] cards = ServiceLocator.Singleton.Get<IGameModeService>().GetAllCardsUI();
            if(data.cards == null)
            {
                data.cards = new CardData[cards.Length];
                for(int i = 0; i < data.cards.Length; i++)
                {
                    data.cards[i] = new CardData();
                }
            }
            for(int i = 0; i < cards.Length; i++)
            {
                data.cards[i].cardSpriteIndex = cards[i].pIndex;
                data.cards[i].isCardClosed = cards[i].pIsCardClosed;
                data.cards[i].isCardFaceDown = cards[i].pIsCardFaceDown;
            }
            data.score = ServiceLocator.Singleton.Get<IScoreService>().GetScore();
            data.turnsTaken = ServiceLocator.Singleton.Get<IScoreService>().GetTurnsTaken();
            data.scoreComboMultiplier = ServiceLocator.Singleton.Get<IScoreService>().GetScoreComboMultiplier();
            data.numberOfColumns = ServiceLocator.Singleton.Get<IGameModeService>().GetNumberOfColumns();
            data.numberOfRows = ServiceLocator.Singleton.Get<IGameModeService>().GetNumberOfRows();
            PlayerPrefs.SetString(KEYFORSAVEDGAME, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString(KEYFORSAVEDGAME));
        }
    }
}