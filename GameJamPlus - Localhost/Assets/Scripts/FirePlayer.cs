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
        if (collision.gameObject.tag == "Player")
        {
            if ((collision.gameObject.name == "Player_SinglePlayer"))
            {
                collision.gameObject.transform.tag = "Enemy";
                PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
                player.Transformation();
                player.isInfected = true;
            }
            else
            {
                collision.gameObject.transform.tag = "Enemy";
                collision.gameObject.GetComponent<Bot>().Transformation();

            }
           
        }

        if (collision.gameObject.tag == "Ground")
        {
            rbBala.isKinematic = true;
            FindObjectOfType<AudioManager>().Play("sfx_spit_impact3");
        }
    }
}
