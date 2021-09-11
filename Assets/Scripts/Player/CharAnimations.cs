using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{
    private Animator animator;
    private bool playingWithKeyboard = true;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PunchAnimation();
        if (playingWithKeyboard)
            RunAnimationKeyboard(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));

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
    private void RunAnimationKeyboard(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_SPEED, CharController.ActualSpeed);
        animator.SetFloat(GameConstants.ANIMATOR_VERTICAL, vertical);
        animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, horizontal);


        // if (Input.GetAxisRaw(GameConstants.VERTICAL) == -1)
        // {

        //     animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, horizontal);

        // }
        // else
        // {
        //     if (Input.GetAxisRaw(GameConstants.HORIZONTAL) > 0)
        //     {
        //         if ((horizontal * 2) - vertical > 0)
        //             animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, (horizontal * 2) - vertical);
        //     }
        //     else if (Input.GetAxisRaw(GameConstants.HORIZONTAL) < 0)
        //     {
        //         if ((horizontal * 2) + vertical < 0)
        //             animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, (horizontal * 2) + vertical);
        //     }
        //     else if (Input.GetAxisRaw(GameConstants.HORIZONTAL) == 0)
        //     {
        //         animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, horizontal);
        //     }
        // }

    }


}
