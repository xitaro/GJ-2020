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
    [SerializeField] float Timer,TotalPartida;
    public float timeInfectado = 120f;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        timeInfectado -= Time.deltaTime;
        players = GameObject.FindGameObjectsWithTag("Player");
        time += Time.deltaTime;
        i = players.Length;
        if (time <= Timer-1)
        {
           // players = GameObject.FindGameObjectsWithTag("Player");
            a = Random.Range(0, i);
            spaw = players[a];
        }
        if (time>=Timer && b == true)
        {
            spaw.transform.gameObject.tag = "Enemy";
            b = false;
        }

        if(i==0)
        {
           
        }
        // 1 tempo apenas para os monstros, ele ganha qdo contaminar todos e ganha mais tempo a cada player/bot infectado
        // Se o tempo acabar os sobreviventes ganham e o infectado ganha qdo contaminar todos
        
    

    }
 
}
