using UnityEngine;

public abstract class SingletoneBehaviour<T> : MonoBehaviour where T : class
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                #if UNITY_EDITOR
                    Debug.Log("Singletone Could not found GameObject of type " + typeof(T).Name);
                #endif
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }
}