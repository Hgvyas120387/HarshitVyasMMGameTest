using UnityEngine;
using cyberspeed.Services;
using cyberspeed.FSM;
using cyberspeed.MatchGame;

public class BootStrap : MonoBehaviour
{
    //all of the states list which our game contains
    [SerializeField] private StateBehaviour[] gameStates;

    // Start is called before the first frame update
    void Start()
    {
        //registor the fsm service
        ServiceLocator.Singleton.Register<IFSMService>(new FSM());
        //add all the states
        for (int i = 0; i < gameStates.Length; i++)
            ServiceLocator.Singleton.Get<IFSMService>().AddState(gameStates[i]);
        //switch to initial state
        ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.Loading.ToString());
    }

    
}
