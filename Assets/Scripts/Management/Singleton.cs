using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Storage a present instance of the class
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        // If there is already an instance of the class, destroy the new one
        if (Instance != null && this.gameObject != null && this.gameObject.IsUnityNull())
        {
            Destroy(this.gameObject);
        } else
        {
            Instance = (T)this;
        }

        // If the object is not a child of another object, don't destroy it when loading a new scene
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

    }

}
