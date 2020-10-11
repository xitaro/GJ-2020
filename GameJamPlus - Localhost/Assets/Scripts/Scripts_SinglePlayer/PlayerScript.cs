using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    

    [Header("Fire")]
    bool shootRequest;
    public GameObject bulletPrefab;
    public Transform transformForward;

    [Header("Models")]
    [SerializeField] private GameObject doctorSkin;
    [SerializeField] private GameObject infectedSkin;

    [Header("Infected")]
    [SerializeField] private bool isInfected;
    [SerializeField] private float startInfectedTime = 10f;
    private float infectedTime;
    

    private void Update()
    {
        // Se a tag for inimigo
        if (gameObject.tag == "Enemy")
        {
            // Está infectado
            isInfected = true;
        }

        // Se está infectado
        if (isInfected == true)
        {
            // Contagem regressiva para morrer
            CountDown();
            Transformation();
        }       
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
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a tag é Goop
        if (collision.gameObject.tag == "Goop")
        {
            // Diz que está infectado
            isInfected = true;
            // Seta a tag para Enemy
            gameObject.tag = "Enemy";
            // Transforma em infectado
            Transformation();
            //Inicializa o timer de infectado
            infectedTime = startInfectedTime;
        }
    }

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
    }
}
