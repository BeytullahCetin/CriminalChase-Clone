using System.Collections;
using UnityEngine;

public class PrisonArea : MonoBehaviour
{
    [SerializeField] private float arrestTime = .1f;

    private bool isInsidePrisonArea = false;
    private PlayerCriminalController criminalHandler;

    private void OnTriggerEnter(Collider other)
    {
        isInsidePrisonArea = true;

        other.gameObject.TryGetComponent<PlayerCriminalController>(out criminalHandler);

        if (criminalHandler != null)
        {
            StartCoroutine(StartArrest());
        }
    }

    //When player enters the arrest area this coroutine starts.
    //If this wasn't coroutine all criminals arrested instantly. 
    IEnumerator StartArrest()
    {
        while (isInsidePrisonArea)
        {
            criminalHandler.ArrestCriminal();
            yield return new WaitForSeconds(arrestTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInsidePrisonArea = false;
    }
}
