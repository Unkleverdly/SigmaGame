using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private GameObject basePlane;

    public Vector3 EndPoint => transform.position + basePlane.transform.localScale.WithX(0).WithY(0) * 10;

    private void Awake()
    {
        UpdatePlane();
    }

    private void OnValidate()
    {
        UpdatePlane();
    }

    private void UpdatePlane()
    {
        basePlane.transform.localScale = basePlane.transform.localScale.WithZ(length);
        basePlane.transform.localPosition = new(0, 0, 5 * length); // z * 10 / 2
    }

    public void Setup(int difficulty, bool sigmaCoins)
    {

    }
}
