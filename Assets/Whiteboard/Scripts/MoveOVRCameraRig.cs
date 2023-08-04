using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//POSITION WHEN FINISH LEVEL 2 UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":-3.899909257888794,"y":0.7179999947547913,"z":5.1539998054504398},"rotation":{ "x":0.0,"y":1.0,"z":0.0,"w":0.0},"scale":{ "x":1.25,"y":1.25,"z":1.25} }

public class MoveOVRCameraRig : MonoBehaviour
{
    public float movementSpeed = 1f; // A velocidade de movimento do OVRCameraRig

    private bool isMoving1 = false;
    private bool isMoving2 = false;
    private Vector3[] movementSequence1; // Array para armazenar as posições do movimento sequencial 1
    private Vector3[] movementSequence2; // Array para armazenar as posições do movimento sequencial 2
    private int currentMovementIndex = 0; // Índice atual do movimento sequencial

    //private bool waitForSecondsFinished = false; // Verifica se o tempo de espera de 3 segundos foi concluído

    public Light directionalLight; // Referência à Directional Light
    public float duracaoTransicao = 3f; // Duração da transição em segundos
    public float intensidadeInicial = 0.75f; // Intensidade inicial da emissão
    public float intensidadeFinal = 0.5f; // Intensidade final da emissão

    void Start()
    {
        // Define o movimento sequencial
        movementSequence1 = new Vector3[]
        {
            new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.44f , transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.44f, transform.position.z - 4f),
            new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z - 4f)
        };

        movementSequence2 = new Vector3[]
        {
            new Vector3(-3.899909257888794f, 0.7179999947547913f, 5.1539998054504398f)
        };

        //StartCoroutine(WaitAndStartMovement(3f));
    }

    void Update()
    {
        if (isMoving1)
        {
            // Verifica se o OVRCameraRig chegou ao destino
            if (Vector3.Distance(transform.position, movementSequence1[currentMovementIndex]) <= 0.1f)
            {
                // Verifica se há mais movimentos no sequencial
                if (currentMovementIndex < movementSequence1.Length - 1)
                {
                    // Atualiza o índice do movimento sequencial
                    currentMovementIndex++;
                }
                else
                {
                    // Para o movimento
                    isMoving1 = false;
                }
            }
            else
            {
                // Calcula a direção e a distância até o destino
                Vector3 direction = (movementSequence1[currentMovementIndex] - transform.position).normalized;

                // Move o OVRCameraRig em direção ao destino com uma velocidade específica
                transform.position += direction * movementSpeed * Time.deltaTime;
            }
        }

        if (isMoving2)
        {
            // Verifica se o OVRCameraRig chegou ao destino
            if (Vector3.Distance(transform.position, movementSequence2[currentMovementIndex]) <= 0.1f)
            {
                // Verifica se há mais movimentos no sequencial
                if (currentMovementIndex < movementSequence2.Length - 1)
                {
                    // Atualiza o índice do movimento sequencial
                    currentMovementIndex++;
                }
                else
                {
                    // Para o movimento
                    isMoving2 = false;
                }
            }
            else
            {
                // Calcula a direção e a distância até o destino
                Vector3 direction = (movementSequence2[currentMovementIndex] - transform.position).normalized;

                // Move o OVRCameraRig em direção ao destino com uma velocidade específica
                transform.position += direction * movementSpeed * Time.deltaTime;
            }
        }
    }

    private IEnumerator DiminuirIntensidadeEmissaoCoroutine()
    {
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracaoTransicao)
        {
            // Calcula a intensidade atual usando a interpolação linear
            float intensidadeAtual = Mathf.Lerp(intensidadeInicial, intensidadeFinal, tempoDecorrido / duracaoTransicao);

            // Define a nova intensidade para a emissão
            directionalLight.intensity = intensidadeAtual;

            // Incrementa o tempo decorrido
            tempoDecorrido += Time.deltaTime;

            yield return null;
        }

        // Certifica-se de definir a intensidade final após o tempo de transição ter decorrido completamente
        directionalLight.intensity = intensidadeFinal;
    }

    //IEnumerator WaitAndStartMovement(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    StartMovement();
    //}

    public void StartMovement1()
    {
        // Inicia o movimento
        isMoving1 = true;
        currentMovementIndex = 0;
        StartCoroutine(DiminuirIntensidadeEmissaoCoroutine());
    }

    public void StartMovement2()
    {
        // Inicia o movimento
        isMoving2 = true;
        currentMovementIndex = 0;
    }
}
