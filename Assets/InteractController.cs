using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public MonoBehaviour handlerComponent;
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
        effect = handlerComponent.GetComponent<IEffectHandler>();
        Debug.AssertFormat(effect != null, "Effect for " + gameObject.name + " not found!");
    }

    private void Update()
    {
        gameCtrl.checkPlayerNearAction(this);
    }
}
