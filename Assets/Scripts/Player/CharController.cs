using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    [Header("Components")]
    // componentes
    private CharacterController characterController;

    [Header("Move Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintFactor;
    [SerializeField] float transitionSprintSeconds;
    [Header("Variables")]
    private bool grounded = false;

    private float vertical_axis;
    private float horizontal_axis;

    private float gravity;
    private float verticalSpeed;


    bool sprinting = false;
    float timer = 0;
    float sprintValue;

    /// <EstaticosParaOtrasClases>
    public static float ActualSpeed
    { get => actualSpeed; }
    private static float actualSpeed;

    // weight que indica la cantidad de animacion se aplica cuando se sprinta (de 0 a 1)
    public static float SprintWeight
    {
        get => sprintWeight;
    }
    private static float sprintWeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravity = GameConstants.PLAYERS_GRAVITY;

        // pasando los valores del multiplicador a las animaciones
        CharAnimations.sprintFactor = sprintFactor;
    }

    void Update()
    {
        GetInput();

        if (!sprinting)
        {
            PlayerMove();
            ResetTimersSprint();
        }
        else
        {
            Sprint();
        }
    }

    private void FixedUpdate()
    {
        PlayerGravity();
    }

    /// <summary ="GetInput()">
    /// Obtiene los inputs
    /// </summary>
    private void GetInput()
    {
        vertical_axis = Input.GetAxis(GameConstants.VERTICAL);
        horizontal_axis = Input.GetAxis(GameConstants.HORIZONTAL);

        sprinting = Input.GetKey(GameConstants.KEY_SPRINT);

        // reseteo el sprint si el jugador va a izquierda o derecha
        if (Input.GetAxis(GameConstants.HORIZONTAL) > 0.2f || Input.GetAxis(GameConstants.HORIZONTAL) < -0.2f || Input.GetAxis(GameConstants.VERTICAL) < 0.2f)
        {
            sprinting = false;

        }
    }
    /// <summary ="PlayerMove()">
    /// Movimiento del jugador
    /// </summary>
    private void PlayerMove()
    {
        Vector3 move = (transform.forward * vertical_axis * speed) + (transform.right * horizontal_axis * speed);
        Vector3 clampedMove = Vector3.ClampMagnitude(move, speed); // definiendo la magnitud maxima para la velocidad del personaje
        CharController.actualSpeed = clampedMove.magnitude; // setting la variable para poder usarse publicamente en animations

        characterController.Move(clampedMove * Time.deltaTime);
    }

    /// <summary ="ResetTimersSprint()">
    /// Reset del Timer
    /// </summary>
    private void ResetTimersSprint()
    {
        if (timer != 0)
            timer = 0;
        if (sprintValue != 0)
            sprintValue = 0;
        SendWeightToAnimator(0);
    }
    /// <summary ="Sprint()">
    /// Cuando el jugador hace sprint
    /// </summary>
    private void Sprint()
    {

        if (Input.GetAxis(GameConstants.VERTICAL) > 0.2f)
        {
            TimerSprint();
            Vector3 move = (transform.forward * vertical_axis * speed * sprintValue);
            Vector3 clampedMove = Vector3.ClampMagnitude(move, speed * sprintValue); // definiendo la magnitud maxima para la velocidad del personaje
            CharController.actualSpeed = clampedMove.magnitude; // setting la variable para poder usarse publicamente en animations

            characterController.Move(clampedMove * Time.deltaTime);
        }
        else
        {
            SendWeightToAnimator(0);
        }

    }
    /// <summary ="TimerSprint()">
    /// timer que empieza cuando se pulsa el boton de sprint
    /// </summary>
    private void TimerSprint()
    {
        if (timer < transitionSprintSeconds)
        {
            timer += Time.deltaTime;
            // mas 1 para que no empieze desde 0.
            if (sprintValue < sprintFactor)
                sprintValue = 1 + (timer / transitionSprintSeconds) * (sprintFactor - 1f);
            SendWeightToAnimator((timer / transitionSprintSeconds));
        }
    }

    private void SendWeightToAnimator(float value)
    {
        CharController.sprintWeight = value;
    }

    /// <summary ="PlayerGravity()">
    /// Movimiento de caiga del jugador
    /// </summary>
    private void PlayerGravity()
    {
        if (grounded)
        {
            verticalSpeed = -gravity * Time.fixedDeltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
            }
        }
        else
        {
            verticalSpeed -= gravity * Time.fixedDeltaTime;
        }
        characterController.Move((transform.up * verticalSpeed) * Time.fixedDeltaTime);
        grounded = characterController.isGrounded;
    }

}
