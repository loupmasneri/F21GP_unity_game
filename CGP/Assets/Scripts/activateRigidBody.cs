using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateRigidBody : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody[] rigidbodies;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateRigidBody()
    {
        foreach (Rigidbody body in rigidbodies)
        {
            body.useGravity = true;
        }
    }
}
