using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    private const float SOUND_EFFECT_UPPER_BOUND = 15.0f;
    private const float SOUND_EFFECT_LOWER_BOUND = 14.0f;
    private bool SoundEffectPlayed = false;
    public GameObject ExplosionGO;

    private const float OFF_BOTTOM_SCREEN_OFFSET = 8.0f;
    private const float LEFT_BOUNDARY = -24.7f;
    private const float RIGHT_BOUNDARY = 24.25f;
    private const float BOTTOM_BOUNDARY = -17.0f;
    public float Rate = 0.25f;

    private Vector3 GoalLocation;
    private AudioSource Descend;

    // Use this for initialization
    void Start()
    {
        // Determine ship color and set the OFF_SCREEN_OFFSET based on that.
        InitializeShipGoal();
        Descend = GetComponent<AudioSource>();
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Greens move at rate.
        if (this.name == "Green_Alien(Clone)")
        {
            transform.position = Vector3.MoveTowards(transform.position, GoalLocation, Rate);
        }
        // Blues move at rate + .10 rate.
        else if (this.name == "Blue_Alien(Clone)")
        {
            transform.position = Vector3.MoveTowards(transform.position, GoalLocation, Rate + .10f);
        }
        // Pinks move at - .10 rate.
        else if (this.name == "Pink_Alien(Clone)")
        {
            transform.position = Vector3.MoveTowards(transform.position, GoalLocation, Rate - 0.10f);
        }
        // Yellows move at +.15 rate.
        else if (this.name == "Yellow_Alien(Clone)")
        {
            transform.position = Vector3.MoveTowards(transform.position, GoalLocation, Rate + .15f);
        }

        if (!SoundEffectPlayed && transform.position.y < SOUND_EFFECT_UPPER_BOUND && transform.position.y > SOUND_EFFECT_LOWER_BOUND )
        {
            Descend.Play();
        }

        // Simply destroy the alien instance when it goes off screen.
        if (transform.position.y <= BOTTOM_BOUNDARY)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            PlayExplosion();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Missile")
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    private void InitializeShipGoal()
    {
        const float BLUE_OFFSET = 7.5f;
        const float PINK_OFFSET = 14.0f;
        Vector3 ShipLocation = GameObject.Find("Ship").transform.position;

        // Green Ship Movement.
        if (this.name == "Green_Alien(Clone)")
        {
            GoalLocation = new Vector3(ShipLocation.x, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
        }

        // Blue Ship Movement.
        else if (this.name == "Blue_Alien(Clone)")
        {
            if (ShipLocation.x + BLUE_OFFSET < RIGHT_BOUNDARY && ShipLocation.x - BLUE_OFFSET > LEFT_BOUNDARY)
            {
                int sideRoll = Random.Range(1, 3);

                // Target the right of the ship.
                if (sideRoll == 1)
                {
                    GoalLocation = new Vector3(ShipLocation.x + BLUE_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
                }
                // Target the left of the ship.
                else
                {
                    GoalLocation = new Vector3(ShipLocation.x - BLUE_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
                }
            }
            else
            {
                GoalLocation = new Vector3(ShipLocation.x, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
            }
        }

        // Pink Ship Movement.
        else if (this.name == "Pink_Alien(Clone)")
        {
            if (ShipLocation.x + PINK_OFFSET < RIGHT_BOUNDARY && ShipLocation.x - PINK_OFFSET > LEFT_BOUNDARY)
            {
                int sideRoll = Random.Range(1, 3);

                // Target the right of the ship.
                if (sideRoll == 1)
                {
                    GoalLocation = new Vector3(ShipLocation.x + PINK_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
                }
                // Target the left of the ship.
                else
                {
                    GoalLocation = new Vector3(ShipLocation.x - PINK_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
                }
            }
            else if (ShipLocation.x + PINK_OFFSET < RIGHT_BOUNDARY)
            {
                GoalLocation = new Vector3(ShipLocation.x + PINK_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
            }
            else if (ShipLocation.x - PINK_OFFSET > LEFT_BOUNDARY)
            {
                GoalLocation = new Vector3(ShipLocation.x - PINK_OFFSET, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
            }
        }

        // Yellow Ship Movement.
        else if (this.name == "Yellow_Alien(Clone)")
        {
            GoalLocation = new Vector3(ShipLocation.x, ShipLocation.y - OFF_BOTTOM_SCREEN_OFFSET);
        }
    }
}
