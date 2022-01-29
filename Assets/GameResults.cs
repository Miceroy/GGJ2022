using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    private static GameResults _instance;

    public static GameResults Instance { get { return _instance; } }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public bool didWin;
}
