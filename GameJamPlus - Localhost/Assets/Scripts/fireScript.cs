using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireScript : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 10;
    public Bot _bot;
    public gameScript _gameScript;
    

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();
        _bot = FindObjectOfType<Bot>();
        _gameScript = FindObjectOfType<gameScript>();
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
            _gameScript.timeInfectado += 5f;
        }

        if (collision.gameObject.tag == "Ground")
        {
            rbBala.isKinematic = true;
        }
    }

}
