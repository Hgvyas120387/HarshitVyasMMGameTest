using UnityEngine;
using cyberspeed.FSM;

namespace cyberspeed.MatchGame
{
    public class GameEnd : StateBehaviour
    {
        public override string stateName => States.GameEnd.ToString();
        private GameObject gameEnd;

        public override void DeInit()
        {
            Destroy(gameEnd);
        }

        public override void Init()
        {
            gameEnd = Instantiate(Resources.Load("Canvas-GameEnd") as GameObject, transform);
        }
    }
}