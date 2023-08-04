using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//POSITION WHEN FINISH LEVEL 2 UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":-3.899909257888794,"y":0.7179999947547913,"z":5.1539998054504398},"rotation":{ "x":0.0,"y":1.0,"z":0.0,"w":0.0},"scale":{ "x":1.25,"y":1.25,"z":1.25} }
//POSITION WHEN LEVEL 3 UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-8.66100025177002,"y":0.7169440388679504,"z":2.750999927520752},"rotation":{"x":0.0,"y":1.0,"z":0.0,"w":0.0},"scale":{"x":1.25,"y":1.25,"z":1.25}}
//POSITION TO GO TO LEVEL 4 UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-3.8141133785247804,"y":0.7524818181991577,"z":2.9477956295013429},"rotation":{"x":0.0,"y":1.0,"z":0.0,"w":0.0},"scale":{"x":1.25,"y":1.25,"z":1.25}}
public class MoveOVRCameraRig : MonoBehaviour
{
    public float movementSpeed = 1f; // A velocidade de movimento do OVRCameraRig

    private bool isMoving1 = false;
    private bool isMoving2 = false;
    private bool isMoving3 = false;
    private bool isMoving4 = false;
    private Vector3[] movementSequence1; // Array para armazenar as posições do movimento sequencial 1
    private Vector3[] movementSequence2; // Array para armazenar as posições do movimento sequencial 2
    private Vector3[] movementSequence3; // Array para armazenar as posições do movimento sequencial 3
    private Vector3[] movementSequence4; // Array para armazenar as posições do movimento sequencial 4
    private int currentMovementIndex = 0; // Índice atual do movimento sequencial

    //private bool waitForSecondsFinished = false; // Verifica se o tempo de espera de 3 segundos foi concluído

    public Light directionalLight; // Referência à Directional Light
    public float duracaoTransicao = 3f; // Duração da transição em segundos
    public float intensidadeInicial = 0.75f; // Intensidade inicial da emissão
    public float intensidadeFinal = 0.5f; // Intensidade final da emissão

    void Start()
    {
        // movement1 = sai da cadeira e vai pro quadro
        movementSequence1 = new Vector3[]
        {
            new Vector3(transform.position.x, transform.position.y + 0.44f, transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.44f , transform.position.z),
            new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z - 4.1f),
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 4.1f)
        };

        // movement2 = vai pro painel de nextlevel no meio da sala
        movementSequence2 = new Vector3[]
        {
            new Vector3(-3.899909257888794f, 0.7179999947547913f, 5.1539998054504398f)
        };

        // movement3 = vai pra parede
        movementSequence3 = new Vector3[]
        {
            new Vector3(-8.66100025177002f, 0.7169440388679504f, 2.750999927520752f)
        };

        movementSequence4 = new Vector3[]
        {
            new Vector3(-3.8141133785247804f, 0.7524818181991577f, 2.9477956295013429f)
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

        if (isMoving3)
        {
            // Verifica se o OVRCameraRig chegou ao destino
            if (Vector3.Distance(transform.position, movementSequence3[currentMovementIndex]) <= 0.1f)
            {
                // Verifica se há mais movimentos no sequencial
                if (currentMovementIndex < movementSequence3.Length - 1)
                {
                    // Atualiza o índice do movimento sequencial
                    currentMovementIndex++;
                }
                else
                {
                    // Para o movimento
                    isMoving3 = false;
                }
            }
            else
            {
                // Calcula a direção e a distância até o destino
                Vector3 direction = (movementSequence3[currentMovementIndex] - transform.position).normalized;

                // Move o OVRCameraRig em direção ao destino com uma velocidade específica
                transform.position += direction * movementSpeed * Time.deltaTime;
            }
        }

        if (isMoving4)
        {
            // Verifica se o OVRCameraRig chegou ao destino
            if (Vector3.Distance(transform.position, movementSequence4[currentMovementIndex]) <= 0.1f)
            {
                // Verifica se há mais movimentos no sequencial
                if (currentMovementIndex < movementSequence4.Length - 1)
                {
                    // Atualiza o índice do movimento sequencial
                    currentMovementIndex++;
                }
                else
                {
                    // Para o movimento
                    isMoving4 = false;
                }
            }
            else
            {
                // Calcula a direção e a distância até o destino
                Vector3 direction = (movementSequence4[currentMovementIndex] - transform.position).normalized;

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

    public void StartMovement3()
    {
        // Inicia o movimento
        isMoving3 = true;
        currentMovementIndex = 0;
    }

    public void StartMovement4()
    {
        // Inicia o movimento
        isMoving4 = true;
        currentMovementIndex = 0;
    }
}
