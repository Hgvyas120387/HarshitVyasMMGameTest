using cyberspeed.Services;
namespace cyberspeed.MatchGame
{
    public class GameScore : IScoreService
    {
        private int score, turnsTaken;

        private const int SCORETOGRANTONMATCHSUCCESS = 100;
        private const int SCORETODEDUCTONMATCHUNSUCCESS = 20;

        public void MatchSuccess()
        {
            score += SCORETOGRANTONMATCHSUCCESS;
            ServiceLocator.Singleton.Get<IHudService>().UpdateScore(score);
        }
        public void MatchUnSuccess()
        {
            if (score > SCORETODEDUCTONMATCHUNSUCCESS)
                score -= SCORETODEDUCTONMATCHUNSUCCESS;
            else
                score = 0;
            ServiceLocator.Singleton.Get<IHudService>().UpdateScore(score);
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
        }
    }
}