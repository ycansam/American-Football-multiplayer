using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] // necesita un collider en trigger
public class CharBallController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform ballLiberationPos;
    [HideInInspector] public Transform ballInPossesion;

    private void Update()
    {
        if (ballInPossesion)
        {
            ballInPossesion.position = playerHand.position;
        }
    }
    public void SetBallInPossesion(Transform ball)
    {
        if (ball.tag == GameConstants.TAG_BALON)
        {
            // si el jugador entra en el trigger de la bola este la recoge
            ballInPossesion = ball.transform;
            ballInPossesion.SetParent(playerHand);
        }
    }

    // funcion para liberar la bola de la posesion del jugador
    public void SetBallFreeOutOfPosesion()
    {
        // auxiliar para evitar errores con los colliders
        if (ballInPossesion)
        {
            // si no ha sido lanzada con carga no se mueve a esa
            ballInPossesion.GetComponent<BallScrpt>().WakeUpAllComponents(); // vuelve a activar los componentes
            ballInPossesion.position = ballLiberationPos.position;
            RemoveParentFromBall();
        }
    }

    public void RemoveParentFromBall()
    {
        ballInPossesion.SetParent(null); // desactiva del parent
        ballInPossesion = null;
    }
}
