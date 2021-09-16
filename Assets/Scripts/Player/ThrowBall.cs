using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Transform trhowBallPosition;
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

    public bool ChargeThrow()
    {
        if (charging < chargeTime)
        {
            charging += Time.deltaTime;
        }
        return true;
    }
    public void Throw(Transform ball, float playerSpeed)
    {
        if (ball)
        {
            BallScrpt ballScrpt = ball.GetComponent<BallScrpt>();
            float throwValue = minForce + (charging / chargeTime) * ((force - minForce) + playerSpeed);
            //lanzando la bola en un sitio, rotacion y fuerza.
            ballScrpt.ThrowBall(trhowBallPosition, throwValue);
            charging = 0; // reseting the timer
        }
    }

}
