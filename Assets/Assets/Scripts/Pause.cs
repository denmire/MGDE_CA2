using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseBtnClick()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void PlayBtn2Click()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
}
