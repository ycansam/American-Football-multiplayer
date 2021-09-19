using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{
    private Animator animator;
    private CharController charController;
    public static float sprintFactor;

    private float timerJump = 1;
    [SerializeField] private float jumpTransitionSeconds;
    float layerWeightJump;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        charController = GetComponent<CharController>();
        SetSprintMultipler(CharAnimations.sprintFactor);
        timerJump = jumpTransitionSeconds;
    }
    public void SetSprintMultipler(float multiplier)
    {
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPRINT_MULTIPLIER, multiplier);
    }

    private void Update()
    {
        RunAnimation(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));
        AimAnimation();
        PunchAnimation();

        JumpAnimation();

    }

    private void RunAnimation(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPEED, CharController.ActualSpeed);
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_VERTICAL, vertical);
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_HORIZONTAL, horizontal);
    }
    private void AimAnimation()
    {
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
    private void JumpAnimation()
    {
        if (!charController.GetPlayerJumped())
        {
            TransitionJumpDown();
        }
        else
        {
            if (timerJump != jumpTransitionSeconds)
            {
                timerJump = jumpTransitionSeconds;
            }
        }

        animator.SetBool(GameConstants.ANIMATOR_PARAMETER_PLAYER_JUMPED, charController.GetPlayerJumped());

        int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_JUMP);
        int indexTop = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_JUMPTOP);

        if (charController.GetPlayerJumped())
        {
            animator.SetLayerWeight(index, System.Convert.ToSingle(charController.GetPlayerJumped()));
            animator.SetLayerWeight(indexTop, System.Convert.ToSingle(charController.GetPlayerJumped()));
        }
        else
        {
            animator.SetLayerWeight(index, layerWeightJump);
            animator.SetLayerWeight(indexTop, layerWeightJump);
        }
    }

    private void TransitionJumpDown()
    {
        Debug.Log(timerJump);

        if (timerJump >= 0f)
        {
            timerJump -= Time.deltaTime;
            layerWeightJump = (timerJump / jumpTransitionSeconds);
        }
    }
}
