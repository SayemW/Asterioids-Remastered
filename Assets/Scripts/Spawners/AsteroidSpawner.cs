using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    float colliderRadius;
    Vector3 left, right, up, down;

    float leftScr, rightScr, upScr, downScr;

    GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        // HUD
        hud = GameObject.FindGameObjectWithTag("HUD");

        // Save screen positions
        leftScr = ScreenUtils.ScreenLeft;
        rightScr = ScreenUtils.ScreenRight;
        upScr = ScreenUtils.ScreenTop;
        downScr = ScreenUtils.ScreenBottom;

        // Instantiate an asteroid and save its colliderRadius
        GameObject asteroidTemp = Instantiate(prefabAsteroid, new Vector3(0, 0, 0), Quaternion.identity);
        colliderRadius = asteroidTemp.GetComponent<CircleCollider2D>().radius;

        // Destroy the asteroid
        Destroy(asteroidTemp);

        // Save locations to spawn the asteroids
        left = new Vector3(leftScr - colliderRadius, 0, 0);
        right = new Vector3(rightScr + colliderRadius, 0, 0);
        up = new Vector3(0, upScr + colliderRadius, 0);
        down = new Vector3(0, downScr - colliderRadius, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length < 3)
        {
            // Spawn Asteroids from random sides at random positions
            switch(Random.Range(0, 4))
            {
                case 0:
                    down.x = Random.Range(leftScr, rightScr);
                    GameObject asteroidOne = Instantiate(prefabAsteroid, down, Quaternion.identity);
                    asteroidOne.GetComponent<Asteroid>().Initialize(Direction.Up, down);
                    break;
                case 1:
                    up.x = Random.Range(leftScr, rightScr);
                    GameObject asteroidTwo = Instantiate(prefabAsteroid, up, Quaternion.identity);
                    asteroidTwo.GetComponent<Asteroid>().Initialize(Direction.Down, up);
                    break;
                case 2:
                    right.y = Random.Range(downScr, upScr);
                    GameObject asteroidThree = Instantiate(prefabAsteroid, right, Quaternion.identity);
                    asteroidThree.GetComponent<Asteroid>().Initialize(Direction.Left, right);
                    break;
                default:
                    left.y = Random.Range(downScr, upScr);
                    GameObject asteroidFour = Instantiate(prefabAsteroid, left, Quaternion.identity);
                    asteroidFour.GetComponent<Asteroid>().Initialize(Direction.Right, left);
                    break;
            }
        }
    }
}
