using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float movementSpeed;
    void Update()
    {
        Movement(movementSpeed);
    }

    public void Movement(float speed)
    {
        var direction = transform.forward;
        movementSpeed = speed;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }
}
