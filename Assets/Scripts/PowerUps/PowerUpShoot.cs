using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines the behaviour of the shoot power up
/// Makes the ship shoot additional bullets
/// </summary>
public class PowerUpShoot : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

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

    // Direction to shoot additional bullets at
    float angle;
    Vector3 position;
    Vector2 thrustDirection;

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
        // Ship position
        if (ship != null)
        {
            position = ship.transform.position;
        }

        // Check if we reached the powerUp
        if (!consumed && Vector3.Distance(position, transform.position) < 1)
        {
            // We reached the timer before it dissapeared
            timer.Stop();
            consumed = true;

            // Start timer and enable ability
            timer.Duration = powerUpDuration;
            timer.Run();

            // Make the gameObject size 0
            gameObject.transform.localScale = ZeroScale;
        }
        // We need to destroy the object
        if (timer.Finished)
        {
            Destroy(gameObject);
        }

        if (consumed && Input.GetKeyDown(KeyCode.Space) && ship != null)
        {
            // Find ship attributes
            angle = ship.transform.eulerAngles.z;
            angle = angle * Mathf.Deg2Rad;

            // Create two bullets
            thrustDirection.x = Mathf.Cos(angle + Mathf.PI / 6);
            thrustDirection.y = Mathf.Sin(angle + Mathf.PI / 6);

            GameObject bullet = Instantiate(prefabBullet, position, Quaternion.identity);
            bullet.GetComponent<Bullets>().applyForce(thrustDirection, 1);

            thrustDirection.x = Mathf.Cos(angle - Mathf.PI / 6);
            thrustDirection.y = Mathf.Sin(angle - Mathf.PI / 6);

            bullet = Instantiate(prefabBullet, position, Quaternion.identity);
            bullet.GetComponent<Bullets>().applyForce(thrustDirection, 1);
        }
    }
}
