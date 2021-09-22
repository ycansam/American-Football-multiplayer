using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [Header("Componentes")]
    public Transform trhowBallPosition;
    public Transform onRagdollTrhowPosition;
    [Header("Configuracion")]
    [SerializeField] private float minForce = 5f; // usado para la carga minima
    [SerializeField] private float force = 30f;
    [SerializeField] private float chargeTime = 1.5f; //tiempo de carga segundos
    private float charging = 0;
    private void OnGUI()
    {
        float value = minForce + (charging / chargeTime) * (force - minForce);
        GUI.Label(new Rect(0, 50, 150, 150), "ThrowValue: " + value);
        value = 0;
    }

    /// <function name="ChargeThrow()" Empieza a cargar para lanzar la bola>
    public bool ChargeThrow()
    {
        if (charging < chargeTime)
        {
            charging += Time.deltaTime;
        }
        return true;
    }

    /// <function name="ThrowWithCharge()" se utiliza para lanzar despues de cargar>
    /// <param name="ball" la bola que se va a lanzar>
    /// <param name="playerSpeed" velocidad que lleva el jugador en el momento para aumentar la fuerza >
    public void ThrowWithCharge(Transform throwPosition, Transform ball, float playerSpeed, Vector3 direction)
    {
        if (ball)
        {
            BallScrpt ballScrpt = ball.GetComponent<BallScrpt>();
            float throwValue = minForce + (charging / chargeTime) * ((force - minForce) + playerSpeed);
            //lanzando la bola en un sitio, rotacion y fuerza.
            ballScrpt.ThrowBall(trhowBallPosition, throwValue, direction);
            charging = 0; // reseting the timer
        }
    }

    /// <function name="Throw" lanza la bola en cualquier sitio en una direccion>
    /// <param name="ball" la bola que se va a lanzar>
    /// <param name="forceValue" fuerza con la que se lanza >
    public void Throw(Transform throwPosition, Transform ball, float forceValue, Vector3 direction)
    {
        if (ball)
        {
            BallScrpt ballScrpt = ball.GetComponent<BallScrpt>();
            //lanzando la bola en un sitio, rotacion y fuerza.
            ballScrpt.ThrowBall(throwPosition, forceValue, direction);
            charging = 0; // reseting the timer
        }
    }

}
