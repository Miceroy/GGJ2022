using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public MonoBehaviour[] handlerComponents;
    public float radius = 1;

    List<IEffectHandler> effects;

    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
    }

    public void actAll()
    {
        foreach (IEffectHandler effec in effects)
        {
            effec.act();
        }
    }

    GameController gameCtrl;

    private void Start()
    {
        gameCtrl = getGameObject("GameController").GetComponent<GameController>();

        effects = new List<IEffectHandler>();
        foreach (MonoBehaviour beh in handlerComponents)
        {
            IEffectHandler effect = beh.GetComponent<IEffectHandler>();
            Debug.AssertFormat(effect != null, "Effect for " + gameObject.name + " not found!");
            effects.Add(effect);
        }

        Debug.AssertFormat(effects.Count > 0, "Not effects set for InteractController: " + gameObject.name + "!");
    }
    private void Update()
    {
        gameCtrl.checkPlayerNearAction(this);
    }
}
