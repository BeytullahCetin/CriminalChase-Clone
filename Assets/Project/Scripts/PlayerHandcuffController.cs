using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandcuffController : MonoBehaviour
{
    public int NumberOfHandcuffs { get { return handcuffs.Count; } }

    [SerializeField] private Transform handcuffContainer;
    [SerializeField] private Vector3 criminalHandcuffPos;
    [SerializeField] private Vector3 criminalHandcuffRot;
    [SerializeField] private Vector3 afterpickupPos;
    [SerializeField] private float pickupSpeed = .1f;
    [SerializeField] private float spacing = .5f;

    private List<Handcuff> handcuffs = new List<Handcuff>();

    private void OnEnable()
    {
        Handcuff.OnPickup += PickupHandcuff;
        PlayerCriminalController.OnHandcuffCriminal += HandcuffCriminal;
        PlayerCriminalController.OnArrestCriminal += PickupHandcuff;
    }

    private void OnDisable()
    {
        Handcuff.OnPickup -= PickupHandcuff;
        PlayerCriminalController.OnHandcuffCriminal -= HandcuffCriminal;
        PlayerCriminalController.OnArrestCriminal -= PickupHandcuff;
    }

    public void PickupHandcuff(Handcuff handcuff)
    {
        handcuff.ChangeState(HandcuffState.InBackpack);
        handcuffs.Add(handcuff);
        handcuff.transform.parent = handcuffContainer;

        StartCoroutine(PlayHandcuffAnimation(handcuff, new Vector3(0, handcuffs.Count * spacing, 0), Quaternion.identity));
    }

    private void HandcuffCriminal(Criminal criminal)
    {
        Handcuff lastHandcuff = handcuffs[handcuffs.Count - 1];
        handcuffs.Remove(lastHandcuff);
        criminal.CriminalHandcuff = lastHandcuff;
        lastHandcuff.transform.parent = criminal.transform;

        StartCoroutine(PlayHandcuffAnimation(lastHandcuff, criminalHandcuffPos, Quaternion.Euler(criminalHandcuffRot)));
    }

    IEnumerator PlayHandcuffAnimation(Handcuff handcuff, Vector3 newPosition, Quaternion newRotation)
    {
        handcuff.transform.position += afterpickupPos;

        while (handcuff.transform.localPosition != newPosition)
        {
            handcuff.transform.localPosition = Vector3.Lerp(handcuff.transform.localPosition, newPosition, pickupSpeed);
            handcuff.transform.localRotation = Quaternion.Lerp(handcuff.transform.localRotation, newRotation, pickupSpeed);

            yield return null;
        }
    }
}
