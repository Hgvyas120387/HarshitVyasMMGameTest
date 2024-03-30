using System;

namespace cyberspeed.MatchGame
{
    [Serializable]
    public class SavedGameData
    {
        public CardData[] cards;
        public int score;
        public int turnsTaken;
        public int scoreComboMultiplier;
    }

}