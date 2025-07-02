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

    private bool isPlayerBullet;
    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        InitializeBullet(bulletColor, trailColor, movementSpeed, bulletDamage, isPlayerBullet);
    }
    void Update()
    {
        Movement();
    }

    public void InitializeBullet(Color color, Color t_Color, float speed, int damage, bool isPlayer)
    {
        bulletColor = color;
        movementSpeed = speed;
        trailColor = t_Color;
        bulletDamage = damage;
        isPlayerBullet = isPlayer;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            if (!isPlayerBullet)
                return;

            enemy.TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            if (isPlayerBullet)
                return;

            player.Death();
        }
    }

}
