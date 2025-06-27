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
    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        InitializeBullet(bulletColor, trailColor, movementSpeed);
    }
    void Update()
    {
        Movement();
    }

    public void InitializeBullet(Color color, Color t_Color, float speed)
    {
        bulletColor = color;
        movementSpeed = speed;
        trailColor = t_Color;

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
}
