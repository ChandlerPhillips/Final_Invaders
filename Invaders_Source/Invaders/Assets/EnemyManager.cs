using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private const float LEFT_BOUNDARY = -24f;
    private const float RIGHT_BOUNDARY = 24f;
    private const float YSTART = 17.5f;

    public GameObject GreenAlien;
    public GameObject BlueAlien;
    public GameObject PinkAlien;
    public GameObject YellowAlien;
    public float spawnRate = 0.75f;

    private float randX;
    private Vector2 spawnLocation;
    private float nextSpawnTime = 0.0f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerScript.Game_State == GameState.MAIN_GAME)
        {
            SpawnAlien();
        }
	}

    private void SpawnAlien()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            randX = Random.Range(LEFT_BOUNDARY, RIGHT_BOUNDARY);
            spawnLocation = new Vector2(randX, YSTART);

            GameObject enemy;
            int enemySelection = Random.Range(1, 5);

            if (enemySelection == 1)
            {
                enemy = GreenAlien;
            }
            else if (enemySelection == 2)
            {
                enemy = BlueAlien;
            }
            else if (enemySelection == 3)
            {
                enemy = PinkAlien;
            }
            else
            {
                enemy = YellowAlien;
            }

            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
}
