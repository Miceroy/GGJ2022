using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableLight : MonoBehaviour, IEffectHandler
{
    public GameObject light;
    public float radius = 4;
    float cooldown;
    bool lightOn;

    public bool isLightActive()
    {
        return lightOn;
    }

    void Start()
    {
        lightOn = true;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void act()
    {
        if (cooldown < 0)
        {
            Animator animator = GetComponent<Animator>();
            if (lightOn)
            {
                light.SetActive(false);
                lightOn = false;
            }
            else 
            {
                light.SetActive(true);
                lightOn = true;
            }
            //getCom//animator.SetBool(animationToToggle, !animator.GetBool(animationToToggle));
            cooldown = 0.5f;
            //Debug.Log(animationToToggle + " is now: " + animator.GetBool(animationToToggle).ToString());
        }
        Debug.Log("PushableLight is now: " + isLightActive().ToString());
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
