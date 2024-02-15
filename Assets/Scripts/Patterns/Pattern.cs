using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    // used for determining length
    [SerializeField] private GameObject basePlane;

    public float Length => basePlane.transform.localScale.z;
}
