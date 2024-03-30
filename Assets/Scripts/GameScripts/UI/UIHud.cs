using TMPro;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame.UI
{
    public class UIHud : MonoBehaviour, IHudService
    {
        private int actualScore, displayScore;
        private float t = 0;
        private bool isScoreIncreased;
        [SerializeField] private TextMeshProUGUI txtScore = null;
        [SerializeField] private TextMeshProUGUI txtTurnTaken = null;
        [SerializeField] private TextMeshProUGUI txtComboMultiplier = null;
        
        private void Awake()
        {
            ServiceLocator.Singleton.Register<IHudService>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Singleton.Unregister<IHudService>();
        }

        public void UpdateScore(int newScore)
        {
            t = 0;
            isScoreIncreased = actualScore < newScore;
            actualScore = newScore;
            enabled = true;
        }

        public void UpdateTurnTaken(int turnsTaken)
        {
            txtTurnTaken.text = $"Turn Taken : {turnsTaken}";
        }

        private void Update()
        {
            t = Mathf.Clamp(t + Time.deltaTime, 0, 1);
            //this is bcoz float is converted to int and we dont want any pause in updating score so it looks smooth
            if(isScoreIncreased)
                displayScore = Mathf.CeilToInt(Mathf.Lerp(displayScore, actualScore, t));
            else
                displayScore = Mathf.FloorToInt(Mathf.Lerp(displayScore, actualScore, t));
            if (t >= 1)
            {
                displayScore = actualScore;
                enabled = false;
            }
            txtScore.text = $"Score : {displayScore}";
        }

        public void UpdateComboMultiplier(int comboMultiplier)
        {
            txtComboMultiplier.text = $"Combo : X{comboMultiplier}";
        }
    }
}
