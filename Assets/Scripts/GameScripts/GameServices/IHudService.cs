using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public interface IHudService : IService
    {
        /// <summary>
        /// to update score in the hud
        /// </summary>
        /// <param name="newScore">latest score</param>
        public void UpdateScore(int newScore);
        /// <summary>
        /// to update turns taken by user
        /// </summary>
        /// <param name="turnsTaken">latest turn count</param>
        public void UpdateTurnTaken(int turnsTaken);
        /// <summary>
        /// to update combo multiplier
        /// </summary>
        /// <param name="comboMultiplier">combo multiplier to apply if next pair matching success</param>
        public void UpdateComboMultiplier(int comboMultiplier);
    }
}
