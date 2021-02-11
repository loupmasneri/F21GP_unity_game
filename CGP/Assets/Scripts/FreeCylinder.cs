using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCylinder : MonoBehaviour
{
    public GameObject cylynderSupport;
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
        cylynderSupport.transform.Translate(Vector3.forward * Time.deltaTime * -300);
    }
}
