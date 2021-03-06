﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;
    [SerializeField] public List<GameObject> infecteds;

    [Header("Time Variables")]
    [SerializeField] private float startTimeToInfect;
    private float timeToInfect;
    [SerializeField] private float startGameTime;
    private float gameTime;

    [Header("Game Variables")]
    private bool hasStarted; 

    bool stopInfect = false;

    [Header("Time Infectado")]
    public float timeInfectado = 180f;
    public float min, sec;
    public Text txtTimer;
    public int infectedCount;


    [Header("PlayerWin Condiction")]
    public GameObject panelWin;
    public GameObject panelLose;

    [Header("GamePause")]
    public GameObject panelPause;
    public bool isPause = false;


    private void Start()
    {
        //panel de vitoria desligado
        panelWin.SetActive(false);
        //Tempo escala normal
        Time.timeScale = 1;

        //panelPause desligado
        panelPause.SetActive(false);

        //players = new List<GameObject>();
        // Inicializa o tempo para infectar o primeiro player
        timeToInfect = startTimeToInfect;
        gameTime = startGameTime;
    }

    private void Update()
    {
        if(infecteds.Count == players.Count)
        {
            YouLOSE();
        }

        InfectSomeone();

        // Se o jogo começou
        if (hasStarted)
        {
            CountDown();
        }

        if (timeInfectado <= 0)
        {
            //SIMPLIFICANDO... O PLAYER GANHOU!
            // infectado perde e todos infectados morrem  
            // Tela GameOver, sobreviventes Wins
            Invoke("playerWIN", 2f);
        }
        //timer to txt  
         min = Mathf.FloorToInt(timeInfectado / 60);
         sec = Mathf.FloorToInt(timeInfectado % 60);
         txtTimer.text = string.Format("{0:00}:{1:00}", min, sec);

        //void Pause
        gamePause();
    }

    void gamePause()
    {
        if (isPause)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0.00000000000000000000001f;
            
        }
        else
        {
            Time.timeScale = 1f;
            panelPause.SetActive(false);
        }
    }

    public void btnMenu()
    {
        SceneManager.LoadScene("Scene_MenuSinglePlayer");
    }

    public void btnPause()
    {
        //btn pause e dispausa
        isPause = !isPause;
    }

    void pplayerWin()
    {
        FindObjectOfType<AudioManager>().Play("EndgameMusic");
        //chama painel de vitoria
        panelWin.SetActive(true);
        // congela o jogo 
        Time.timeScale = 0.00000000000000000001f;

    }

    // Função para infectar alguém no inicio
    public void InfectSomeone()
    {
        timeToInfect -= Time.deltaTime;

        if(timeToInfect <= 0 && !stopInfect)
        {
            int rand = Random.Range(0, players.Count-1);
            players[rand].gameObject.tag = "Enemy";
            Bot bot = players[rand].gameObject.GetComponent<Bot>();
            bot.Transformation();
        
            stopInfect = true;
            hasStarted = true;
        }

    }

    public void CountDown()
    {
        timeInfectado -= Time.deltaTime;
        VerifyWin();
    }

    public void VerifyWin()
    {
        if (timeInfectado <= 0)
        {
            foreach (GameObject player in players)
            {
                if (player.tag == "Player")
                {
                    // Tem um player vivo, 
                    //Player ganhou!!
                    return;
                }
                else if (player.tag == "Enemy")
                {
                    infectedCount++;
                }
            }

            if (infectedCount >= players.Count)
            {
                YouLOSE();
            }
        }
    }

    void YouLOSE()
    {
        //VOCE PERDEU! JOGO SIMPLIFICADO

        //Parar o tempo sem perder as funçoes dos botoes
        Time.timeScale = 0.000000000000000001f;
        //Chama painel de derrota
        panelLose.SetActive(true);


    }
}
