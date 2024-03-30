using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.Pooling
{
    public interface IPoolService : IService
    {
        public GameObject Instantiate(string tag);
        public AudioSource GetAudioSource();
    }
}