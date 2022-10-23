using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonController : MonoBehaviour
{
    [SerializeField] private float arrestTime = .1f;

    private bool isInsidePrisonArea = false;
    private CriminalHandler criminalHandler;

    private void OnTriggerEnter(Collider other)
    {
        isInsidePrisonArea = true;

        other.gameObject.TryGetComponent<CriminalHandler>(out criminalHandler);

        if (criminalHandler != null)
        {
            StartCoroutine(StartArrest());
        }
    }

    IEnumerator StartArrest()
    {
        while (isInsidePrisonArea)
        {
            criminalHandler.Arrest();
            yield return new WaitForSeconds(arrestTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInsidePrisonArea = false;
    }
}
