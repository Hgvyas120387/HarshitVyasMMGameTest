using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.Pooling
{
    public interface IPoolService : IService
    {
        /// <summary>
        /// to get the audio source from the pool
        /// </summary>
        /// <returns></returns>
        public AudioSource GetAudioSource();
        /// <summary>
        /// to instantiate game object from the pool
        /// </summary>
        /// <typeparam name="T">any class derived from MonoBehaviour</typeparam>
        /// <param name="tag">pool tag to identify which object needs to instantiate</param>
        /// <returns>returns T please make sure GameObject has T as Component</returns>
        public T Instantiate<T>(string tag) where T : MonoBehaviour;
        /// <summary>
        /// Never ever delete the pooled object instead use this method so we can re use it later
        /// </summary>
        /// <param name="go">object which we want to return in pool and no longer want to use</param>
        public void ReturnToPool(GameObject go);
    }
}