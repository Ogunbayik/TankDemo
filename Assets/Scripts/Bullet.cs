using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject shellVisual;

    private TrailRenderer trailRenderer;

    private Color bulletColor;
    private Color trailColor;
    private float movementSpeed;
    private int bulletDamage;
    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        InitializeBullet(bulletColor, trailColor, movementSpeed, bulletDamage);
    }
    void Update()
    {
        Movement();
    }

    public void InitializeBullet(Color color, Color t_Color, float speed, int damage)
    {
        bulletColor = color;
        movementSpeed = speed;
        trailColor = t_Color;
        bulletDamage = damage;

        shellVisual.GetComponent<MeshRenderer>().material.color = bulletColor;
        Material trailMaterial = trailRenderer.material;
        trailMaterial.color = t_Color;
        trailMaterial.SetColor("_Color", t_Color);

    }

    public void Movement()
    {
        var direction = transform.forward;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(this.gameObject);
            Debug.Log("Collision");
        }
        else if(collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            player.Death();
        }
    }


}
