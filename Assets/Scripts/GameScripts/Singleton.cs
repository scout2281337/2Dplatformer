using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance {  get; private set; }

    protected virtual void Awake()
    {
        // If an instance already exists and it's not this one, destroy this duplicate
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Duplicate instance of {typeof(T).Name} was destroyed");
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
    }
}
