using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableItemController : MonoBehaviour
{

    public Vector3 direction;

    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object of type: " + tag);
        return go;
    }

    private void Update()
    {
        if (direction.magnitude > 0)
        {
            transform.Translate(10 * direction * Time.deltaTime);
        }
    }

    GameController getGameController()
    {
        return getGameObject("GameController").GetComponent<GameController>();
    }
        
    private void OnTriggerEnter(Collider other)
    {
        getGameController().itemCollisionEnter(this,other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        getGameController().itemCollisionExit(this, other.gameObject);
    }
}
