using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public event EventHandler OnDown;
    public event EventHandler OnUp;
    public event EventHandler OnRight;

    [SerializeField] private GameObject slider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetSliderSlider(false);
    }

    public void SetSliderSlider(bool active)
    {
        slider.gameObject.SetActive(active);
    }

    private void Update()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        // Trigger animations based on input
        if (!currentState.IsName("UpHandAnimation") && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && currentState.IsName("DownHandAnimation"))
        {
            animator.SetTrigger("Up");
            OnUp?.Invoke(this, EventArgs.Empty);
        }
        if (!currentState.IsName("DownHandAnimation") && Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && (currentState.IsName("UpHandAnimation") || currentState.IsName("SlideRightAnimation") || currentState.IsName("Idle")))
        {
            animator.SetTrigger("Down");
            OnDown?.Invoke(this, EventArgs.Empty);
        }
        if (!currentState.IsName("LeftHandAnimation") && Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && currentState.IsName("DownHandAnimation") && (GameManager.Instance.GetChildIndex()==19))
        {
            animator.SetTrigger("Left");
        }
        if (!currentState.IsName("RightHandAnimation") && Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && currentState.IsName("SlideLeftHandAnimation"))
        {
            animator.SetTrigger("Right");
            OnRight?.Invoke(this, EventArgs.Empty);
        }
    }
}
