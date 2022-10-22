using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonController : MonoBehaviour
{
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
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInsidePrisonArea = false;
    }
}
