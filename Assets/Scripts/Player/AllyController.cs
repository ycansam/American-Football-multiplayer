using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    [HideInInspector] public Transform allyInPosession;
    [SerializeField] private Transform playerShoulder;
    [SerializeField] private Collider playerTrigger;

    // variables locales cuando se ponga el multijugador
    [SerializeField] private bool isLocalPlayer;
    private void Update()
    {
        if (allyInPosession)
        {
            allyInPosession.position = playerShoulder.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == GameConstants.TAG_PLAYER && other.gameObject != gameObject)
        {
            if (Input.GetKeyDown(GameConstants.KEY_COLLECT) && isLocalPlayer)
            {
                if (!allyInPosession)
                    SetPlayerOnShoulder(other.transform);
                else
                    ReleasePlayerInPosession();
            }
        }
    }
    public void SetPlayerOnShoulder(Transform allyPlayer)
    {
        if (allyPlayer.tag == GameConstants.TAG_PLAYER)
        {
            // si el jugador entra en el trigger de la bola este la recoge
            allyInPosession = allyPlayer.transform;
            allyInPosession.rotation = playerShoulder.rotation;
            allyInPosession.SetParent(playerShoulder);
            allyInPosession.GetComponent<CharacterController>().enabled = false;
            allyInPosession.GetComponent<CharController>().isOnShoulder = true;

        }
    }

    // libera al jugador sin lanzarlo
    public void ReleasePlayerInPosession()
    {
        if (allyInPosession)
        {
            // si el jugador entra en el trigger de la bola este la recoge
            allyInPosession.SetParent(null);
            allyInPosession.GetComponent<CharacterController>().enabled = true;
            allyInPosession.GetComponent<CharController>().isOnShoulder = false;
            allyInPosession = null;
        }
    }

    public void AllyPlayerRemoveParent()
    {
        allyInPosession.SetParent(null); // desactiva del parent
        allyInPosession = null;
        StartCoroutine(DesactivateColliderBySeconds()); // desactiva el trigger del jugador durante 1 segundo
    }

    IEnumerator DesactivateColliderBySeconds()
    {
        playerTrigger.enabled = false;
        yield return new WaitForSeconds(1f);
        playerTrigger.enabled = true;
    }
}
