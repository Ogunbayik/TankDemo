using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [Header("General Settings")]
    [SerializeField] private GameObject tankVisual;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float tankRotationSpeed;
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxReloadTime;
    [SerializeField] private int bulletDamage;
    [Header("Color Settings")]
    [SerializeField] private Color bulletColor;
    [SerializeField] private Color bulletTrailColor;
    
    private float horizontalInput;
    private float verticalInput;
    private float reloadTimer = 0;

    private Vector3 movementDirection;

    private bool isMoving = false;
    private bool canAttack = true;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (canAttack)
            HandleAttack();
        else
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
            {
                canAttack = true;
                reloadTimer = 0;
            }
        }

        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(Consts.PlayerInput.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(Consts.PlayerInput.VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        playerRb.velocity = movementDirection * movementSpeed;

        if (movementDirection.sqrMagnitude != 0)
            isMoving = true;
        else
            isMoving = false;
    }

    private void HandleRotation()
    {
        if (isMoving)
        {
            var desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            tankVisual.transform.rotation = Quaternion.RotateTowards(tankVisual.transform.rotation, desiredRotation, tankRotationSpeed * Time.deltaTime);
        }
    }
    private void HandleAttack()
    {
        var pressedAttackButton = Input.GetKeyDown(KeyCode.Space);

        if(pressedAttackButton)
        {
            CreateBullet();
            reloadTimer = maxReloadTime;
            canAttack = false;
        }
    }

    private void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, attackPosition.transform.position, attackPosition.transform.rotation);
        bullet.GetComponent<Bullet>().InitializeBullet(bulletColor, bulletTrailColor, bulletSpeed, bulletDamage, true);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
