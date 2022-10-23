using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCriminalController : MonoBehaviour
{
    // PlayerHandcuffController observes these two events
    public static event Action<Criminal> OnHandcuffCriminal = delegate { };
    public static event Action<Criminal> OnArrestCriminal = delegate { };

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

    //When Player touches the criminal this method called. 
    //This method only contains criminal related codes.
    public void HandcuffCriminal(Criminal criminal)
    {
        if (playerHandcuffController.NumberOfHandcuffs == 0)
            return;

        criminal.ChangeState(CriminalState.Caught);
        criminal.objectToFollow = criminals.Count == 0 ? this.gameObject.transform : criminals[criminals.Count - 1].transform;
        criminals.Add(criminal);

        OnHandcuffCriminal(criminal);
    }

    //When Player enters the prison area this method called. 
    //This method only contains criminal related codes.
    public void ArrestCriminal()
    {
        if (NumberOfCriminals == 0)
            return;

        Criminal lastCriminal = criminals[criminals.Count - 1];
        criminals.Remove(lastCriminal);
        lastCriminal.gameObject.SetActive(false);
        lastCriminal.objectToFollow = null;
        
        OnArrestCriminal(lastCriminal);
    }
}
