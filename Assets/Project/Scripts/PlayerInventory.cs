using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfHandcuffs { get { return handcuffs.Count; } }

    [SerializeField] private Transform handcuffContainer;
    [SerializeField] private Vector3 afterpickupPos;
    [SerializeField] private float pickupSpeed = .1f;
    [SerializeField] private float spacing = .5f;

    [SerializeField] Vector3 criminalHandcuffPos;

    private List<Handcuff> handcuffs = new List<Handcuff>();

    private void OnEnable()
    {
        Handcuff.OnPickup += PickupHandcuff;
        CriminalHandler.OnHandcuffCriminal += HandcuffCriminal;
        CriminalHandler.OnArrestCriminal += PickupHandcuff;
    }

    private void OnDisable()
    {
        Handcuff.OnPickup -= PickupHandcuff;
        CriminalHandler.OnHandcuffCriminal -= HandcuffCriminal;
        CriminalHandler.OnArrestCriminal -= PickupHandcuff;
    }

    public void PickupHandcuff(Handcuff handcuff)
    {
        handcuff.ChangeState(HandcuffState.InBackpack);
        handcuffs.Add(handcuff);
        handcuff.transform.parent = handcuffContainer;

        StartCoroutine(PlayPickupAnimation(handcuff, new Vector3(0, handcuffs.Count * spacing, 0)));
    }

    private void HandcuffCriminal(Criminal criminal)
    {
        Handcuff lastHandcuff = handcuffs[handcuffs.Count - 1];
        handcuffs.Remove(lastHandcuff);
        criminal.CriminalHandcuff = lastHandcuff;
        lastHandcuff.transform.parent = criminal.transform;

        StartCoroutine(PlayPickupAnimation(lastHandcuff, criminalHandcuffPos));
    }

    IEnumerator PlayPickupAnimation(Handcuff handcuff, Vector3 newPosition)
    {
        while (handcuff.transform.localPosition != newPosition)
        {
            handcuff.transform.localPosition = Vector3.Lerp(handcuff.transform.localPosition, newPosition, pickupSpeed);
            handcuff.transform.localRotation = Quaternion.Lerp(handcuff.transform.localRotation, Quaternion.identity, pickupSpeed);

            yield return null;
        }
    }
}
