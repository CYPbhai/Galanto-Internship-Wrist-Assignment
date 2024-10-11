using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] GameObject[] children;

    public GameObject[] GetVegetableChildren()
    {
        return children;
    }
}
