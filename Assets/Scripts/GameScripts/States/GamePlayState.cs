using UnityEngine;
using cyberspeed.FSM;

namespace cyberspeed.MatchGame
{
    public class GamePlayState : StateBehaviour
    {
        public override string stateName => States.GamePlay.ToString();
        private GameObject gamePlay;

        public override void DeInit()
        {
            Destroy(gamePlay);
        }

        public override void Init()
        {
            //usually this should be loaded via assetbundle and not via resources folder
            gamePlay = Instantiate(Resources.Load("Canvas-GamePlay") as GameObject, transform);
        }
    }
}
