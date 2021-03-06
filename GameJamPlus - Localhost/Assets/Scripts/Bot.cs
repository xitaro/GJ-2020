﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    
    //public Transform[] points;
    private int nextpoint;
    private Transform target;
    private float DistancePerception = 50;


    [Header("Models")]
    [SerializeField] private GameObject doctorModel;
    [SerializeField] private GameObject infectedModel;

    [Header("Infected")]
    [SerializeField] private bool isInfected;
    private bool canShoot = true;
    public GameObject prefBala;
    public Transform fireTransform;

    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameController gameController;

    private bool chasing,test=true;
    private float visionEnemy = 360f, distance=100f, distanceP=40;
    private int actualPoint = 0;
    public float time;
    public List<GameObject> goList,Pointss;

    private Transform lastPlayerSee;
    public  NavMeshAgent navAgent;
    private bool B=true;
    GameObject player;
    GameObject Enemy;

   
   void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        actualPoint = Random.Range(0, 4);
        target = Pointss[actualPoint].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.speed > 0.01f)
        {
            anim.SetFloat("Speed", navAgent.speed);
        }

        // Se a tag for inimigo
    
            // Está infectado
          


            // Se transforma
     
        
        // Se está infectado
        if (isInfected)
        {
            // Começa a caçar
            cassando();
        }
        else moving(); // Se não, se move

        // Se está perseguindo
        if (chasing)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                //B = true;
                chasing = false;
                time = 0;
            }
        }
    }

    void cassando()
    {  
        navAgent.speed = 7f;

        if (B == true)
        {
            target = Pointss[actualPoint].transform;
            if (Vector3.Distance(transform.position, target.position) > 3f)
            {
                ///actualPoint = Random.Range(1, 10);
                navAgent.destination = target.position;
            }
            if (Vector3.Distance(transform.position, target.position) < 3f)
            {
                actualPoint = Random.Range(0, 20);
            }
        }
        //GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        // player.Add(GameObject.FindGameObjectWithTag("Player"));
        int L;
        L = goList.Count;
        //if (test)
        //{
            for (int A = 0; A < L; A++)
            {
   
                if (Vector3.Distance(transform.position, goList[A].transform.position) < distanceP && goList[A].transform.tag=="Player")
                {
                    player = goList[A];
                }

                distanceP = Vector3.Distance(transform.position, goList[A].transform.position);
                if(goList[A].transform.tag == "Enemy")
            {
                goList.Remove(goList[A]);
            }

            }
        //}


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
                test = false;
                if (canShoot == true) {
                    Invoke("Fire", 2f);
                    canShoot = false;
                }
                
            }
         
            //Persegue o último lugar que viu o player
           else if (chasing == true)
            {
                navAgent.SetDestination(lastPlayerSee.transform.position);
                Invoke("Ronda", 3f);
            }
         
        }

    }
    void Ronda()
    {
       // test = true;
        B = true;
    }

    void Fire()
    {
        //Atira
        FindObjectOfType<AudioManager>().Play("sfx_spit_wvoice");
        Instantiate(prefBala,fireTransform.position, fireTransform.rotation);
        Invoke("FireAgain", 4f);
        anim.SetTrigger("Shoot");
    }

    void FireAgain()
    {
        canShoot = true;
    }
 
    void moving()
    {

        int Co;
        Co= Pointss.Count;
        if (Vector3.Distance(transform.position, target.position) > 4f)
        {
            ///actualPoint = Random.Range(1, 10);
            navAgent.destination = target.position;
        }
        if (Vector3.Distance(transform.position, target.position) < 4f)
        {
            actualPoint = Random.Range(0, Co);
            target = Pointss[actualPoint].transform;
        }

        //GameObject Enemy;
        
        bool C = false;
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Enemy");
        // player.Add(GameObject.FindGameObjectWithTag("Player"));
        int L;
        //L = players.Length;
        L = goList.Count;
        for (int A = 0; A < L; A++)
        {
            if (Vector3.Distance(transform.position, goList[A].transform.position) < distanceP && goList[A].transform.tag == "Enemy")
            {
                Enemy = goList[A];
                C = true;
            }

            distanceP = Vector3.Distance(transform.position, goList[A].transform.position);

        }

        if (C)
        {
            Vector3 direction = Enemy.transform.position - navAgent.transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            float DistancePlayer = Vector3.Distance(Enemy.transform.position, navAgent.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, 1000) && DistancePlayer < DistancePerception)
            {

                if (((hit.collider.gameObject.CompareTag("Enemy")) && (angle < visionEnemy)))
                {
                    for (int i = 0; i < Co; i++)
                    {
           
                    if (Vector3.Distance(Enemy.transform.position, Pointss[i].transform.position) > distance)
                    {
                        target = Pointss[i].transform;
                    }
                    
                        distance = Vector3.Distance(Enemy.transform.position, Pointss[i].transform.position);
                        

                    }

                }


            }
        }
    }

    public void Transformation()
    {
        //
        gameController.infecteds.Add(this.gameObject);
        // Desativa o model de doutor
        doctorModel.SetActive(false);
        // Ativa o model de infectado
        infectedModel.SetActive(true);
        FindObjectOfType<AudioManager>().Play("sfx_infected");
        isInfected = true;
        //Troca o animator
        anim = infectedModel.GetComponent<Animator>();
    }

    // Ao colidir em algo
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a tag é Goop
        if(collision.gameObject.tag == "Goop")
        {
            // Diz que está infectado
            isInfected = true;
            // Seta a tag para Enemy
            this.gameObject.tag = "Enemy";
        }
    }

}
