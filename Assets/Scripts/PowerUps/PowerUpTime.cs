using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTime : MonoBehaviour
{
    // Dissapear or power lasting period
    Timer timer;

    const int timeToDissapear = 7;
    const float powerUpDuration = 0.5f;

    // Check if we consumed it or not
    bool consumed;
    bool slowedDown;
    // Ship 
    GameObject ship;

    // Scale to 0
    Vector3 ZeroScale = new Vector3(0, 0, 0);

    // Ship position
    Vector3 position;

    // Slow down 
    float slowDownFactor = 0.1f;
    float slowDownDuration = 0.5f;

    // Ship velocity
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        // Get ship
        ship = GameObject.FindGameObjectWithTag("Ship");

        // Havn't slowed down yet
        slowedDown = false;

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

        if (timer.Finished)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
        
        if (consumed && !slowedDown)
        {
            // Slow down time
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            slowedDown = true;
        }

        if (slowedDown)
        {
            // Slowly bring time back to normal
            //Time.timeScale += ((1f / slowDownDuration) * Time.unscaledDeltaTime);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }
}
