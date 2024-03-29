using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyberspeed.Services
{
    public class ServiceLocator
    {
        //to hold all of the our services
        private Dictionary<string, IService> services = new Dictionary<string, IService>();
        //private constructor so no one out side the class can create instance
        private ServiceLocator() { }
        //singleton
        private static ServiceLocator instance;
        public static ServiceLocator Singleton
        {
            get
            {
                if (instance == null)
                    instance = new ServiceLocator();
                return instance;
            }
        }
        /// <summary>
        /// Get the service of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to get.</typeparam>
        /// <returns>The service instance interface</returns>
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError("No service of this type has been registered please verify");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }
        /// <summary>
        /// Registers the service with the current service locator
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance interface</param>
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (services.ContainsKey(key))
            {
                Debug.LogError($"trying to register service of type {key} which is already registered ignoring this please verify");
                return;
            }

            //Add
            services.Add(key, service);
        }
        /// <summary>
        /// Unregister the service from the current service locator
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"trying to unregister service of type {key} which is not registored ignoring this please verify");
                return;
            }

            services.Remove(key);
        }
    }
}