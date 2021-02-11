using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorites : MonoBehaviour
{
    public float delay = 0.05f;
    public GameObject meteorites;

    // Start is called before the first frame update
    void Start()
    {
        meteorites.AddComponent<Rigidbody>();
        meteorites.GetComponent<Rigidbody>().AddForce(Physics.gravity * 1f, ForceMode.Acceleration);
        meteorites.tag = "EnemySphere";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(meteorites, new Vector3(Random.Range(51, 123), Random.Range(70, 90), Random.Range(66, 70)), Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke("Spawn");
            InvokeRepeating("Spawn", delay, delay);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke("Spawn");
        }
    }
}
