using System.Collections.Generic;
using UnityEngine;
using cyberspeed.Services;

namespace cyberspeed.Pooling
{
    public class ObjectPooler : MonoBehaviour, IPoolService
    {
        [Tooltip("Please make sure to have enough objects in the pool")]
        [SerializeField] private Pool[] pools;
        [Tooltip("Please make sure to have enough audio source in the pool")]
        [SerializeField] private int audioSourceCount = 10;
        [SerializeField] private AudioSource pfAudioSource;
        //we store all the pooled objects in this dictionary
        private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
        //we store all the pooled audio source in this queue
        private Queue<AudioSource> poolAudioSource = new Queue<AudioSource>();
        
        private void Start()
        {
            //instantiate and add to queue few audio source
            for (int i = 0; i < audioSourceCount; i++)
                poolAudioSource.Enqueue(Instantiate<AudioSource>(pfAudioSource, transform));

            CreatePool();
            //register this service
            ServiceLocator.Singleton.Register<IPoolService>(this);
        }

        private void CreatePool()
        {
            foreach (Pool pool in pools)
            {
                //instantiate all the object we want in the pool
                Queue<GameObject> poolItems = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject go = Instantiate(pool.pfPoolObj, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(transform);
                    go.SetActive(false);
                    poolItems.Enqueue(go);
                }
                //add this pool in the dictionary
                poolDict.Add(pool.tag, poolItems);
            }
        }

        public AudioSource GetAudioSource()
        {
            AudioSource audioSource = poolAudioSource.Dequeue();
            poolAudioSource.Enqueue(audioSource);
            return audioSource;
        }

        public T Instantiate<T>(string tag) where T : MonoBehaviour
        {
            //Dequeue the object to give
            GameObject go = poolDict[tag].Dequeue();
            //Enqueue it again so it will come back at last in case we want to use again
            poolDict[tag].Enqueue(go);
            go.SetActive(true);
            //give correct component
            return go.GetComponent<T>();
        }

        public void ReturnToPool(GameObject go)
        {
            go.transform.SetParent(transform);
            go.SetActive(false);
        }
    }
}