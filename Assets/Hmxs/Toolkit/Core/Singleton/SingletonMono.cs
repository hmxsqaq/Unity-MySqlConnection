using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hmxs.Toolkit
{
    /// <summary>
    /// 泛型单例基类-继承Mono
    /// 采用Lazy进行实例化保证线程安全
    /// </summary>
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;

                _instance = new Lazy<GameObject>(new GameObject(typeof(T).Name)).Value.AddComponent<T>();
                _instance.OnInstanceCreate(_instance);
                return _instance;
            }
        }

        /// <summary>
        /// 单例被第一次调用后调用该方法
        /// </summary>
        protected virtual void OnInstanceCreate(T instance) {}
    }
}