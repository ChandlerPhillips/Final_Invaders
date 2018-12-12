using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    const int EXTRA_LIFE_OFFSET = 100;

    public static uint ScoreValue = 0;
    public static uint CurrentLives = 3;
    private static uint UpdatedLives;
    TextMesh Score;

	void Start ()
    {
        Score = GetComponent<TextMesh>();
        UpdatedLives = CurrentLives;
        UpdateLives();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Score.text = "SCORE:" + ScoreValue;

        if (CurrentLives != UpdatedLives)
        {
            CurrentLives = UpdatedLives;
            UpdateLives();
        }
	}

    private void UpdateLives()
    {
        if (CurrentLives == 0)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = false;
        }
        else if (CurrentLives == 1)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = false;
        }
        else if (CurrentLives == 2)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = false;
        }
        else if (CurrentLives == 3)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = false;
        }
        else if (CurrentLives == 4)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = false;
        }
        else if (CurrentLives == 5)
        {
            GameObject.Find("Life_1").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_2").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_3").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_4").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Life_5").GetComponent<Renderer>().enabled = true;
        }
    }

    public static void IncreaseLives()
    {
        if (UpdatedLives < 5 && CurrentLives < 5)
        {
            UpdatedLives++;
        }
    }

    public static void DecreaseLives()
    {
        if (UpdatedLives != 0 && CurrentLives != 0)
        {
            UpdatedLives--;
        }
    }

    public static void IncreaseScore()
    {
        ScoreValue++;

        if (ScoreValue % EXTRA_LIFE_OFFSET == 0 && CurrentLives != 5)
        {
            IncreaseLives();
        }
        else if (ScoreValue % EXTRA_LIFE_OFFSET == 0 && CurrentLives == 5)
        {
            ScoreValue += EXTRA_LIFE_OFFSET;
        }
    }

    public static void ResetScoreAndLives()
    {
        ScoreValue = 0;
        IncreaseLives();
        IncreaseLives();
        IncreaseLives();
    }
}
