using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public float radius = 1;
    public IEffectHandler effect;

    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
    }

    GameController gameCtrl;

    private void Start()
    {
        gameCtrl = getGameObject("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        gameCtrl.checkPlayerNearAction(this);
    }
}
