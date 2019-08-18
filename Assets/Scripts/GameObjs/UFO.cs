using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows and shoots the player
/// </summary>
public class UFO : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject effect;

    float targetDistance, speed;
    
    // Set range where the ufo will stop and keep shooting
    float minDistance, maxDistance;

    // Player GameObject
    GameObject player;

    // Timer for bullets
    Timer timer;
    const int timeToShoot = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Get player
        player = GameObject.FindGameObjectWithTag("Ship");

        // Initialze UFO 
        speed = Random.Range(2f, 4f);
        maxDistance = Random.Range(3f, 4f);
        minDistance = maxDistance - 0.5f;

        // Timer 
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = timeToShoot;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Make UFO shoot
            if (timer.Finished)
            {
                GameObject bullet = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);

                // UFO bullet layer
                bullet.layer = 12;
                bullet.tag = "UFOBullet";
                bullet.GetComponent<Bullets>().applyForce((player.transform.position - transform.position).normalized * 0.75f, 1.5f);
                timer.Run();
            }
            // Make the UFO move
            targetDistance = Vector3.Distance(transform.position, player.transform.position);
            if (targetDistance > maxDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else if (targetDistance < minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        AudioManager.Play(AudioClipName.PlayerDeath);
        Destroy(gameObject);
    }
}
