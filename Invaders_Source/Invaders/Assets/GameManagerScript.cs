using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    START_SCREEN,
    WAIT,
    MAIN_GAME,
    GAME_OVER
}

public class GameManagerScript : MonoBehaviour {

    public static GameState Game_State;

    const float STROBE_TIME = 0.75f;
    private float LastStrobe = 0.0f;



	// Use this for initialization
	void Start ()
    {
        Game_State = GameState.START_SCREEN;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Game_State == GameState.START_SCREEN)
        {
            StrobeStartText();

            if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject.Find("Title").GetComponent<Renderer>().enabled = false;
                GameObject.Find("Press_to_Fire").GetComponent<Renderer>().enabled = false;
                GameObject.Find("Press_to_Move").GetComponent<Renderer>().enabled = false;
                GameObject.Find("Names").GetComponent<Renderer>().enabled = false;
                GameObject.Find("Press_to_Start").GetComponent<Renderer>().enabled = false;
                Game_State = GameState.MAIN_GAME;
            }
        }

        if (Game_State == GameState.MAIN_GAME)
        {
            if (ScoreScript.CurrentLives == 0)
            {
                Game_State = GameState.GAME_OVER;
            }
        }

        if (Game_State == GameState.GAME_OVER)
        {
            GameObject.Find("Game_Over").GetComponent<Renderer>().enabled = true;

            StrobeRestartText();

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Find("Game_Over").GetComponent<Renderer>().enabled = false;
                GameObject.Find("Press_to_Replay").GetComponent<Renderer>().enabled = false;
                ScoreScript.ResetScoreAndLives();
                Game_State = GameState.MAIN_GAME;
            }
        }

    }

    private void StrobeStartText()
    {
        if (Time.time > LastStrobe)
        {
            LastStrobe = Time.time + STROBE_TIME;

            bool enabledState = !GameObject.Find("Press_to_Start").GetComponent<Renderer>().enabled;
            GameObject.Find("Press_to_Start").GetComponent<Renderer>().enabled = enabledState;
        }
    }

    private void StrobeRestartText()
    {
        if (Time.time > LastStrobe)
        {
            LastStrobe = Time.time + STROBE_TIME;

            bool enabledState = !GameObject.Find("Press_to_Replay").GetComponent<Renderer>().enabled;
            GameObject.Find("Press_to_Replay").GetComponent<Renderer>().enabled = enabledState;
        }
    }
}
