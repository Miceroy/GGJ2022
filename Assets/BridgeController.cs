using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour, IEffectHandler
{
    float cooldown;

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }


    public void act()
    {
        if (cooldown < 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("OpenBridge", !animator.GetBool("OpenBridge"));
            cooldown = 0.5f;
            Debug.Log("Bridge State is now: " + animator.GetBool("OpenBridge").ToString() );
        }
    }
}
