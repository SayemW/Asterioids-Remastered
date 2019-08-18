using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Thrusts the ship in the direction it is facing
/// </summary>
public class Ship : MonoBehaviour
{
    // Bullet
    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    GameObject effect;

    // The Body
    Rigidbody2D shipRigidBody2D;

    // Thrust properties
    Vector2 thrustDirection;
    const int ThrustForce = 5;

    // Rotate
    const int RotateDegreesPerSecond = 200;

    // Health
    int health;

    // Camera
    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        thrustDirection = new Vector2(1, 0);
        health = 30;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float rotateInput = Input.GetAxis("Rotate");
        if (rotateInput != 0)
        {
            // Rotate ship
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime * ( 1f / Time.timeScale);
            if (Input.GetAxis("Rotate") < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // New thrust angle
            // Rotation around z axis
            float angle = transform.eulerAngles.z;
            // Convert to radians
            angle *= Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(angle);
            thrustDirection.y = Mathf.Sin(angle);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Create bullet
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullets>().applyForce(thrustDirection * (1f / Time.timeScale), 1 * Time.timeScale);

            // Give audio
            AudioManager.Play(AudioClipName.PlayerShot);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            shipRigidBody2D.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 10;
        AudioManager.Play(AudioClipName.AsteroidHit);
        if (health < 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            transform.localScale = new Vector3(0, 0, 0);

            // Give audio
            AudioManager.Play(AudioClipName.PlayerDeath);

            StartCoroutine(ExecuteAfterTime());
        }
        camera.GetComponent<StressReceiver>().InduceStress(0.5f);
    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(2);

        // Code to execute after the delay
        SceneManager.LoadScene("menu");

        Destroy(gameObject);
    }
}
