using UnityEngine;
using cyberspeed.Services;
using cyberspeed.FSM;
using cyberspeed.MatchGame;

public class BootStrap : MonoBehaviour
{
    //all of the states list which our game contains
    [SerializeField] private StateBehaviour[] gameStates = null;
    [SerializeField] private States initialState = States.Loading;

    // Start is called before the first frame update
    void Start()
    {
        //allow game to run as fast as possible
        Application.targetFrameRate = 60;
        //register game load save service
        ServiceLocator.Singleton.Register<IGameSaveLoadService>(new GameSaveLoadService());
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

        SavedGameData savedGameData = ServiceLocator.Singleton.Get<IGameSaveLoadService>().LoadGameIfAny();
        if (savedGameData != null)
        {
            //resume last saved game
            ServiceLocator.Singleton.Get<IGameModeService>().SetSavedGameData(savedGameData);
            ServiceLocator.Singleton.Get<IGameModeService>().SetGameGrid(savedGameData.numberOfRows, savedGameData.numberOfColumns);
            ServiceLocator.Singleton.Get<IScoreService>().SetSavedGameData(savedGameData.score, savedGameData.turnsTaken, savedGameData.scoreComboMultiplier);
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.GamePlay.ToString());
            ServiceLocator.Singleton.Get<IHudService>().UpdateScore(savedGameData.score);
            ServiceLocator.Singleton.Get<IHudService>().UpdateTurnTaken(savedGameData.turnsTaken);
            ServiceLocator.Singleton.Get<IHudService>().UpdateComboMultiplier(savedGameData.scoreComboMultiplier);
        }
        else
        {
            //switch to initial state
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(initialState.ToString());
        }
    }
}