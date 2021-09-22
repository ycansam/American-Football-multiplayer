using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRagdoll : MonoBehaviour
{
    [Header("Components")]
    private Animator animator;
    private CharacterController characterController;
    private CharController charController;
    private Collider[] AllColliders;
    private Rigidbody[] AllRigibodys;
    [SerializeField] private Transform hips;
    public bool isOnRagdoll = false;
    Vector3 impact = Vector3.zero;
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
        charController = GetComponentInParent<CharController>();
        AllColliders = GetComponentsInChildren<Collider>();
        AllRigibodys = GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in AllColliders)
            Physics.IgnoreCollision(characterController, col);
        foreach (Rigidbody rb in AllRigibodys)
            rb.useGravity = false;

        DoRagdoll(false);
    }
    private void Update()
    {
        MoveRagdoll();
    }

    private void MoveRagdoll()
    {
        if (!animator.enabled)
        {
            hips.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
            if (impact.magnitude > 0.2F) characterController.Move(impact * Time.deltaTime);
            AddingGravity();
            // consumes the impact energy each cycle:
            impact = Vector3.LerpUnclamped(impact, Vector3.zero, 1 * Time.deltaTime);
        }
    }

    /// <summary name="DoRagdoll()"> activa o desactiva el ragdoll
    /// <param name="isRagdoll">true false
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (Collider col in AllColliders)
            col.enabled = isRagdoll;
        foreach (Rigidbody rb in AllRigibodys)
            rb.useGravity = isRagdoll;
        
        animator.enabled = !isRagdoll;
        charController.enabled = !isRagdoll;
        isOnRagdoll = isRagdoll;
    }

    /// <summary ="AddImpact"> Añade un impacto al caracter controler simulando fisicas
    /// <param name="dir"> Direccion del impacto (hacia donde va a ir)
    /// <param name="force"> Fuerza del Impacto
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / 1f;
    }

    bool grounded;
    float verticalSpeed;
    private void AddingGravity()
    {
        if (grounded)
        {
            verticalSpeed = -GameConstants.PLAYERS_GRAVITY * Time.deltaTime;
        }
        else
        {
            verticalSpeed -= GameConstants.PLAYERS_GRAVITY * Time.deltaTime;
        }
        characterController.Move((transform.up * verticalSpeed) * Time.deltaTime);
        grounded = characterController.isGrounded;
    }
}
