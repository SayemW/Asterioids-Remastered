using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns powerUps after a fixed time
/// Removes powerUps if not claimed in fixed time
/// </summary>
public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject timePrefab;

    [SerializeField]
    GameObject shiedPrefab;

    [SerializeField]
    GameObject shootPrefab;

    // Timer
    Timer spawnTimer;

    const int timeToSpawn = 15;

    // PowerUp we instantiate
    GameObject[] powerUps;

    // Start is called before the first frame update
    void Start()
    {
        // PowerUps in array
        powerUps = new GameObject[3];
        powerUps[0] = timePrefab;
        powerUps[1] = shiedPrefab;
        powerUps[2] = shootPrefab;

        // Set timers and start
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = timeToSpawn;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Finished)
        {
            // Random spot to spawn powerUp
            Vector3 location = new Vector3(Random.Range(ScreenUtils.ScreenLeft + 1, ScreenUtils.ScreenRight - 1),
                Random.Range(ScreenUtils.ScreenBottom + 1, ScreenUtils.ScreenTop - 1), 0);

            // Spawn random powerUp
            GameObject.Instantiate<GameObject>(powerUps[Random.Range(0, 3)], location, Quaternion.identity);
            spawnTimer.Run();
        }
    }
}
