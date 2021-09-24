using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlayer : MonoBehaviour
{
    [Header("Componentes")]
    private AllyController allyController;
    [Header("Posiciones de Lanzamiento")]
    [SerializeField] private Transform throwPosition;
    [Header("Variables")]
    [SerializeField] private float minCharge = 5f; // usado para la carga minima
    [SerializeField] private float maxCharge = 30f; //  maxima carga
    [SerializeField] private float timeToGetMaxCharge = 1.5f;
    private float charge; // carga

    private void OnGUI()
    {
        float value = minCharge + (charge / timeToGetMaxCharge) * (maxCharge - minCharge);
        GUI.Label(new Rect(0, 200, 150, 150), "ThrowValue: " + value);
        value = 0;
    }
    void Start()
    {
        allyController = GetComponent<AllyController>();
    }

   
    public bool ChargeThrow()
    {
        if (charge < timeToGetMaxCharge)
        {
            charge += Time.deltaTime;
        }
        return true;
    }

    public void ThrowWithCharge()
    {
        if (allyController.allyInPosession)
        {
            float throwValue = minCharge + (charge / timeToGetMaxCharge) * ((maxCharge - minCharge));
            allyController.allyInPosession.GetComponentInChildren<CharRagdoll>().AddImpact(throwPosition.forward, throwValue);
            allyController.allyInPosession.GetComponentInChildren<CharController>().isFlying = true;
            allyController.ReleasePlayerInPosession();
            charge = 0; // reseting the timer
        }
    }
}
