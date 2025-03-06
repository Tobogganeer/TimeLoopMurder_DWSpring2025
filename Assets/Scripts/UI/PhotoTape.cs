using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTape : MonoBehaviour
{
    private void Start()
    {
        // Spare one child from our wrath
        Transform variationToBeSpared = transform.GetChild(Random.Range(0, transform.childCount)).transform;
        variationToBeSpared.SetParent(transform.parent);

        // Goodbye everyone else
        Destroy(gameObject);
    }
}
