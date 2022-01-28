using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    Vector3 lightDir;
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
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
        Vector3 pos = transform.position - 1 * lightDir;
        if (Physics.Raycast(pos, lightDir, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(pos, lightDir * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(pos, lightDir * 1000, Color.yellow);
            Debug.Log("Did not Hit");
        }
    }
}
