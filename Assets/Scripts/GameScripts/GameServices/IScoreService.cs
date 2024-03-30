using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public interface IScoreService : IService
    {
        /// <summary>
        /// call it for every turn taken by user
        /// </summary>
        public void TurnTaken();
        /// <summary>
        /// call it when match success it will calculate new score
        /// </summary>
        public void MatchSuccess();
        /// <summary>
        /// call it when match un success it will calculate new score
        /// </summary>
        public void MatchUnSuccess();
        /// <summary>
        /// reset the score, combo bonus and turn taken useful to restart game
        /// </summary>
        public void Reset();
        /// <summary>
        /// get the score of the user
        /// </summary>
        /// <returns>score of the user</returns>
        public int GetScore();
        /// <summary>
        /// Get the turns taken by user
        /// </summary>
        /// <returns>turns taken by user</returns>
        public int GetTurnsTaken();
        /// <summary>
        /// Get the score combo multiplier
        /// </summary>
        /// <returns>score combo multiplier</returns>
        public int GetScoreComboMultiplier();
        /// <summary>
        /// To set last saved games score, turns taken and combo multiplier
        /// </summary>
        public void SetSavedGameData(int score,int turnsTaken,int scoreComboMultiplier);
    }
}