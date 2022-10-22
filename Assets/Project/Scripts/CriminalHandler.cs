using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalHandler : MonoBehaviour
{
    public static event Action<Criminal> OnHandcuffCriminal = delegate { };
    public static event Action<Handcuff> OnArrestCriminal = delegate { };
    public int NumberOfCriminals { get { return criminals.Count; } }

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Transform criminalContainer;

    private List<Criminal> criminals = new List<Criminal>();

    private void OnEnable()
    {
        Criminal.OnPickup += HandcuffCriminal;
    }

    private void OnDisable()
    {
        Criminal.OnPickup -= HandcuffCriminal;
    }

    public void HandcuffCriminal(Criminal criminal)
    {
        if (playerInventory.NumberOfHandcuffs == 0)
            return;

        criminal.ChangeState(CriminalState.Caught);
        criminals.Add(criminal);
        criminal.transform.parent = criminalContainer;
        OnHandcuffCriminal(criminal);
    }

    public void Arrest()
    {
        if (NumberOfCriminals == 0)
            return;

        Criminal lastCriminal = criminals[criminals.Count - 1];
        OnArrestCriminal(lastCriminal.CriminalHandcuff);
        criminals.Remove(lastCriminal);
        lastCriminal.gameObject.SetActive(false);
    }
}
