using System.Collections.Generic;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.Pooling
{
    public class ObjectPooler : MonoBehaviour, IPoolService
    {
        [SerializeField] private Pool[] pools;
        [SerializeField] private int audioSourceCount = 10;
        [SerializeField] private AudioSource pfAudioSource;
        private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
        private Queue<AudioSource> poolAudioSource = new Queue<AudioSource>();
        
        private void Start()
        {
            for (int i = 0; i < audioSourceCount; i++)
                poolAudioSource.Enqueue(Instantiate<AudioSource>(pfAudioSource, transform));

            CreatePool();
            ServiceLocator.Singleton.Register<IPoolService>(this);
        }

        private void CreatePool()
        {
            foreach (Pool pool in pools)
            {
                Queue<GameObject> poolItems = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject go = Instantiate(pool.pfPoolObj, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(transform);
                    go.SetActive(false);
                    poolItems.Enqueue(go);
                }
                poolDict.Add(pool.tag, poolItems);
            }
        }

        public GameObject Instantiate(string tag)
        {
            GameObject go = poolDict[tag].Dequeue();
            poolDict[tag].Enqueue(go);
            go.SetActive(true);
            return go;
        }

        public AudioSource GetAudioSource()
        {
            AudioSource audioSource = poolAudioSource.Dequeue();
            poolAudioSource.Enqueue(audioSource);
            return audioSource;
        }
    }
}