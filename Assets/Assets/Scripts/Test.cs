using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    Text hello;
	// Use this for initialization
	void Start () {
        hello = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        hello.text = Input.acceleration.ToString() ;
	}
}