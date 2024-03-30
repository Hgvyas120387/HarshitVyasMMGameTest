using cyberspeed.Services;
using UnityEngine;
using TMPro;

namespace cyberspeed.MatchGame.UI
{
    public class UIGameEnd : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtScore;
        [SerializeField] private AudioClip gameEndSound;

        private void Awake()
        {
            ServiceLocator.Singleton.Get<IAudioService>().PlayAudioOneShot(gameEndSound);
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