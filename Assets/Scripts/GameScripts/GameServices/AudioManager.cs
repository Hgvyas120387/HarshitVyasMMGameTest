using cyberspeed.Services;
using UnityEngine;
using cyberspeed.Pooling;

namespace cyberspeed.MatchGame
{
    public class AudioManager : IAudioService
    {
        public void PlayAudioOneShot(AudioClip audioClip)
        {
            AudioSource audioSource = ServiceLocator.Singleton.Get<IPoolService>().GetAudioSource();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
