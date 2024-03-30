using UnityEngine;
using cyberspeed.Services;
using cyberspeed.FSM;
using cyberspeed.MatchGame;

public class BootStrap : MonoBehaviour
{
    //all of the states list which our game contains
    [SerializeField] private StateBehaviour[] gameStates;
    [SerializeField] private States initialState = States.Loading;

    // Start is called before the first frame update
    void Start()
    {
        //allow game to run as fast as possible
        Application.targetFrameRate = 60;
        //register audio service
        ServiceLocator.Singleton.Register<IAudioService>(new AudioManager());
        //register the Game mode service
        ServiceLocator.Singleton.Register<IGameModeService>(new GameMode());
        //register score service
        ServiceLocator.Singleton.Register<IScoreService>(new GameScore());
        //register the fsm service
        ServiceLocator.Singleton.Register<IFSMService>(new FSM());
        //add all the states
        for (int i = 0; i < gameStates.Length; i++)
            ServiceLocator.Singleton.Get<IFSMService>().AddState(gameStates[i]);
        //switch to initial state
        ServiceLocator.Singleton.Get<IFSMService>().ChangeState(initialState.ToString());
    }

    
}
