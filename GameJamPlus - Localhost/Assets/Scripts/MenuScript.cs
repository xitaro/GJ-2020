        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Animator menuAnimator;
    public GameObject Title;
    public GameObject BtnPlay;
    public GameObject BtnOpcoes;
    public GameObject BtnAbout;
    public GameObject BtnReturn;
    public GameObject SettingPanel;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Menu");
        menuAnimator = gameObject.GetComponent<Animator>();
        BtnReturn.SetActive(false);
        SettingPanel.SetActive(false);
    }

    public void Play()
    {
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Play("Gameplay");
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        menuAnimator.SetBool("IsSettings", true);
        BtnOpcoes.SetActive(false);
        Title.SetActive(false);
        BtnPlay.SetActive(false);
        BtnAbout.SetActive(false);
        BtnReturn.SetActive(true);
        SettingPanel.SetActive(true);
    }

    public void Return()
    {
        if (menuAnimator.GetBool("IsSettings") == true)
        {
    
            StartCoroutine(Waiter1());
         
        }
        else //isabout
        {
            StartCoroutine(Waiter2());

        }
    }

    public void About()
    {       
        menuAnimator.SetBool("IsAbout", true);
        BtnOpcoes.SetActive(false);
        Title.SetActive(false);
        BtnPlay.SetActive(false);
        BtnAbout.SetActive(false);
        BtnReturn.SetActive(true);

    }

    IEnumerator Waiter1()
    {
        BtnReturn.SetActive(false);
        SettingPanel.SetActive(false);
        menuAnimator.SetBool("IsSettings", false);

        yield return new WaitForSeconds(0.5f);  
        
        BtnOpcoes.SetActive(true);
        Title.SetActive(true);
        BtnPlay.SetActive(true);
        BtnAbout.SetActive(true);

    }

    IEnumerator Waiter2()
    {
        BtnReturn.SetActive(false);
        menuAnimator.SetBool("IsAbout", false);

        yield return new WaitForSeconds(0.5f);

        BtnAbout.SetActive(true);
        Title.SetActive(true);
        BtnPlay.SetActive(true);
        BtnAbout.SetActive(true);
        BtnOpcoes.SetActive(true);

    }
}
  


