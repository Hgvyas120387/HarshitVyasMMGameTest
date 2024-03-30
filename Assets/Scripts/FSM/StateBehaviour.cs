using UnityEngine;

namespace cyberspeed.FSM
{
    public abstract class StateBehaviour : MonoBehaviour, IState
    {
        /// <summary>
        /// name of the state
        /// </summary>
        public abstract string stateName { get; }
        /// <summary>
        /// what should be the behaviour on entering the state
        /// usually load all art or prefabs
        /// </summary>
        public abstract void DeInit();
        /// <summary>
        /// what should be the behaviour on exiting the state
        /// usually unload all the art or prefabs and cleanup of state
        /// </summary>
        public abstract void Init();
    }
}
