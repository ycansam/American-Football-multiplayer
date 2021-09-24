using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour
{
    [Header("Componentes")]
    private Animator animator;
    private CharController charController;
    private AllyController allyController;
    public static float sprintFactor;
    private float timerJump = 1;
    [SerializeField] private float jumpTransitionSeconds;
    private float layerWeightJump;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        charController = GetComponent<CharController>();
        allyController = GetComponent<AllyController>();
        SetSprintMultipler(CharAnimations.sprintFactor);
        timerJump = jumpTransitionSeconds;
    }
    public void SetSprintMultipler(float multiplier)
    {
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPRINT_MULTIPLIER, multiplier);
    }

    private void Update()
    {
        Flying();

        if (charController.isLocalClient)
        {
            RunAnimation(Input.GetAxis(GameConstants.HORIZONTAL), Input.GetAxis(GameConstants.VERTICAL));
            AimAnimation();
            PunchAnimation();

            JumpAnimation();
        }

        if (charController.isOnShoulder)
            PlayerOnShoulder();
        else
            CatchingOtherPlayer();
    }

    private void RunAnimation(float horizontal, float vertical)
    {
        animator.SetFloat(GameConstants.ANIMATOR_PARAMETER_SPEED, charController.ActualSpeed);
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
        if (timerJump >= 0f)
        {
            timerJump -= Time.deltaTime;
            layerWeightJump = (timerJump / jumpTransitionSeconds);
        }
    }
    private void Flying()
    {
        int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_FLYING);
        animator.SetLayerWeight(index, System.Convert.ToSingle(charController.isFlying));
        animator.SetBool(GameConstants.ANIMATOR_PARAMETER_FLYING, charController.isFlying);
    }

    private void PlayerOnShoulder()
    {
        Debug.Log(gameObject.name);

        if (transform.parent != null)
        {
            animator.SetBool(GameConstants.ANIMATOR_PARAMETER_ON_SHOULDER_STAY, true);

            int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_CATCHED);
            animator.SetLayerWeight(index, 1);
        }
        else if (transform.parent)
        {
            animator.SetBool(GameConstants.ANIMATOR_PARAMETER_ON_SHOULDER_STAY, false);

            int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_CATCHED);
            animator.SetLayerWeight(index, 0);
        }

    }

    private void CatchingOtherPlayer()
    {
        if (allyController.allyInPosession)
        {
            animator.SetBool(GameConstants.ANIMATOR_PARAMETER_CATCHING_PLAYER, true);


            int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_CATCHING);
            animator.SetLayerWeight(index, 1);
        }
        else
        {
            animator.SetBool(GameConstants.ANIMATOR_PARAMETER_CATCHING_PLAYER, false);

            int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_CATCHING);
            animator.SetLayerWeight(index, 0);
        }
    }

}
