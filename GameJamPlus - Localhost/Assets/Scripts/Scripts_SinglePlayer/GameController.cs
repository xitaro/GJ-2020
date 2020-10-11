using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;

    [Header("Time Variables")]
    [SerializeField] private float startTimeToInfect;
    private float timeToInfect;
    [SerializeField] private float startGameTime;
    private float gameTime;

    [Header("Game Variables")]
    private bool hasStarted; 

    bool stopInfect = false;

    public float timeInfectado = 180f;

    private void Start()
    {
        //players = new List<GameObject>();
        // Inicializa o tempo para infectar o primeiro player
        timeToInfect = startTimeToInfect;
        gameTime = startGameTime;
    }

    private void Update()
    {
        timeInfectado -= Time.deltaTime;

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
            
        }

    }

    // Função para infectar alguém no inicio
    public void InfectSomeone()
    {
        timeToInfect -= Time.deltaTime;

        if(timeToInfect <= 0 && !stopInfect)
        {
            int rand = Random.Range(0, players.Count-1);
            players[rand].gameObject.tag = "Enemy";
            stopInfect = true;
            hasStarted = true;
        }

    }

    public void CountDown()
    {
        gameTime -= Time.deltaTime;
        VerifyWin();
    }

    public void VerifyWin()
    {
        if (gameTime <= 0)
        {
            foreach (GameObject player in players)
            {
                if(player.tag == "Player")
                {
                    //Player ganhou!!
                    return;
                }
                else
                {
                    Debug.Log("Não é player");
                }
            }
        }
    }
}
