using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    
    public Transform[] points;
    private int nextpoint;
    private Transform target;
    private float DistancePerception = 30;
    public GameObject Skin1, Skin2;
   
    private bool chasing;
    private float visionEnemy = 360f, distance=100f,actual, distanceP=100,DistanceE=100;
    private int actualPoint = 0;
    public float time;
 
    private Transform lastPlayerSee;
    public  NavMeshAgent navAgent;
    private bool B=true;
    GameObject player;
    GameObject Enemy;

    private bool canShoot = true;
    public GameObject prefBala;
    public Transform fireTransform;
   
    

    
    // Start is called before the first frame update
    void Start()
    {
       
        actualPoint = Random.Range(0, 4);
        target = points[actualPoint];
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameObject.tag == "Enemy")
        {
            cassando();
        }
        else moving();

        if (chasing)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                B = true;
                chasing = false;
                time = 0;
            }
        }


    }

    void cassando()
    {
        Skin1.SetActive(false);
        Skin2.SetActive(true);
        navAgent.speed = 2f;

        if (B == true)
        {
            target = points[actualPoint];
            if (Vector3.Distance(transform.position, target.position) > 1.5f)
            {
                ///actualPoint = Random.Range(1, 10);
                navAgent.destination = target.position;
            }
            if (Vector3.Distance(transform.position, target.position) < 1.5f)
            {
                actualPoint = Random.Range(0, 21);
            }
        }
        GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        // player.Add(GameObject.FindGameObjectWithTag("Player"));
        int L;
        L = playerss.Length;
        for (int A = 0; A < L; A++)
        {  
                if (Vector3.Distance(transform.position, playerss[A].transform.position) < distanceP)
                    player = playerss[A];
            
            distanceP = Vector3.Distance(transform.position, playerss[A].transform.position);

        }


        Vector3 direction = player.transform.position - navAgent.transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        float DistancePlayer = Vector3.Distance(player.transform.position, navAgent.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 1000) && DistancePlayer < DistancePerception)
        {
            //Persegue o player se estiver em vista
            if (((hit.collider.gameObject.CompareTag("Player")) && (angle < visionEnemy)))
            {
                lastPlayerSee = player.transform;
                navAgent.SetDestination(player.transform.position);
                Vector3 look = navAgent.steeringTarget;
                navAgent.transform.LookAt(look);
                chasing = true;
                B = false;
                //Atirar
               
                if (canShoot == true) {
                    Invoke("Fire", 2f);
                    canShoot = false;
                }
                
            }
         
            //Persegue o último lugar que viu o player
           else if (chasing == true)
            {
                navAgent.SetDestination(lastPlayerSee.transform.position);     
            }
         
        }

    }

    void Fire()
    {
        //Atira
        Instantiate(prefBala, new Vector3(fireTransform.position.x, fireTransform.position.y, fireTransform.position.z), Quaternion.identity);
        Invoke("FireAgain", 10f);

    }

    void FireAgain()
    {
        canShoot = true;
    }
 
    void moving()
    {

        
        if (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            ///actualPoint = Random.Range(1, 10);
            navAgent.destination = target.position;
        }
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            actualPoint = Random.Range(0, 21);
            target = points[actualPoint];
        }

        //GameObject Enemy;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Enemy");
        // player.Add(GameObject.FindGameObjectWithTag("Player"));
        int L;
        L = players.Length;
        for (int A = 0; A < L; A++)
        {
            if (Vector3.Distance(transform.position, players[A].transform.position) < DistanceE)
                Enemy = players[A];

            DistanceE = Vector3.Distance(transform.position, players[A].transform.position);

        }

        Vector3 direction = Enemy.transform.position - navAgent.transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        float DistancePlayer = Vector3.Distance(Enemy.transform.position, navAgent.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 1000) && DistancePlayer < DistancePerception)
        {
            
            if (((hit.collider.gameObject.CompareTag("Enemy")) && (angle < visionEnemy)))
            {
                for(int i=0;i<21;i++)
                {   
                    actual= Vector3.Distance(Enemy.transform.position, target.position);

                    if (Vector3.Distance(Enemy.transform.position, points[i].position)>= actual)
                    {
                        if(Vector3.Distance(Enemy.transform.position, points[i].position)>distance)
                        target = points[i];
                    }
                    distance = Vector3.Distance(Enemy.transform.position, points[i].position);

                }

            }


        }
    }
  
  
}
