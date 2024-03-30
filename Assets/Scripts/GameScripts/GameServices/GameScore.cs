using cyberspeed.Services;
namespace cyberspeed.MatchGame
{
    public class GameScore : IScoreService
    {
        //score and turns taken by user
        private int score, turnsTaken;
        //score combo in case user do multiple match in row we increase score multiplier and on fail we reset back to 1
        private int scoreComboMultiplier = 1;
        //score to grant with multiplier on each successful match
        private const int SCORETOGRANTONMATCHSUCCESS = 100;
        //score to deduct on each un successful match
        private const int SCORETODEDUCTONMATCHUNSUCCESS = 20;

        public void MatchSuccess()
        {
            //reward the score
            score += SCORETOGRANTONMATCHSUCCESS * scoreComboMultiplier;
            //update on ui
            ServiceLocator.Singleton.Get<IHudService>().UpdateScore(score);
            //increase score multiplier
            scoreComboMultiplier++;
            ServiceLocator.Singleton.Get<IHudService>().UpdateComboMultiplier(scoreComboMultiplier);
        }
        public void MatchUnSuccess()
        {
            // reset score multiplier to 1 
            scoreComboMultiplier = 1;
            //deduct score if more than 0 else reset score to 0
            if (score > SCORETODEDUCTONMATCHUNSUCCESS)
                score -= SCORETODEDUCTONMATCHUNSUCCESS;
            else
                score = 0;
            ServiceLocator.Singleton.Get<IHudService>().UpdateScore(score);
            ServiceLocator.Singleton.Get<IHudService>().UpdateComboMultiplier(scoreComboMultiplier);
        }

        public void TurnTaken()
        {
            turnsTaken++;
            ServiceLocator.Singleton.Get<IHudService>().UpdateTurnTaken(turnsTaken);
        }

        public void Reset()
        {
            score = 0;
            turnsTaken = 0;
            scoreComboMultiplier = 1;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetTurnsTaken()
        {
            return turnsTaken;
        }

        public int GetScoreComboMultiplier()
        {
            return scoreComboMultiplier;
        }

        public void SetSavedGameData(int score, int turnsTaken, int scoreComboMultiplier)
        {
            this.score = score;
            this.turnsTaken = turnsTaken;
            this.scoreComboMultiplier = scoreComboMultiplier;
        }
    }
}