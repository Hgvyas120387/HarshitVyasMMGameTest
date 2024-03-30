using System.Collections;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame.UI
{
    public class UILoading : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            //simulating fake loading for 2 second
            CoroutineManager.Singleton.StartCoroutine(FakeLoading(2.0f));
        }

        private IEnumerator FakeLoading(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.MainMenu.ToString());
        }
    }
}
