using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

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
}
