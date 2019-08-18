using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject UFOPrefab;

    Vector3 left, right, up, down;

    float leftScr, rightScr, upScr, downScr;

    int spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 1;

        // Save screen positions
        leftScr = ScreenUtils.ScreenLeft;
        rightScr = ScreenUtils.ScreenRight;
        upScr = ScreenUtils.ScreenTop;
        downScr = ScreenUtils.ScreenBottom;

        // Save spawn position
        left = new Vector3(ScreenUtils.ScreenLeft - 1, 0, 0);
        right = new Vector3(ScreenUtils.ScreenRight + 1, 0, 0);
        up = new Vector3(0, ScreenUtils.ScreenTop + 1, 0);
        down = new Vector3(0, ScreenUtils.ScreenBottom - 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("UFO").Length < spawnRate)
        {
            // Spawn UFOs from random sides at random positions
            switch (Random.Range(0, 4))
            {
                case 0:
                    down.x = Random.Range(leftScr, rightScr);
                    Instantiate(UFOPrefab, down, Quaternion.identity);
                    //asteroidOne.GetComponent<Asteroid>().Initialize(Direction.Up, down);
                    break;
                case 1:
                    up.x = Random.Range(leftScr, rightScr);
                    Instantiate(UFOPrefab, up, Quaternion.identity);
                    //asteroidTwo.GetComponent<Asteroid>().Initialize(Direction.Down, up);
                    break;
                case 2:
                    right.y = Random.Range(downScr, upScr);
                    Instantiate(UFOPrefab, right, Quaternion.identity);
                    //asteroidThree.GetComponent<Asteroid>().Initialize(Direction.Left, right);
                    break;
                default:
                    left.y = Random.Range(downScr, upScr);
                    Instantiate(UFOPrefab, left, Quaternion.identity);
                    //asteroidFour.GetComponent<Asteroid>().Initialize(Direction.Right, left);
                    break;
            }
        }
    }

    public void increase()
    {
        spawnRate++;
    }
}
