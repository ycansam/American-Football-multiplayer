using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Transform trhowBallPosition;
    [Header("Configuracion")]
    [SerializeField] private float force;
    [SerializeField] private float maxCharge; // segundos
    private float charging = 0;
    public bool ChargeThrow()
    {
        if (charging < maxCharge)
        {
            charging += Time.deltaTime;
        }
        return true;
    }
    public void Throw(Transform ball)
    {
        if (ball)
        {
            BallScrpt ballScrpt = ball.GetComponent<BallScrpt>();
            float throwValue = (charging / maxCharge) * force;
        
            //lanzando la bola en un sitio, rotacion y fuerza.
            ballScrpt.ThrowBall(trhowBallPosition, throwValue);
        }

    }

}
