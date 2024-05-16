using System;

namespace Hmxs.Toolkit
{
    /// <summary>
    /// 泛型单例基类
    /// 采用Lazy进行实例化保证线程安全
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = new Lazy<T>(true).Value;
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