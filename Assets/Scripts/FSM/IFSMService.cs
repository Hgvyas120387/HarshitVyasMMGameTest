using cyberspeed.Services;
using cyberspeed.FSM;

namespace cyberspeed.Services
{
    public interface IFSMService : IService
    {
        /// <summary>
        /// gives current state name empty in case no state is there
        /// </summary>
        public string currentStateName { get; }
        /// <summary>
        /// Before using FSM first need to add all the states we want in the game
        /// </summary>
        /// <param name="stateBehaviour">State we want to add</param>
        public void AddState(IState stateBehaviour);
        /// <summary>
        /// To change the state of the game state must be added before switching to it
        /// </summary>
        /// <param name="stateName">state name in which we want to enter</param>
        public void ChangeState(string stateName);
    }
}