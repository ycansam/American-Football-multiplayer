using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    // componentes
    private CharacterController characterController;

    [Header("Move Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool grounded = false;

    private float vertical_axis;
    private float horizontal_axis;

    private float gravity;
    private float verticalSpeed;

    public static float ActualSpeed { get => actualSpeed; }
    private static float actualSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravity = GameConstants.PLAYERS_GRAVITY;
    }

    void Update()
    {
        GetInput();
        PlayerMove();
    }
    private void FixedUpdate()
    {
        PlayerGravity();
    }
    private void GetInput()
    {
        vertical_axis = Input.GetAxis(GameConstants.VERTICAL);
        horizontal_axis = Input.GetAxis(GameConstants.HORIZONTAL);
    }
    /* 
    *   PlayerMove() - Movimiento del jugador
    */
    private void PlayerMove()
    {
        Vector3 move = (transform.forward * vertical_axis * speed) + (transform.right * horizontal_axis * speed);
        Vector3 clampedMove = Vector3.ClampMagnitude(move, speed); // definiendo la magnitud maxima para la velocidad del personaje
        CharController.actualSpeed = clampedMove.magnitude; // setting la variable para poder usarse publicamente en animations

        characterController.Move(clampedMove * Time.deltaTime);
    }
    /* 
    *   PlayerGravity() - Movimiento de caiga del jugador
    * @param 
    */
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
