using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSphereRain : MonoBehaviour
{
    public GameObject door;
    public activateRigidBody activateRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        door.transform.Translate(Vector3.down * Time.deltaTime * 10);
        activateRigidBody.ActivateRigidBody();
    }
}
