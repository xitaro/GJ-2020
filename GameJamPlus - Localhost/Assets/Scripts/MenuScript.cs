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

    // Start is called before the first frame update
    void Start()
    {
        menuAnimator = gameObject.GetComponent<Animator>();
        BtnReturn.SetActive(false);
    }

    public void Play()
    {    
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
    }

    public void Return()
    {
        if (menuAnimator.GetBool("IsSettings") == true)
        {
            menuAnimator.SetBool("IsSettings", false);          
            BtnReturn.SetActive(false);
            BtnOpcoes.SetActive(true);
            Title.SetActive(true);
            BtnPlay.SetActive(true);
            BtnAbout.SetActive(true);
        }
        else
        {
            menuAnimator.SetBool("IsAbout", false);         
            BtnReturn.SetActive(false);
            BtnAbout.SetActive(true);
            Title.SetActive(true);
            BtnPlay.SetActive(true);
            BtnAbout.SetActive(true);
            BtnOpcoes.SetActive(true);
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
}
  


