using cyberspeed.Services;
using UnityEngine;
using TMPro;

namespace cyberspeed.MatchGame.UI
{
    public class UIGameEnd : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtScore;

        private void Awake()
        {
            txtScore.text = $"Your score : {ServiceLocator.Singleton.Get<IScoreService>().GetScore()}\n\nTurns taken : {ServiceLocator.Singleton.Get<IScoreService>().GetTurnsTaken()}";
        }

        public void OnBtnHomeClicked()
        {
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.MainMenu.ToString());
        }

        public void OnBtnReplayClicked()
        {
            ServiceLocator.Singleton.Get<IScoreService>().Reset();
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.GamePlay.ToString());
        }
    }
}