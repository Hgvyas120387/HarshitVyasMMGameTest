using TMPro;
using UnityEngine;
using cyberspeed.Services;
using System;

namespace cyberspeed.MatchGame
{
    public class UIPopUp : MonoBehaviour,IPopUpService
    {
        [SerializeField] private TextMeshProUGUI txtTitle = null;
        [SerializeField] private TextMeshProUGUI txtMessage = null;
        [SerializeField] private TextMeshProUGUI txtButtonText = null;
        private Action callBackForButtonClick;

        public void HidePopUp()
        {
            gameObject.SetActive(false);
        }

        public void ShowPopUp(string title, string message, string buttonText, Action OnButtonClick)
        {
            txtTitle.text = title;
            txtMessage.text = message;
            txtButtonText.text = buttonText;
            callBackForButtonClick = OnButtonClick;
            gameObject.SetActive(true);
        }

        public void OnButtonClicked()
        {
            callBackForButtonClick?.Invoke();
        }

        private void Start()
        {
            ServiceLocator.Singleton.Register<IPopUpService>(this);
            HidePopUp();
        }
    }
}