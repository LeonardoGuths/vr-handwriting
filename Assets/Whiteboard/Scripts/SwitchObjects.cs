using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDeactivate; // Objeto a ser desativado

    [SerializeField]
    private GameObject objectToActivate; // Objeto a ser ativado

    [SerializeField]
    private float delayTime;

    public void ActivateAndSwitchObjects()
    {
        StartCoroutine(SwitchObjectsWithDelay());
    }

    public void ActivateAndSwitchObjectsDelayBetween()
    {
        StartCoroutine(SwitchObjectsWithDelayBetween());
    }

    private System.Collections.IEnumerator SwitchObjectsWithDelay()
    {
        // Aguardar segundos
        yield return new WaitForSeconds(delayTime);

        // Desativar o objeto de desativação
        objectToDeactivate.SetActive(false);


        // Ativar o objeto de ativação
        objectToActivate.SetActive(true);
    }

    private System.Collections.IEnumerator SwitchObjectsWithDelayBetween()
    {

        // Desativar o objeto de desativação
        objectToDeactivate.SetActive(false);
        Debug.Log("deactivated and lets wait");

        // Aguardar segundos
        yield return new WaitForSeconds(delayTime);

        // Ativar o objeto de ativação
        objectToActivate.SetActive(true);
        Debug.Log("activated and waited");

    }
}