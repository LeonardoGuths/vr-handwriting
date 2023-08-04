using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDeactivate; // Objeto a ser desativado

    [SerializeField]
    private GameObject objectToActivate; // Objeto a ser ativado

    public void ActivateAndSwitchObjects()
    {
        StartCoroutine(SwitchObjectsWithDelay());
    }

    private System.Collections.IEnumerator SwitchObjectsWithDelay()
    {
        // Aguardar 3 segundos
        yield return new WaitForSeconds(3.0f);

        // Desativar o objeto de desativação
        objectToDeactivate.SetActive(false);


        // Ativar o objeto de ativação
        objectToActivate.SetActive(true);
    }
}