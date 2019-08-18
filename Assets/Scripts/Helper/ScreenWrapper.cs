using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen Wrapping the gameObjects
/// </summary>
public class ScreenWrapper : MonoBehaviour
{
    // Collider radius
    float colliderRadius;

    // Start is called before the first frame update
    void Start()
    {
        //colliderRadius = GetComponent<CircleCollider2D>().radius;
    }
    void OnBecameInvisible()
    {
        Vector3 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x <= ScreenUtils.ScreenLeft ||
            position.x >= ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y >= ScreenUtils.ScreenTop ||
            position.y <= ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }
        transform.position = position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
