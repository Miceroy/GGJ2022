using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour, IEffectHandler
{
    public bool isBridgeOpen = false;
    public string animationToToggle = "OpenBridge";

    float cooldown;

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void Start()
    {
        if( isBridgeOpen )
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool(animationToToggle, !animator.GetBool(animationToToggle));
        }
    }

    public void act()
    {
        if (cooldown < 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool(animationToToggle, !animator.GetBool(animationToToggle));
            cooldown = 0.5f;
            Debug.Log(animationToToggle + " is now: " + animator.GetBool(animationToToggle).ToString() );
        }
    }
}
