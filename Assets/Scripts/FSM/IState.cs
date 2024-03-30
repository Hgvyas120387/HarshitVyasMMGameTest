/// <summary>
/// Every state of the game should implement this interface
/// </summary>
namespace cyberspeed.FSM
{
    public interface IState
    {
        /// <summary>
        /// name of the state
        /// </summary>
        public string stateName { get; }
        /// <summary>
        /// what should be the behaviour on entering the state
        /// usually load all art or prefabs
        /// </summary>
        void Init();
        /// <summary>
        /// what should be the behaviour on exiting the state
        /// usually unload all the art or prefabs and cleanup of state
        /// </summary>
        void DeInit();
    }
}