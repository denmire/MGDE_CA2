using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public GameObject BtnPanel;
    public GameObject soundsetting;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void RuleBtnClick()
    {
        SceneManager.LoadScene("Rules");
    }

    public void PlayBtnClick()
    {
        SceneManager.LoadScene("Endless runner");
    }

    public void BackbtnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MenuOptionsBtnClick()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitBtnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    
}
