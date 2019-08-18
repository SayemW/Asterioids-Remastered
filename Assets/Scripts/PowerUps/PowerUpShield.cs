using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField]
    GameObject shieldPrefab;

    // Dissapear or power lasting period
    Timer timer;

    const int timeToDissapear = 7;
    const int powerUpDuration = 7;

    // Scale to 0
    Vector3 ZeroScale = new Vector3(0, 0, 0);

    // Check if we consumed it or not
    bool consumed;

    // Ship
    GameObject ship;

    // GameObject Shield
    GameObject shield;

    // Position of shield
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        // Get ship
        ship = GameObject.FindGameObjectWithTag("Ship");

        // Set timers
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = timeToDissapear;
        consumed = false;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        // Position of shield & ship
        if (ship != null)
        {
            position = ship.transform.position;
        }

        // Check if powerup has been taken
        if (!consumed && Vector3.Distance(position, transform.position) < 1)
        {
            // We reached the timer before it dissapeared
            timer.Stop();
            consumed = true;

            // Start timer and enable ability
            timer.Duration = powerUpDuration;
            timer.Run();

            // Make the gameObject  size 0
            gameObject.transform.localScale = ZeroScale;
        }

        // Remove gameObjects
        if (timer.Finished)
        {
            Destroy(gameObject);
            Destroy(shield);
        }

        // Make a shield
        if (consumed && shield == null)
        {
            shield = Instantiate(shieldPrefab, position, Quaternion.identity);
        }

        // Update Shield location
        if (shield != null)
        {
            shield.transform.position = position;
        }
    }
}
