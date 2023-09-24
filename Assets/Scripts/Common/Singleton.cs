using UnityEngine;

namespace Common
{
    /// <summary>
    /// A static instance is similar to a singleton, but instead of destroying any new instances it overwrites the current instance. This is helpful for resetting the state.
    /// </summary>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Basic singleton pattern. Will destroy any new versions of that class created, leaving only the original instance intact.
    /// </summary>
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            base.Awake();
        }
    }

    /// <summary>
    /// A persistent version of a <see cref="Singleton{T}"/> that will survive across scene loads.
    /// Perfect for system classes which require stateful, persistent data. Or audio sources where music plays through loading screens etc. 
    /// </summary>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}