using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PunchAnimation();
        RunAnimation(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));

    }
    private void PunchAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("PUNCH");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.ResetTrigger("PUNCH");
        }
    }
    private void RunAnimation(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_SPEED, CharController.ActualSpeed);
        animator.SetFloat(GameConstants.ANIMATOR_VERTICAL, vertical);
        Debug.Log(horizontal * 2 + vertical);

        if (Input.GetAxisRaw(GameConstants.HORIZONTAL) > 0)
        {
            if ((horizontal * 2) - vertical > 0)
                animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, (horizontal * 2) - vertical);
        }
        else if (Input.GetAxisRaw(GameConstants.HORIZONTAL) < 0)
        {
            if ((horizontal * 2) + vertical < 0)
            animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, (horizontal * 2) + vertical);
        }
        else if (Input.GetAxisRaw(GameConstants.HORIZONTAL) == 0)
        {
            animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, horizontal);
        }

    }


}
