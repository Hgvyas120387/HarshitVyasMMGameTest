using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public interface IScoreService : IService
    {
        public void TurnTaken();
        public void MatchSuccess();
        public void MatchUnSuccess();
        public void Reset();
        public int GetScore();
        public int GetTurnsTaken();
    }
}