using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_TriggerMediaVuelta : MonoBehaviour
{
    CharacterController pCC;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instanceGameManager.SetIsPauseGame(true);

        pCC = other.GetComponent<CharacterController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (pCC != null)
        {
            //Play audio: "Por aca no es la escuela"
            //Show text: "Por aca no es la escuela"

            pCC.Move(gameObject.transform.forward * 0.005f);
            other.transform.rotation = gameObject.transform.rotation;

            //capaz que un cooldown para el dialogo y el texto sino re molesto

            Debug.Log("por aca no la concha de tu madre. Esta re feo esto, pero bueno algo habia que mostrar.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.instanceGameManager.SetIsPauseGame(false);
    }
}
