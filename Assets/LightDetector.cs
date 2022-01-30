using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public PlayerController player;

    Vector3 lightDir;
    //bool hitsLight = false;
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object: " + tag);
        return go;
    }
    GameObject[] getGameObjects(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        //Debug.AssertFormat(gos.Length > 0, "Did not found game object of type: " + tag);
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
   /* void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + (10 * lightDir), Color.red);
        //Debug.DrawLine(transform.position, transform.position + (-10 * lightDir), Color.green);
    }*/

    void Update()
    {
        bool hitsLight = false;
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;

        int layerMask = LayerMask.GetMask("PushableObject", "PushableLight", "IgnoreRaycast");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        float delta = 0.5f;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 pos = transform.position + (delta * lightDir);
        if (Physics.Raycast(pos, lightDir, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(pos - (delta * lightDir), lightDir * hit.distance, Color.red);
            hitsLight = false;
            //Debug.Log(Time.realtimeSinceStartup.ToString() + ": Not hits light: " + gameObject.name);
        }
        else
        {
            Debug.DrawRay(pos - (delta * lightDir), lightDir * 1000, Color.magenta);
            hitsLight = true;
            //Debug.Log(Time.realtimeSinceStartup.ToString() + ": Hits light:" + gameObject.name);
        }

        GameObject[] gos = getGameObjects("PushableLight");
        foreach (GameObject go in gos)
        {
            Vector3 direction = (go.transform.position - pos).normalized;
            float distance = (go.transform.position - pos).magnitude;
            if (Physics.Raycast(pos, direction, out hit, distance, layerMask))
            {
                Debug.DrawRay(pos - (delta * direction), direction * hit.distance, Color.cyan);
            }
            else
            {
                if (distance < go.GetComponent<PushableLight>().radius && go.GetComponent<PushableLight>().isLightActive())
                {
                    Debug.DrawRay(pos - (delta * direction), direction * distance, Color.magenta);
                    //Debug.Log("Is in pushable light!");
                    hitsLight = true;
                }
                else
                {
                    Debug.DrawRay(pos - (delta * direction), direction * hit.distance, Color.cyan);
                }
            }
        }

        if (hitsLight)
        {
            getGameController().hitsLight(player, this);
        }
        else
        {
            getGameController().notHitsLight(player, this);
        }
    }
}
