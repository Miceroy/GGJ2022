using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public PlayerController player;

    Vector3 lightDir;
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
    }
    GameController getGameController()
    {
        return getGameObject("GameController").GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lightDir = -getGameObject("MainLight").transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + (10 * lightDir), Color.red);
        //Debug.DrawLine(transform.position, transform.position + (-10 * lightDir), Color.green);
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        int layerMask = 0;
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 pos = transform.position + (0.5f * lightDir);
        if (Physics.Raycast(pos, lightDir, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(pos - (0.5f * lightDir), lightDir * hit.distance, Color.cyan);
            getGameController().playerNotHitsLight(player, this);
        }
        else
        {
            Debug.DrawRay(pos - (0.5f * lightDir), lightDir * 1000, Color.magenta);
            getGameController().playerHitsLight(player, this);
        }
    }
}
