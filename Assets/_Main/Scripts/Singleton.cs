using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject gObject = new GameObject(typeof(T).Name);
                    instance = gObject.AddComponent<T>();
                }
            }

            return instance;
        }
      
    }
}
