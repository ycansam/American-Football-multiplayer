using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{
    private Animator animator;

    public static float sprintFactor;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        SetSprintMultipler(CharAnimations.sprintFactor);
    }
    public void SetSprintMultipler(float multiplier){
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPRINT_MULTIPLIER, multiplier);
    }

    private void Update()
    {
        RunAnimation(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));
        AimAnimation();
        PunchAnimation();

    }

    private void RunAnimation(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPEED, CharController.ActualSpeed);
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_VERTICAL, vertical);
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_HORIZONTAL, horizontal);
    }
    private void AimAnimation(){
        animator.SetBool(GameConstants.ANIMATOR_PARAMETER_AIMING, CamController.Aiming);
    }

    private void PunchAnimation()
    {
        if (Input.GetButtonDown(GameConstants.BUTTON_FIRE1))
        {
            animator.SetTrigger("PUNCH");
        }
        if (Input.GetButtonUp(GameConstants.BUTTON_FIRE1))
        {
            animator.ResetTrigger("PUNCH");
        }
    }
}
