using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the asteroid behaviour
/// </summary>

public class Asteroid : MonoBehaviour
{
    Sprite[] asteroidSprites = new Sprite[3];

    [SerializeField]
    Sprite sprite0;

    [SerializeField]
    Sprite sprite1;

    [SerializeField]
    Sprite sprite2;

    [SerializeField]
    Material mat0;

    [SerializeField]
    Material mat1;

    [SerializeField]
    Material mat2;

    [SerializeField]
    GameObject effect0;

    [SerializeField]
    GameObject effect1;

    [SerializeField]
    GameObject effect2;

    // Asteroid force
    Rigidbody2D asteroidRigidBody;
    const float MinImpulseForce = 10f;
    const float MaxImpulseForce = 20f;
    float angle, impulseForce;

    // Material
    Material[] mats = new Material[3];

    // Effects
    GameObject[] effects = new GameObject[3];
    GameObject effect;
    // Health
    int health;

    // Start is called before the first frame update
    void Start()
    {
        // Get health
        health = transform.localScale.x == 2 ? 30 : 10;

        // Populate array
        asteroidSprites[0] = sprite0;
        asteroidSprites[1] = sprite1;
        asteroidSprites[2] = sprite2;

        mats[0] = mat0;
        mats[1] = mat1;
        mats[2] = mat2;

        effects[0] = effect0;
        effects[1] = effect1;
        effects[2] = effect2;

        // Set random sprite
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int idx = Random.Range(0, 3);
        spriteRenderer.sprite = asteroidSprites[idx];
        spriteRenderer.material = mats[idx];
        effect = effects[idx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initialize the Asteroid direction

    public void Initialize(Direction direction, Vector3 position)
    {
        // Set position
        transform.position = position;

        // Give the asteroid a starting velocity
        angle = Random.Range(0, Mathf.PI / 6);
        switch (direction)
        {
            case Direction.Right:
                angle = angle - 15 * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                angle = angle + 75 * Mathf.Deg2Rad;
                break;
            case Direction.Left:
                angle = angle + 165 * Mathf.Deg2Rad;
                break;
            default:
                angle = angle + 255 * Mathf.Deg2Rad;
                break;
        }
        startMoving(angle);
    }

    // Make asteroid move in a direction
    public void startMoving(float angle)
    {
        // Get rigidBody
        asteroidRigidBody = gameObject.GetComponent<Rigidbody2D>();
        
        // Make it move
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        impulseForce = Random.Range(MinImpulseForce, MaxImpulseForce);
        asteroidRigidBody.AddForce(moveDirection * impulseForce, ForceMode2D.Impulse);
    }

    // When asteroid collides with a bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 10;
        if (health <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            // Check if the asteroid is has already been split once
            if (transform.localScale.x <= 1)
            {
                Destroy(gameObject);
            }
            else
            {
                // Change scale of the gameobject
                Vector3 scale = gameObject.transform.localScale;
                scale = scale / 2;
                gameObject.transform.localScale = scale;

                // Change collider radius
                gameObject.GetComponent<CircleCollider2D>().radius /= 2;

                // Instantiate new gameObject
                GameObject newAsteroid = Instantiate(gameObject);
                newAsteroid.GetComponent<Asteroid>().startMoving(Random.Range(0, Mathf.PI / 2));

                newAsteroid = Instantiate(gameObject);
                newAsteroid.GetComponent<Asteroid>().startMoving(Random.Range(0, Mathf.PI / 2));

                AudioManager.Play(AudioClipName.PlayerDeath);

                Destroy(gameObject);
            }
        }
      
        // Give audio
        AudioManager.Play(AudioClipName.AsteroidHit);
    }
}
