using UnityEngine;

public enum HandcuffState{OnGround, InBackpack, OnPrisoner}

public class Handcuff : MonoBehaviour
{
    private HandcuffState state = HandcuffState.OnGround;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory);

        if (playerInventory != null  && state == HandcuffState.OnGround)
        {
            playerInventory.AddToInventory(this);
            state = HandcuffState.InBackpack;
        }
    }
}
