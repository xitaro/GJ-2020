using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    public Rigidbody rbBala;
    public float explosionForce = 10;

    public BeatEmUpMovement_SinglePlayer _player;

    public void Awake()
    {
        rbBala = FindObjectOfType<Rigidbody>();

    }

    public void Start()
    {

        rbBala.AddForce(_player.transform.forward * explosionForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.tag = "Enemy";
        }
    }
}
