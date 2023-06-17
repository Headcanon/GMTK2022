using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance) throw new System.Exception("NÃO PODE TER DOIS " + Instance.GetType() + ", é uma Singleton");
        Instance = this as T;
    }

    private void OnDestroy() { if (Instance == this) Instance = null; }
}
