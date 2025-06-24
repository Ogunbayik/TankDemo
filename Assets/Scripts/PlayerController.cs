using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateController stateController;

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

    private float horizontalInput;
    private float verticalInput;
    private float reloadTimer = 0;

    private Vector3 movementDirection;

    private bool isMoving = false;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        stateController = GetComponent<PlayerStateController>();
    }
    void Update()
    {
        HandleMovement();
        SetPlayerState();
        HandleRotation();
    }

    private void SetPlayerState()
    {
        if (isMoving)
            stateController.ChangeState(PlayerStateController.States.Moving);
        else
            stateController.ChangeState(PlayerStateController.States.Idle);
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

    private void CreateBullet(Vector3 spawnPosition, float speed)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawnPosition;
        bullet.transform.rotation = tankVisual.transform.rotation;
        bullet.GetComponent<PlayerBullet>().Movement(speed);
    }
}
