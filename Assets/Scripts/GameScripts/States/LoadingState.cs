using cyberspeed.FSM;
using UnityEngine;

namespace cyberspeed.MatchGame
{
    public class LoadingState : StateBehaviour
    {
        public override string stateName => States.Loading.ToString();
        private GameObject loading;

        public override void DeInit()
        {
            Destroy(loading);
        }

        public override void Init()
        {
            //usually this should be loaded via assetbundle and not via resources folder
            loading = Instantiate(Resources.Load("Canvas-Loading") as GameObject,transform);
        }
    }
}