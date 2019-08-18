using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the bullets
/// </summary>
public class Bullets : MonoBehaviour
{
    GameObject Hud;

    Timer timer;
    const float Magnitude = 10f;

    void Start()
    {
        Hud = GameObject.FindGameObjectWithTag("HUD");
    }
    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Applys force to the bullets
    /// </summary>
    /// <param name="direction"></param>
    public void applyForce(Vector2 direction, float duration)
    {
        // Set timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = duration;
        timer.Run();

        // Apply force to bullet
        GetComponent<Rigidbody2D>().AddForce(Magnitude * direction, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Bullet")
        {
            Hud.GetComponent<HUD>().addScore();
        }
        Destroy(gameObject);
    }
}
