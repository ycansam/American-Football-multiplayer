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
        RunAnimation(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));
        AimAnimation();
        PunchAnimation();

    }

    private void RunAnimation(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_SPEED, CharController.ActualSpeed);
        animator.SetFloat(GameConstants.ANIMATOR_VERTICAL, vertical);
        animator.SetFloat(GameConstants.ANIMATOR_HORIZONTAL, horizontal);
    }
    private void AimAnimation(){
        animator.SetBool(GameConstants.ANIMATOR_AIMING, CamController.Aiming);
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
}
