using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAlphabetLetters : MonoBehaviour
{
    public GameObject[] alphabetLetterPrefabs;  // Array contendo os prefabs das letras do alfabeto
    private Vector3 initialPosition;            // A posição inicial para clonagem
    public float spacingX = 1.5f;               // Espaçamento entre as letras no eixo X
    public float offsetBetweenLetters = 0.2f;   // Deslocamento entre as letras no eixo X
    public Transform parentTransform;           // Objeto pai onde os clones serão posicionados


    void Start()
    {
        initialPosition = transform.position;
        CloneWordToPositions("JOÃO THIAGO");        // A palavra desejada deverá ser passada como parâmetro aqui.
    }

    void CloneWordToPositions(string word)
    {
        // Verifica se a string não está vazia ou nula
        if (!string.IsNullOrEmpty(word))
        {
            Vector3 currentPosition = initialPosition;

            // Itera por cada caractere na string
            foreach (char letterChar in word)
            {
                // Procura o prefab correspondente à letra atual na string
                GameObject letterPrefab = FindLetterPrefab(letterChar);

                // Se encontrar o prefab, clona o objeto da letra para a posição desejada
                if (letterPrefab != null)
                {
                    GameObject clonedLetter = Instantiate(letterPrefab, parentTransform);
                    Debug.Log("Instanciei a letra: " + letterChar);
                    // Atualiza a posição para a próxima letra, deslocada levemente à direita
                    clonedLetter.transform.position = currentPosition;
                    currentPosition.x -= spacingX + offsetBetweenLetters;
                    // clonedLetter.transform.localRotation = Quaternion.identity;
                    
                }
                else
                {
                    Debug.LogWarning("O prefab correspondente à letra '" + letterChar + "' não foi encontrado. Verifique se está presente no array alphabetLetterPrefabs.");
                }
            }
        }
        else
        {
            Debug.LogWarning("A string da palavra está vazia ou nula. Insira uma palavra válida para clonar as letras.");
        }
    }

    GameObject FindLetterPrefab(char letterChar)
    {
        // Itera pelo array de prefabs procurando o prefab correspondente à letra
        foreach (GameObject letterPrefab in alphabetLetterPrefabs)
        {
            LetterIdentifier letterIdentifier = letterPrefab.GetComponent<LetterIdentifier>();

            if (letterIdentifier != null && letterIdentifier.letter == letterChar)
            {
                Debug.Log("retornei do find a letra: " + letterChar);
                return letterPrefab;
            }
        }

        return null; // Caso não encontre o prefab correspondente à letra, retorna null
    }
}
