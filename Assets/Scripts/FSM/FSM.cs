/// <summary>
/// Small finite state machine to manage the menus or different states of the game
/// </summary>
using System.Collections.Generic;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.FSM
{
    public class FSM : IFSMService
    {
        //dictionary to hold all our game states like loading, main menu, game play, game over
        private Dictionary<string, IState> states = new Dictionary<string, IState>();
        //reference of the current state
        private IState currentState;

        /// <summary>
        /// gives current state name empty in case no state is there
        /// </summary>
        public string currentStateName
        {
            get
            {
                if (currentState != null)
                    return currentState.stateName;
                else
                    return string.Empty;
            }
        }
        /// <summary>
        /// Before using FSM first need to add all the states we want in the game
        /// </summary>
        /// <param name="stateBehaviour">State we want to add</param>
        public void AddState(IState stateBehaviour)
        {
            if (states.ContainsKey(stateBehaviour.stateName))
            {
                Debug.LogError($"FSM already contains {stateBehaviour.stateName} please verify");
            }
            else
            {
                states.Add(stateBehaviour.stateName, stateBehaviour);
            }
        }
        /// <summary>
        /// To change the state of the game state must be added before switching to it
        /// </summary>
        /// <param name="stateName">state name in which we want to enter</param>
        public void ChangeState(string stateName)
        {
            if (states.ContainsKey(stateName))
            {
                //first exit the current state if any
                if (currentState != null)
                    currentState.DeInit();
                //get the new state and enter in that
                currentState = states[stateName];
                currentState.Init();
            }
            else
            {
                Debug.LogError($"FSM is not aware about any state named {stateName} discarding this request please verify");
            }
        }
    }
}