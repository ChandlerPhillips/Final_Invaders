using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public float RestingState = -7.75f;
    public float Speed = 0.50f;

    private bool Active = false;
    private const float UPPER_BOUNDS = 16f;

    private AudioSource shoot;
    private AudioSource explode;

	// Use this for initialization
	void Start ()
    {
        DisableMissileFunctionality();
        shoot = GetComponents<AudioSource>()[0];
        explode = GetComponents<AudioSource>()[1];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerScript.Game_State == GameState.MAIN_GAME)
        {
            HandleLauching();
        }
        else if (GameManagerScript.Game_State == GameState.GAME_OVER || GameManagerScript.Game_State == GameState.WAIT)
        {
            DisableMissileFunctionality();
        }
	}

    private void HandleLauching()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Active)
        {
            transform.position = new Vector3(GameObject.Find("Ship").transform.position.x, RestingState,
                GameObject.Find("Ship").transform.position.z);
            EnableMissileFunctionality();
            shoot.Play();
        }

        // Update position using speed.
        if (Active)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Speed, transform.position.z);
        }

        if (transform.position.y > UPPER_BOUNDS)
        {
            DisableMissileFunctionality();
            transform.position = new Vector3(transform.position.x, RestingState, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Alien")
        {
            DisableMissileFunctionality();
            shoot.Stop();
            explode.Play();
            ScoreScript.IncreaseScore();
        }
    }

    private void DisableMissileFunctionality()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Active = false;
    }

    private void EnableMissileFunctionality()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Active = true;
    }
}
