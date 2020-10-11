using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 100;
   // public gameScript _gameScript;

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();
        //_gameScript = FindObjectOfType<gameScript>();

    }

    public void Start()
    {

        rbBala.AddForce(transform.forward * explosionForce, ForceMode.Impulse);

    }
    public void Update()
    {
       // rbBala.transform.Translate(0, 0, 10 * Time.deltaTime);
    }

   private void OnCollisionEnter(Collision collision)
    {
       /* if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.tag = "Enemy";
            _gameScript.timeInfectado += 5f;
        }*/

        if (collision.gameObject.tag == "Ground")
        {
            rbBala.isKinematic = true;
        }
    }
}
