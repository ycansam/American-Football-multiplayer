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
            // si esta activado el cuerpo cabeza etc seguira mirando a un objetivo
            if (ikActive)
            {
                if (bodyLookingGameobject != null)
                {
                    SettingWeights();
                }
            }
            else
            {
                // si esta desactivado no mirara nada.
                animator.SetLookAtWeight(0, 0);
            }
            SetWeightSprint(CharController.SprintWeight);

            SetAnimationDirectionRunning(Input.GetAxisRaw(GameConstants.HORIZONTAL), Input.GetAxisRaw(GameConstants.VERTICAL));
        }
    }

    /// <summary ="SettingWeights()">
    ///  funcion para añadir el weight cuando esta corriendo de izquierda a derecha y mientras apunta.
    /// </summary>
    private void SettingWeights()
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

        int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_AIMING);
        animator.SetLayerWeight(index, System.Convert.ToSingle(CamController.Aiming));
    }


    /// <summary ="SetAnimationDirectionRunning()">
    ///  funcion para añadir el weight de los layers correspondientes para las animaciones
    /// <param ="horizontalRaw"> Axis Horizontal 0 || 1
    /// <param ="verticalRaw"> Axis Vertical 0 || 1
    /// </summary>
    private void SetAnimationDirectionRunning(float horizontalRaw, float verticalRaw)
    {
        int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_HORIZONTAL);
        float horizontal = Input.GetAxis(GameConstants.HORIZONTAL);
        float vertical = Input.GetAxis(GameConstants.VERTICAL);

        if (horizontalRaw != 0 || verticalRaw != 0)
        {
            // si el jugador se mueve comenzaran las animaciones
            animator.SetLayerWeight(index, 1);
        }
        else
        {
            // si el jugaddor empieza a moverse izquierda o derecha tambien se activa por la cantidad que haya en el horizontal
            if (horizontal > 0)
                animator.SetLayerWeight(index, horizontal);
            else if (horizontal < 0)
                animator.SetLayerWeight(index, -1 * (horizontal));

            // correccion de error para el mando para ponerla a 0 y se quede en idle.
            if (horizontal < 0.2 || horizontal < -0.2)
                animator.SetLayerWeight(index, 0);
        }
    }
    /// <summary ="SetWeightSprint()">
    ///  Añade el peso a la animacion de sprint
    /// <param ="weight"> parametro procediente  de la clase CharController. 0 a 1
    /// </summary>
    private void SetWeightSprint(float weight)
    {
        int index = animator.GetLayerIndex(GameConstants.ANIMATOR_LAYER_SPRINT);
        animator.SetLayerWeight(index, weight); // obtiene el peso en referencia al tiempo transcurrido sprintando
    }
}
