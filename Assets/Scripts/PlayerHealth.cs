using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDeath;

    public void Death()
    {
        OnDeath?.Invoke();
    }


}
