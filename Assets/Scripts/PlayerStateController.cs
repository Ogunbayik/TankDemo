using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public static Action<States, States> OnStateChanged;

    public enum States
    {
        Idle,
        Moving,
        Attacking,
    }

    public States currentState;
    void Start()
    {
        currentState = States.Idle;
    }

    public void ChangeState(States nextState)
    {
        if (currentState == nextState)
            return;

        currentState = nextState;
        OnStateChanged?.Invoke(currentState, nextState);
    }
}
