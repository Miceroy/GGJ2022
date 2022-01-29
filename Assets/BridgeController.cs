using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour, IEffectHandler
{
    public float rotateSpeed = 45;
    public Transform end1;
    public Transform end2;
    enum State
    {
        OPENING,
        OPENED,
        CLOSING,
        CLOSED
    }
    State state;

    float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        end1.localEulerAngles = new Vector3(270, 0,0);
        end2.localEulerAngles = new Vector3(-270, 0,0);
        state = State.CLOSING;
    }

    void rotateBridge(Transform obj, float angle)
    {
        obj.Rotate(angle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        int stopCount = 0;
        switch (state)
        {
            case State.OPENING:
                end1.localEulerAngles = new Vector3(270, 0, 0);
                end2.localEulerAngles = new Vector3(-270, 0, 0);
                state = State.OPENED;
                break;
            case State.OPENED:
                break;
            case State.CLOSING:
                end1.localEulerAngles = new Vector3(0, 0, 0);
                end2.localEulerAngles = new Vector3(0, 0, 0);
                state = State.CLOSED;
                break;
            case State.CLOSED:
                break;
        }
        //Debug.Log("Bridge State is now: " + state.ToString());
    }


    public void act()
    {
        if (cooldown < 0)
        {
            switch (state)
            {
                case State.OPENING:
                    state = State.CLOSING;
                    break;
                case State.OPENED:
                    state = State.CLOSING;
                    break;
                case State.CLOSING:
                    state = State.OPENING;
                    break;
                case State.CLOSED:
                    state = State.OPENING;
                    break;
            }

            cooldown = 1;
            //isClosing = true; !isClosed;
            Debug.Log("Bridge State is now: " + state.ToString() );
        }
    }
}
