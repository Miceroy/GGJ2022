using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableItemController : MonoBehaviour
{
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(direction.magnitude > 0)
        {
            transform.Translate(10 * direction * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        direction = (transform.position - other.transform.position).normalized;
        direction.y = 0;
        direction.Normalize();
    }

    private void OnTriggerExit(Collider other)
    {
        direction.Set(0,0,0);
    }

}
