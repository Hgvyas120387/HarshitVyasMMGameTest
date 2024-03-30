using UnityEngine;
/// <summary>
/// To start any coroutine please use this class and never ever disable this game object
/// </summary>
namespace cyberspeed
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton == null)
                Singleton = this;
            else
                Debug.LogError("Multiple CoroutineManager found only first one will be used rest will be ignored please verify");
        }
    }
}