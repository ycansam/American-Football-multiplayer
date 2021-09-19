using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharBallController))]
// [RequireComponent(typeof(Rigidbody))]
public class CharController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Shield playerShield;
    // componentes
    private CharacterController characterController;
    private CharBallController charBallController;
    private ThrowBall throwBallController;

    [Header("Move Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedWithBall = 8.5f;
    [SerializeField] private float speedWithBallAimingFactor = 0.7f;
    [SerializeField] private float sprintFactor = 1.5f;
    [SerializeField] float transitionSprintSeconds = 2.5f;
    [Header("Jump Settings")]
    [SerializeField] private float jumpForceMin = 5f;
    [SerializeField] private float jumpForceMax = 15f;
    [SerializeField] private float transitionJumpInSeconds = 2f;
    float timerJumping = 0;
    float finalJumpForce;
    [Header("Variables")]
    private bool grounded = false;
    private bool jumped = false;
    private bool aiming = false;
    private bool punching = false;
    bool hasBall;                   // si el jugador tiene la bola o no

    private float vertical_axis;
    private float horizontal_axis;

    private float gravity;
    private float verticalSpeed;

    bool sprinting = false;
    float timer = 0; // timer para el sprint
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
    bool startedCharge = false; // charge to throw the ball
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        charBallController = GetComponent<CharBallController>();
        throwBallController = GetComponent<ThrowBall>();
        gravity = GameConstants.PLAYERS_GRAVITY;

        // pasando los valores del multiplicador a las animaciones
        CharAnimations.sprintFactor = sprintFactor;
    }
    private void OnGUI()
    {
        GUILayout.Label("Speed: " + CharController.actualSpeed);
        GUILayout.Label("JumpForce: " + finalJumpForce);
    }

    void Update()
    {
        GetInput();

        PlayerShield();
        PlayerWithBallActions();

        if (!sprinting)
        {
            if (!hasBall)
            {
                PlayerMove(speed); // corre normal
            }
            else
            {
                // cambia la velocidad si esta aimeando y solo si esta en el suelo
                if (!aiming)
                    PlayerMove(speedWithBall); // corre con la bola
                else if (aiming && !grounded)
                    PlayerMove(speedWithBall * speedWithBallAimingFactor); // corre con la bola
            }
            ResetTimersSprint();
        }
        else
        {
            Sprint();
        }

        PlayerGravity();
    }

    private void FixedUpdate()
    {
    }

    /// <summary ="GetInput()">
    /// Obtiene los inputs
    /// </summary>
    private void GetInput()
    {
        vertical_axis = Input.GetAxis(GameConstants.VERTICAL);
        horizontal_axis = Input.GetAxis(GameConstants.HORIZONTAL);

        sprinting = Input.GetKey(GameConstants.KEY_SPRINT);
        CamController.Aiming = Input.GetButton(GameConstants.BUTTON_FIRE2);

        aiming = CamController.Aiming;
        hasBall = charBallController.ballInPossesion; // control sobre la bola. Si exite true, else not
        punching = Input.GetButton(GameConstants.BUTTON_FIRE1);

        // reseteo el sprint si el jugador va a izquierda o derecha
        if (Input.GetAxis(GameConstants.HORIZONTAL) > 0.2f
        || Input.GetAxis(GameConstants.HORIZONTAL) < -0.2f
        || Input.GetAxis(GameConstants.VERTICAL) < 0.2f
        || hasBall)
        {
            sprinting = false;
        }
    }

    /// <summary name="PlayerWithBallActions()">
    /// Acciones cuando tiene la bola
    /// </summary>
    private void PlayerWithBallActions()
    {
        bool throwing = Input.GetButton(GameConstants.BUTTON_FIRE1);
        if (aiming && throwing && hasBall)
        {
            // se carga la bola para lanzarse
            startedCharge = throwBallController.ChargeThrow();
        }
        else if (!throwing && hasBall && startedCharge)
        {
            // cuando se suelta el boton se dispara la bola y la libera
            startedCharge = false;
            throwBallController.Throw(charBallController.ballInPossesion, CharController.ActualSpeed);
            charBallController.RemoveParentFromBall();
        }
    }

    /// <summary name="PlayerShield()">
    /// Acciones cuando el jugador activa el escudo
    /// </summary>
    private void PlayerShield()
    {
        if (sprinting)
        {
            if (playerShield.GetOpenShield())
                playerShield.CloseShield();
            return;
        }
        // abriendo el escudo para golpear
        if (playerShield && aiming && !hasBall && !playerShield.GetOpenShield())
        {
            playerShield.OpenShield();
            if(punching){
                
            }
        }// cerrando escudo
        else if (playerShield && !aiming && !hasBall && playerShield.GetOpenShield())
        {
            playerShield.CloseShield();
        }
    }

    /// <summary ="PlayerMove()">
    /// Movimiento del jugador
    /// speed segun tiene posesion de la bola o no
    /// </summary>
    private void PlayerMove(float speed)
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
            verticalSpeed = -gravity * Time.deltaTime;
            jumped = false;

            if (Input.GetKey(GameConstants.KEY_JUMP)){
                Jump(jumpForceMin);
                jumped = true;
            }
            // JumpTransition(); por si quiero hacer un salto propulsado
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        // JetPack(); // por si en vez de un salto quiero hacer un jetpack
        characterController.Move((transform.up * verticalSpeed) * Time.deltaTime);
        grounded = characterController.isGrounded;
    }
    public bool GetPlayerJumped(){
        return jumped;
    }

    // not implemented
    private void JumpTransition()
    {
        bool jumping = Input.GetKeyDown(GameConstants.KEY_JUMP);
        if (jumping)
        {
            if (timerJumping < transitionJumpInSeconds)
            {
                timerJumping += Time.deltaTime;
                finalJumpForce = jumpForceMin + (timerJumping / transitionJumpInSeconds) * (jumpForceMax - jumpForceMin);
            }
        }
        else if (timerJumping > 0.01f)
        {
            Jump(finalJumpForce);
            timerJumping = 0;
            finalJumpForce = 0;
        }
    }
    // not implemented
    private void JetPack()
    {
        bool flying = Input.GetKey(GameConstants.KEY_JUMP);
        if (flying)
            verticalSpeed = jumpForceMin;
    }

    private void Jump(float force)
    {
        verticalSpeed = force;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body)
        {
            BallScrpt ball = body.GetComponent<BallScrpt>();
            CheckColliderByHit(hit, body, ball.transform);
        }
    }
    void CheckColliderByHit(ControllerColliderHit hit, Rigidbody body, Transform Object)
    {
        // no rigidbody no object to compare
        if (body == null || body.isKinematic || !Object)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.AddForce(pushDir * CharController.ActualSpeed, ForceMode.Impulse);
    }
}
