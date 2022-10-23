using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CriminalState { Uncaught, Caught }

public class Criminal : MonoBehaviour
{
    public static event Action<Criminal> OnPickup = delegate { };
    public Handcuff CriminalHandcuff { get { return criminalHandcuff; } set { criminalHandcuff = value; } }

    public Transform objectToFollow;

    [SerializeField] float followSpeed = .5f;
    [SerializeField] float followDistance = 2f;

    private CriminalState state = CriminalState.Uncaught;
    private Handcuff criminalHandcuff;

    public void ChangeState(CriminalState newState)
    {
        state = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerHandcuffController>(out PlayerHandcuffController playerInventory);

        if (playerInventory != null && state == CriminalState.Uncaught)
        {
            OnPickup(this);
        }
    }

    void Follow()
    {
        if (Vector3.Distance(transform.position, objectToFollow.position) > followDistance)
            transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position, followSpeed);
    }


    private void Update()
    {
        if (objectToFollow != null)
            Follow();
    }
}
