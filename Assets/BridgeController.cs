using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour, IEffectHandler
{
    public Transform end1;
    public Transform end2;

    bool isClosed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Bridge rot: " + end1.rotation);
        if (isClosed)
        {
            Debug.Log("TODO Bridge Closing: " + end1.rotation);
        }
    }


    public void act()
    {
        isClosed = !isClosed;
        Debug.Log("Bridge is now " + (isClosed ? "Closed" : "Open"));
    }
}
