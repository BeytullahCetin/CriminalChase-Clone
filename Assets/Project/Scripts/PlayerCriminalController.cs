using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCriminalController : MonoBehaviour
{
    public static event Action<Criminal> OnHandcuffCriminal = delegate { };
    public static event Action<Handcuff> OnArrestCriminal = delegate { };
    public int NumberOfCriminals { get { return criminals.Count; } }

    [SerializeField] private PlayerHandcuffController playerHandcuffController;

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
        if (playerHandcuffController.NumberOfHandcuffs == 0)
            return;

        criminal.ChangeState(CriminalState.Caught);
        criminal.objectToFollow = criminals.Count == 0 ? this.gameObject.transform : criminals[criminals.Count - 1].transform;
        criminals.Add(criminal);

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
        lastCriminal.objectToFollow = null;
    }
}
