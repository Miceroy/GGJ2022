using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object of type: " + tag);
        return go;
    }

    GameController getGameController()
    {
        return getGameObject("GameController").GetComponent<GameController>();
    }

    private void Update()
    {   
        getGameController().checkPlayerNearGoal(this);
    }
}
