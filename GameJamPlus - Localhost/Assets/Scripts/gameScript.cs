using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScript : MonoBehaviour
{
    float time;
    int i,a;
    bool b=true;
    public GameObject spaw;
    public GameObject[] players;
    [SerializeField] float Timer;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= Timer-1)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            i = players.Length;
            a = Random.Range(0, i);
            spaw = players[a];
        }
        if (time>=Timer && b == true)
        {
            spaw.transform.gameObject.tag = "Enemy";
            b = false;
        }
    }
 
}
