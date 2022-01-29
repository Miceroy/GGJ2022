using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public PlayerController player;

    Vector3 lightDir;
    bool hitsLight = false;
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
    }
    GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Debug.AssertFormat(gos.Length > 0, "Did not found game object of type: " + tag);
        return gos;
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
        hitsLight = false;
        //Debug.DrawLine(transform.position, transform.position + (10 * lightDir), Color.red);
        //Debug.DrawLine(transform.position, transform.position + (-10 * lightDir), Color.green);
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;

        int layerMask = LayerMask.GetMask("PushableObject", "PushableLight");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 pos = transform.position + (0.5f * lightDir);
        if (Physics.Raycast(pos, lightDir, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(pos - (0.5f * lightDir), lightDir * hit.distance, Color.cyan);
            GameObject[] gos = getGameObjects("PushableLight");
            foreach (GameObject go in gos)
            {
                Vector3 direction = (go.transform.position - pos).normalized;
                float distance = (go.transform.position - pos).magnitude;
                if (Physics.Raycast(pos, direction, out hit, distance, layerMask))
                {
                    Debug.DrawRay(pos - (0.5f * direction), direction * hit.distance, Color.cyan);
                }
                else
                {
                    if (distance < go.GetComponent<PushableLight>().radius)
                    {
                        Debug.DrawRay(pos - (0.5f * direction), direction * distance, Color.magenta);
                        Debug.Log("Is in pushable light!");
                        hitsLight = true;
                    }
                    else
                    {
                        Debug.DrawRay(pos - (0.5f * direction), direction * hit.distance, Color.cyan);
                    }
                }
            }
        }
        else
        {
            Debug.DrawRay(pos - (0.5f * lightDir), lightDir * 1000, Color.magenta);
            hitsLight = true;
        }

        

        if (hitsLight)
        {
            getGameController().playerNotHitsLight(player, this);
        }
        else
        {
            getGameController().playerHitsLight(player, this);
        }
    }
}
