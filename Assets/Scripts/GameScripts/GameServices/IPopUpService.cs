using cyberspeed.Services;
using System;

namespace cyberspeed.MatchGame
{
    public interface IPopUpService : IService
    {
        public void ShowPopUp(string title, string message, string buttonText, Action OnButtonClick);
        public void HidePopUp();
    }
}