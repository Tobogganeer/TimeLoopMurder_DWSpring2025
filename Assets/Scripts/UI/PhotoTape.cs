using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTape : MonoBehaviour
{
    private void Start()
    {
        // Unparent one variation
        Transform variationToBeSpared = transform.GetChild(Random.Range(0, transform.childCount)).transform;
        variationToBeSpared.SetParent(transform.parent);

        // Goodbye everyone else
        Destroy(gameObject);
    }
}
