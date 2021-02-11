using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInside : MonoBehaviour
{
    public Material m_material;
    public GameObject nextLevelDoor;
    public GameObject arch;
    public Animator doorAnimator;
    public Animator archAnimator;
    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("win");
        m_material.color = Color.green;
        doorAnimator.SetTrigger("isTriggered");
        archAnimator.SetTrigger("isTriggered");
    }
}
