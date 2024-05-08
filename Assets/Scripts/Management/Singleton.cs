using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Storage a present instance of the class
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        // If there is already an instance of the class, destroy the new one
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = (T)this;
        }

        // If the object is not a child of another object, don't destroy it when loading a new scene
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

    }

}
