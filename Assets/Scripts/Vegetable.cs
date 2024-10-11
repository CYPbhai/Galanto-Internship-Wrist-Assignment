using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] GameObject[] children;

    public GameObject[] GetVegetableChildren()
    {
        return children;
    }
}
