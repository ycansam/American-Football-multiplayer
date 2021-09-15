using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] float scaleXmax = 0.8f;
    [SerializeField] float scaleYmax = 0.5f;
    [SerializeField] float scaleXmin = 0;
    [SerializeField] float scaleYmin = 0;
    [SerializeField] float timerShieldOpenClose = 1f;
    [SerializeField] Collider playerCollider;
    private bool shieldOppened;
    float timer = 0;
    private void Start()
    {
        Physics.IgnoreCollision(playerCollider, GetComponent<Collider>());
    }
    private void Update()
    {
        CheckShieldOpenClosed();
    }
    private void CheckShieldOpenClosed()
    {
        // abriendo shield
        if (shieldOppened)
        {
            // mientras la escala sea menor a la maxima
            if (transform.localScale.x < scaleXmax && transform.localScale.y < scaleYmax)
            {
                timer += Time.deltaTime;
                transform.localScale = new Vector3((timer / timerShieldOpenClose) * scaleXmax, (timer / timerShieldOpenClose) * scaleYmax, transform.localScale.z);
            }
            else if (timer != timerShieldOpenClose)
            {
                // si ha llegado al maximo se prepara para cuando se va a cerrar
                timer = timerShieldOpenClose;
                transform.localScale = new Vector3(scaleXmax, scaleYmax, transform.localScale.z);
            }
        }
        else // cerrando shield
        {
            // mientras la escala sea mayor al minimo
            if (transform.localScale.x > scaleXmin && transform.localScale.y > scaleYmin)
            {
                timer -= Time.deltaTime;
                transform.localScale = new Vector3((timer / timerShieldOpenClose) * scaleXmax, (timer / timerShieldOpenClose) * scaleYmax, transform.localScale.z);
            }
            else if (timer != 0)
            {
                // se resetea y se acaba de cerrar.
                timer = 0;
                transform.localScale = new Vector3(scaleXmin, scaleYmin, transform.localScale.z);
                gameObject.SetActive(false);
            }
        }
    }
    public void OpenShield()
    {
        shieldOppened = true;
        gameObject.SetActive(shieldOppened);
        // StartCoroutine(OpenShieldTransition());
    }
    public void CloseShield()
    {
        shieldOppened = false;
    }
    public bool GetOpenShield()
    {
        return shieldOppened;
    }
}
