using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int childIndex = 0;
    [SerializeField] Vegetable vegetablePrefab;
    [SerializeField] Hand hand;

    private Vegetable vegetableReference;
    private Vector3 vegetablePosition = new Vector3(5.25f, 9.5f, -1.5f);
    private GameObject[] vegetableChildren;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Game Manager Already Exixts.");
    }

    private void Start()
    {
        vegetableReference = Instantiate(vegetablePrefab, vegetablePosition, Quaternion.identity);
        vegetableChildren = vegetableReference.GetVegetableChildren();
        hand.OnUp += Hand_OnUp;
        hand.OnDown += Hand_OnDown;
        hand.OnRight += Hand_OnRight;
    }

    private void Hand_OnRight(object sender, EventArgs e)
    {
        hand.SetSliderSlider(false);
        vegetableReference = Instantiate(vegetablePrefab, vegetablePosition, Quaternion.identity);
        vegetableChildren = vegetableReference.GetVegetableChildren();
        childIndex = 0;
    }
    private void Hand_OnDown(object sender, EventArgs e)
    {
        if (childIndex < vegetableChildren.Length)
        {
            vegetableReference.transform.GetChild(childIndex).GetComponent<Rigidbody>().isKinematic = false;

        }
        else if(childIndex == vegetableChildren.Length)
        {
            foreach (var child in vegetableChildren)
            {
                child.transform.SetParent(null);
            }
        }

        // Check if we have reached the last child
        if (childIndex == vegetableChildren.Length-1)
        {
            hand.SetSliderSlider(true);
        }

    }

    private void Hand_OnUp(object sender, EventArgs e)
    {
        // Only proceed if childIndex is within bounds for position update
        if (childIndex < vegetableChildren.Length - 1)
        {
            // Move vegetable reference based on the current child's size
            vegetableReference.transform.position -= new Vector3(
                vegetableReference.transform.GetChild(++childIndex).GetComponent<MeshCollider>().bounds.size.x,
                0,
                0
            );
        }
    }

    public int GetChildIndex()
    {
        return childIndex;
    }

    private void Update()
    {
        // Stop play mode or quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
