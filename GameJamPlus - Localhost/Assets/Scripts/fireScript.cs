using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireScript : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 10;
    public Bot _bot;
 
    

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();
        _bot = FindObjectOfType<Bot>();
        
    }

    public void Start()
    {
        rbBala.AddForce(_bot.transform.forward * explosionForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.tag = "Enemy";
           
        }

        if (collision.gameObject.tag == "Ground")
        {
            rbBala.isKinematic = true;
        }
    }

}
