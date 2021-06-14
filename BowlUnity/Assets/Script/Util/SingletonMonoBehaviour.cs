using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance = null;

    public static T I
    {
        get
        {
            if (s_instance != null)
            {
                return s_instance;
            }
            CheckAndFindInstance();
            return s_instance;
        }
    }

    private static void CheckAndFindInstance()
    {
        if (s_instance != null)
        {
            return;
        }
        s_instance = (T)FindObjectOfType(typeof(T));
        if (s_instance == null)
        {
            Debug.LogError(typeof(T) + " is nothing");
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }

    protected virtual void Awake()
    {
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (this == I)
        {
            return true;
        }
        Destroy(this);
        return false;
    }

    public static bool IsValid()
    {
        return s_instance != null;
    }
}

