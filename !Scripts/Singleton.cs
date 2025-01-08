using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
