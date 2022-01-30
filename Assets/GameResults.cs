using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    private static GameResults _instance;

    public static GameResults Instance { get { return _instance; } }

    string[] levels = {
        "Scenes/Level1",
        "Scenes/Level2",
        "Scenes/Level3",
        "Scenes/Level4",
        "Scenes/Level5"
    };

    public int getLevelCount()
    {
        return levels.Length;
    }

    public string getLevelName()
    {
        return levels[lastLevel];
    }

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
    public int lastLevel = 0;
}
