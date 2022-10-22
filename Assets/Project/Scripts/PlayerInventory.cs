using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform handcuffInventoryTransform;
    [SerializeField] private Vector3 afterpickupPos;
    [SerializeField] private float pickupSpeed = .1f;
    [SerializeField] private float spacing = .5f;

    private List<Handcuff> handcuffs = new List<Handcuff>();

    public void AddToInventory(Handcuff handcuff)
    {
        handcuffs.Add(handcuff);
        handcuff.transform.parent = handcuffInventoryTransform;

        StartCoroutine(PlayPickupAnimation(handcuff, new Vector3(0, handcuffs.Count * spacing, 0)));
    }

    IEnumerator PlayPickupAnimation(Handcuff handcuff, Vector3 newPosition)
    {
        handcuff.transform.localPosition = afterpickupPos;

        while (handcuff.transform.localPosition != newPosition)
        {
            handcuff.transform.localPosition = Vector3.Lerp(handcuff.transform.localPosition, newPosition, pickupSpeed);
            handcuff.transform.localRotation = Quaternion.Lerp(handcuff.transform.localRotation, Quaternion.identity, pickupSpeed);

            yield return null;
        }
    }
}
