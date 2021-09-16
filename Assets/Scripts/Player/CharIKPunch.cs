using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIKPunch : MonoBehaviour
{
    // Start is called before the first frame updateprivate Animator animator;
    private Animator animator;
    [SerializeField] Transform bodyLookingGameobject = null;
    [SerializeField] bool ikActive = false;
    [SerializeField] [Range(0, 1f)] float rightHandWeight = 0;
    AnimatorClipInfo[] m_CurrentClipInfo;
    private enum RigAnimMode
    {
        off,
        inc,
        dec,
    }
    private RigAnimMode mode = RigAnimMode.off;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // si esta activado el cuerpo cabeza etc seguira mirando a un objetivo
            if (ikActive)
            {
                if (bodyLookingGameobject != null)
                {
                    m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_PUNCH));
                    if (Input.GetButtonDown(GameConstants.BUTTON_FIRE1) && m_CurrentClipInfo.Length == 0)
                    {
                        mode = RigAnimMode.inc;
                    }

                    switch (mode)
                    {
                        case RigAnimMode.inc:
                            rightHandWeight = Mathf.Lerp(rightHandWeight, 1, 6 * Time.deltaTime);
                            if (rightHandWeight > 0.95f)
                            {
                                rightHandWeight = 1;
                                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                                mode = RigAnimMode.dec;
                            }
                            break;
                        case RigAnimMode.dec:
                            rightHandWeight = Mathf.Lerp(rightHandWeight, 0, 10f * Time.deltaTime);
                            if (rightHandWeight < 0.1f)
                            {
                                rightHandWeight = 0;
                                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                                mode = RigAnimMode.off;
                            }
                            break;
                    }
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                }
            }
            else
            {
                // si esta desactivado no mirara nada.
                animator.SetLookAtWeight(0, 0);
            }

        }
    }
    private void FixedUpdate()
    {

    }
}
