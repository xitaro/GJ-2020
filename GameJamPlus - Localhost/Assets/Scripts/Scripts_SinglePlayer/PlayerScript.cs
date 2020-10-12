using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private BeatEmUpMovement_SinglePlayer movementScript;
    [SerializeField] private Button fireBtn;
   
    [Header("Fire")]
    bool shootRequest;
    public GameObject bulletPrefab;
    public Transform transformForward;

    [Header("Models")]
    [SerializeField] private GameObject doctorSkin;
    [SerializeField] private GameObject infectedSkin;
    [SerializeField] private GameObject navMeshObject;

    [Header("Infected")]
    [SerializeField] public bool isInfected;
    [SerializeField] private float startInfectedTime = 10f;
    private float infectedTime;

    [Header("Lose Condiction")]
    public GameObject painelLose;

    public void Start()
    {
        //Tempo do jogo normal
        Time.timeScale = 1;
        //painel de derrota desativado
        painelLose.SetActive(false);
    }

    private void Update()
    {
        // Se a tag for inimigo
  
        // Se está infectado
         
    }

    private void FixedUpdate()
    {
       StartCoroutine(Shoot(shootRequest));
       shootRequest = false;
    }

    public void ShootRequest()
    {
        shootRequest = true;
    }

    public IEnumerator Shoot(bool shoot)
    {
        // If the player should fire
        if (isInfected && shoot)
        {
            Instantiate(bulletPrefab, transformForward.position, transformForward.rotation);
            // Animação de atirar
            infectedSkin.GetComponent<Animator>().SetTrigger("Shoot");
            FindObjectOfType<AudioManager>().Play("sfx_spit");
            //Invoke("FireAgain", 10f);
            yield return new WaitForSeconds(5f);
        }
    }

    /*public void Fire(bool shoot)
    {
        // If the player should fire
        if (isInfected && shoot)
        {
            Instantiate(bulletPrefab, transformForward.position, transformForward.rotation);
            Invoke("FireAgain", 10f);
        }
        
    }
    void FireAgain()
    {
        //Colocar uma imagem que pode atirar de novo!
        ShootRequest();
    }*/

    //Se colidir com algo
  

    private void CountDown()
    {
        infectedTime -= Time.deltaTime;

        if (infectedTime == 0)
        {
            // Die!!!
        }
    }

    public void Transformation()
    {
        // Desativa o model de doutor
        doctorSkin.SetActive(false);
        // Ativa o model de infectado
        infectedSkin.SetActive(true);
        //Troca animator
        movementScript.anim = infectedSkin.GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Play("sfx_infected");
        fireBtn.gameObject.SetActive(true);
        //JOGO SIMPLIFICADO VC MORREU
        //Invoke("YouLOSE", 5f);
        
    }

    void YouLOSE()
    {
        //VOCE PERDEU! JOGO SIMPLIFICADO

        //Parar o tempo sem perder as funçoes dos botoes
        Time.timeScale = 0.000000000000000001f;
        //Chama painel de derrota
        painelLose.SetActive(true);


    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a tag é Goop
        if (collision.gameObject.tag == "Goop")
        {
            // Diz que está infectado
            isInfected = true;
            // Seta a tag para Enemy
            navMeshObject.tag = "Enemy";
            // Transforma em infectado
            Transformation();
            //Inicializa o timer de infectado
            infectedTime = startInfectedTime;
        }
    }
}
