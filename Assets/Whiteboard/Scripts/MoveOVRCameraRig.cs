using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOVRCameraRig : MonoBehaviour
{
    public float movementSpeed = 1f; // A velocidade de movimento do OVRCameraRig

    private bool isMoving = false; // Verifica se o OVRCameraRig está se movendo
    private Vector3[] movementSequence; // Array para armazenar as posições do movimento sequencial
    private int currentMovementIndex = 0; // Índice atual do movimento sequencial

    private bool waitForSecondsFinished = false; // Verifica se o tempo de espera de 3 segundos foi concluído

    public Light directionalLight; // Referência à Directional Light
    public float duracaoTransicao = 3f; // Duração da transição em segundos
    public float intensidadeInicial = 0.75f; // Intensidade inicial da emissão
    public float intensidadeFinal = 0.5f; // Intensidade final da emissão

    void Start()
    {
        // Define o movimento sequencial
        movementSequence = new Vector3[]
        {
            new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.44f , transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.44f, transform.position.z - 4f),
            new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z - 4f)
        };

        StartCoroutine(WaitAndStartMovement(3f));
    }

    void Update()
    {
        if (isMoving)
        {
            // Verifica se o OVRCameraRig chegou ao destino
            if (Vector3.Distance(transform.position, movementSequence[currentMovementIndex]) <= 0.1f)
            {
                // Verifica se há mais movimentos no sequencial
                if (currentMovementIndex < movementSequence.Length - 1)
                {
                    // Atualiza o índice do movimento sequencial
                    currentMovementIndex++;
                }
                else
                {
                    // Para o movimento
                    isMoving = false;
                }
            }
            else
            {
                // Calcula a direção e a distância até o destino
                Vector3 direction = (movementSequence[currentMovementIndex] - transform.position).normalized;

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

    IEnumerator WaitAndStartMovement(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartMovement();
    }

    public void StartMovement()
    {
        // Inicia o movimento
        isMoving = true;
        currentMovementIndex = 0;
        StartCoroutine(DiminuirIntensidadeEmissaoCoroutine());
    }
}
