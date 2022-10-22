using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CriminalState { Uncaught, Caught }

public class Criminal : MonoBehaviour
{
    public static event Action<Criminal> OnPickup = delegate { };
    
    private CriminalState state = CriminalState.Uncaught;

    public void ChangeState(CriminalState newState)
    {
        state = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory);

        if (playerInventory != null && state == CriminalState.Uncaught)
        {
            OnPickup(this);
        }
    }
}
