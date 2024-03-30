using UnityEngine;
using cyberspeed.FSM;

namespace cyberspeed.MatchGame
{
    public class MainMenuState : StateBehaviour
    {
        public override string stateName => States.MainMenu.ToString();
        private GameObject mainMenu;

        public override void DeInit()
        {
            Destroy(mainMenu);
        }

        public override void Init()
        {
            //usually this should be loaded via assetbundle and not via resources folder
            mainMenu = Instantiate(Resources.Load("Canvas-MainMenu") as GameObject, transform);
        }
    }
}
