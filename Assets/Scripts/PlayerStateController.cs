using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public static Action<States, States> OnStateChanged;

    private PlayerController playerController;

    public enum States
    {
        Idle,
        Moving,
    }

    public States currentState;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        currentState = States.Idle;
    }
    private void Update()
    {
        SetPlayerState();
    }

    private void SetPlayerState()
    {
        var isMoving = playerController.IsMoving();

        if (isMoving)
            ChangeState(States.Moving);
        else
            ChangeState(States.Idle);
    }

    public void ChangeState(States nextState)
    {
        if (currentState == nextState)
            return;

        currentState = nextState;
        OnStateChanged?.Invoke(currentState, nextState);
    }
}
