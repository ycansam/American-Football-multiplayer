using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIKAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Transform bodyLookingGameobject = null;
    [SerializeField] bool ikActive = false;
    [SerializeField] int layerHorizontal = 1;
    [SerializeField] int layerAiming = 2;
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
                    if (!Input.GetKey(GameConstants.KEY_FREE_VISION))
                        animator.SetLookAtWeight(1, 0.3f, 1, 1, 0); // se gira la cabeza al maximo pero el cuerpo no
                    else if (Input.GetKey(GameConstants.KEY_FREE_VISION) && Input.GetAxisRaw(GameConstants.HORIZONTAL) != 0 || Input.GetAxisRaw(GameConstants.VERTICAL) != 0)
                        animator.SetLookAtWeight(1, 0.3f, 1, 1, 0.5f); // se gira la cabeza al maximo pero el cuerpo no
                    else if (Input.GetAxisRaw(GameConstants.HORIZONTAL) != 0 || Input.GetAxisRaw(GameConstants.VERTICAL) != 0)
                        animator.SetLookAtWeight(1, 1f, 1, 1, 1); // se gira todo limitado
                    else
                        animator.SetLookAtWeight(1); // se gira todo limitado


                    if (CamController.Aiming && !Input.GetKey(GameConstants.KEY_FREE_VISION)) // si esta apuntando el cuerpo se puede girar lo maximo posible
                        animator.SetLookAtWeight(1, 1, 1, 1, 0);
                    else if (CamController.Aiming && Input.GetKey(GameConstants.KEY_FREE_VISION))
                        animator.SetLookAtWeight(0); 


                    animator.SetLookAtPosition(bodyLookingGameobject.position);
                    animator.SetLayerWeight(layerAiming, System.Convert.ToSingle(CamController.Aiming));
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
        if (horizontalRaw != 0 || verticalRaw != 0)
        {
            animator.SetLayerWeight(layerHorizontal, 1);
        }
        else
        {
            if (horizontal > 0)
                animator.SetLayerWeight(layerHorizontal, horizontal);
            else if (horizontal < 0)
                animator.SetLayerWeight(layerHorizontal, -horizontal);

            if (horizontal < 0.2 || horizontal > -0.2)
                animator.SetLayerWeight(layerHorizontal, 0);
        }
    }
}
