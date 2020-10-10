using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireScript : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 10;

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();
    }

    public void Start()
    {
        rbBala.AddForce(transform.forward * explosionForce, ForceMode.Impulse);
    }



}
