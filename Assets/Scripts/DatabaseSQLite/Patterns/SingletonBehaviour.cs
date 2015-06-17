using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>, new()
{
    
    // Use this for initialization
    public static T shared = null;

    void Awake()
    {
//        Debug.Log(name + " is awakening");

        if(shared == null)
        {
            shared = (T)(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static T Shared()
    {   
        return shared;
    }
}
