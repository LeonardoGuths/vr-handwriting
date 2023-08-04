using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveABCInitial : MonoBehaviour
{
    public float movementSpeed = 1f; 

    private bool isMoving = false;
    private Vector3[] movementSequence; // Array para armazenar as posições do movimento sequencial
    private int currentMovementIndex = 0; // Índice atual do movimento sequencial

    
    void Start()
    {
        // Define o movimento sequencial
        movementSequence = new Vector3[]
        {
            new Vector3(-0.06499999761581421f, 7.057000160217285f, 0.47600001096725466f),
        };

        //StartCoroutine(WaitAndStartMovement(3f));
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

    public void StartMovement()
    {
        // Inicia o movimento
        isMoving = true;
        currentMovementIndex = 0;
    }
}

