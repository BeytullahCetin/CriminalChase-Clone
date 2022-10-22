using System;
using UnityEngine;

public enum HandcuffState { OnGround, InBackpack, OnPrisoner }

public class Handcuff : MonoBehaviour
{
    public static event Action<Handcuff> OnPickup = delegate { };

    private HandcuffState state = HandcuffState.OnGround;

    public void ChangeState(HandcuffState newState)
    {
        state = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory);

        if (playerInventory != null && state == HandcuffState.OnGround)
        {
            OnPickup(this);
        }
    }
}
