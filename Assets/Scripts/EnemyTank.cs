using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyTank : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")]
    [SerializeField] private float wanderRadius;
    [SerializeField] private float maxWanderTimer;
    [Header("Attack Settings")]
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;
    [Header("Color Settings")]
    [SerializeField] private Color[] bodyColors;
    [SerializeField] private Color[] bulletColors;
    [SerializeField] private Color[] bulletTrailColors;

    private float wanderTimer;
    private float attackTimer;

    private Color bulletColor;
    private Color bulletTrailColor;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackTimer = GetRandomAttackTime();

    }
    private void Start()
    {
        SetRandomTankColor();
    }
    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= maxWanderTimer)
        {
            //Enemy is moving random position

            Vector3 newPosition;
            if (GetRandomPosition(transform.position, wanderRadius, out newPosition))
            {
                navMeshAgent.SetDestination(newPosition);
            }
            wanderTimer = 0;
        }
    }

    private bool GetRandomPosition(Vector3 center, float moveRange, out Vector3 result)
    {
        var randomPoint = center + UnityEngine.Random.insideUnitSphere * moveRange;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint,out hit, moveRange, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void HandleAttack()
    {
        if(attackTimer <= 0)
        {
            CreateBullet();
            attackTimer = GetRandomAttackTime();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }    
    }
    private void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
        bullet.GetComponent<Bullet>().InitializeBullet(bulletColor, bulletTrailColor, bulletSpeed, bulletDamage, false);
    }

    private float GetRandomAttackTime()
    {
        var randomTime = UnityEngine.Random.Range(minAttackTime, maxAttackTime);
        return randomTime;
    }

    private void SetRandomTankColor()
    {
        MeshRenderer[] allMeshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        var randomIndex = UnityEngine.Random.Range(0, bodyColors.Length);

        for (int i = 0; i < allMeshRenderers.Length; i++)
        {
            allMeshRenderers[i].material.color = bodyColors[randomIndex];
            bulletColor = bulletColors[randomIndex];
            bulletTrailColor = bulletTrailColors[randomIndex];
        }
    }

}
