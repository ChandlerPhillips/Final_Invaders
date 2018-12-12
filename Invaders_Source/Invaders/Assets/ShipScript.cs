using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float Speed = 0.25f;
    public GameObject Ship_Explosion;

    const float START_POSITION_X = 0f;
    const float START_POSITION_Y = -10.9f;

    public float LEFT_BOUNDARY = -24.5f;
    public float RIGHT_BOUNDARY = 23.5f;
	// Use this for initialization
	void Start ()
    {
		
	}

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Ship_Explosion);

        explosion.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameManagerScript.Game_State == GameState.MAIN_GAME)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponents<Collider2D>()[0].enabled = true;
            gameObject.GetComponents<Collider2D>()[1].enabled = true;
            HandleUserInput();
        }
	}

    private void HandleUserInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > LEFT_BOUNDARY)
        {
            transform.position = new Vector3(transform.position.x - Speed, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < RIGHT_BOUNDARY)
        {
            transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
        }
    }// End of HandleUserInput().

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Alien")
        {
            ScoreScript.DecreaseLives();
            PlayExplosion();
            StartCoroutine(WaitMethod());
        }
    }

    private IEnumerator WaitMethod()
    {
        GameManagerScript.Game_State = GameState.WAIT;
        gameObject.GetComponents<Collider2D>()[0].enabled = false;
        gameObject.GetComponents<Collider2D>()[1].enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(5);
        transform.position = new Vector3(START_POSITION_X, START_POSITION_Y);

        if (ScoreScript.CurrentLives != 0)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            GameManagerScript.Game_State = GameState.MAIN_GAME;
            gameObject.GetComponents<Collider2D>()[0].enabled = true;
            gameObject.GetComponents<Collider2D>()[1].enabled = true;
        }
        else
        {
            GameManagerScript.Game_State = GameState.GAME_OVER;
        }
    }
}
