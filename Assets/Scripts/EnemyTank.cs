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
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;
    [SerializeField] private Transform firePosition;
    [SerializeField] private Color bulletColor;
    [SerializeField] private Color bulletTrailColor;

    private float wanderTimer;
    private float attackTimer;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackTimer = GetRandomAttackTime();
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
        bullet.GetComponent<Bullet>().InitializeBullet(bulletColor, bulletTrailColor, bulletSpeed, bulletDamage);
    }

    private float GetRandomAttackTime()
    {
        var randomTime = UnityEngine.Random.Range(minAttackTime, maxAttackTime);
        return randomTime;
    }

}
