﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;

    [Header("Time Variables")]
    [SerializeField] private float startTimeToInfect;
    private float timeToInfect;

    bool stopInfect = false;

    private void Start()
    {
        //players = new List<GameObject>();
        // Inicializa o tempo para infectar o primeiro player
        timeToInfect = startTimeToInfect;
    }

    private void Update()
    {
        InfectSomeone();
    }

    // Função para infectar alguém no inicio
    public void InfectSomeone()
    {
        timeToInfect -= Time.deltaTime;

        if(timeToInfect <= 0 && !stopInfect)
        {
            int rand = Random.Range(1, players.Count);
            players[rand].gameObject.tag = "Enemy";
            stopInfect = true;
        }

    }
}