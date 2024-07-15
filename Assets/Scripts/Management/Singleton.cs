using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Storage a present Instance of the class
    public static T instance; 
    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()
    {
        // If there is already an Instance of the class, destroy the new one
        if (Instance != null && this.gameObject != null && this.gameObject.IsUnityNull())
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
