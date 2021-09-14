using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallScrpt : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Collider ballCollider;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharBallController charBallController = other.GetComponent<CharBallController>();
        if (charBallController)
        {
            // si la bola no esta poseida por ningun jugador == null
            if (!transform.parent)
            {

                rb.Sleep(); // desactiva el rigibody
                rb.useGravity = !rb.IsSleeping(); // desactiva la gravedad
                ballCollider.enabled = !rb.IsSleeping(); // desactiva el collider para evitar choques con el jugador
                charBallController.SetBallInPossesion(transform);
            }
        }
    }

    // se vuelve a activar
    public void WakeUpAllComponents()
    {
        rb.WakeUp(); // despierta el rigibody
        rb.useGravity = !rb.IsSleeping(); // desactiva la gravedad
        ballCollider.enabled = !rb.IsSleeping(); // activa el collider 
    }

    public void ThrowBall(Transform trhowPos, float force)
    {
        Debug.Log(rb.IsSleeping());
        transform.position = trhowPos.position;
        WakeUpAllComponents();
        rb.AddForce(trhowPos.forward * force, ForceMode.Impulse);
    }

}
