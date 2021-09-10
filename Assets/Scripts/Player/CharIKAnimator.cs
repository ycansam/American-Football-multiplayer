using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIKAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Transform bodyLookingGameobject = null;
    [SerializeField] bool ikActive = false;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (ikActive)
            {
                if (bodyLookingGameobject != null)
                {
                    if (!Input.GetKey(GameConstants.KEY_FREE_VISION) && Input.GetAxis(GameConstants.HORIZONTAL) == 0)
                        animator.SetLookAtWeight(1, 1);
                    else
                        animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(bodyLookingGameobject.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(0, 0);
            }
            SetAnimationDirectionRunning(Input.GetAxisRaw(GameConstants.HORIZONTAL), Input.GetAxisRaw(GameConstants.VERTICAL));


        }
    }
    private void SetAnimationDirectionRunning(float horizontalRaw, float verticalRaw)
    {
        float horizontal = Input.GetAxis(GameConstants.HORIZONTAL);
        float vertical = Input.GetAxis(GameConstants.VERTICAL);
        if (horizontalRaw > 0)
        {
            if (verticalRaw != 1)
            {
                animator.SetLayerWeight(1, horizontalRaw);
            }
            else
            {
                animator.SetLayerWeight(1, horizontalRaw);

            }

        }
        else if (horizontalRaw < 0)
        {
            if (verticalRaw != 1)
            {
                animator.SetLayerWeight(1, -horizontalRaw);
            }
            else
            {
                animator.SetLayerWeight(1, (-horizontalRaw - (-vertical)) / 2);
            }
        }
        else
        {
            animator.SetLayerWeight(1, Input.GetAxis(GameConstants.HORIZONTAL));
        }

    }
}
