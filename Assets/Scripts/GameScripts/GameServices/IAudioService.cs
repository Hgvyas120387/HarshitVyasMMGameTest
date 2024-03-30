using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public interface IAudioService : IService
    {
        /// <summary>
        /// Play any clip you want should pool from available audiosource
        /// </summary>
        /// <param name="clip">audio clip to play</param>
        public void PlayAudioOneShot(AudioClip clip);
    }
}