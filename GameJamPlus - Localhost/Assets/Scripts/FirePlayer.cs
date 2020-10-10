using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 100;

    public BeatEmUpMovement_SinglePlayer _player;

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();

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
            collision.gameObject.transform.tag = "Enemy";
        }
    }
}
