using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoremanager : MonoBehaviour {

    public TMP_Text scoreTxt;
    public TMP_Text highScoreTxt;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSec;

    public bool scoreIncreasing;
    
    // Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            scoreCount += pointsPerSec * Time.deltaTime;
        }

        if(scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }
        scoreTxt.text = "Score: " + Mathf.Round(scoreCount);
        highScoreTxt.text = "High Score: " + Mathf.Round(highScoreCount);

	}

    public void AddScore(int addPoints)
    {
        scoreCount += addPoints;
    }
}
