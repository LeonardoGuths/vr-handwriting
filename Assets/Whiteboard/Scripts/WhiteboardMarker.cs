using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(AudioSource))]

public class WhiteboardMarker : MonoBehaviour
{
    [Header("Pen Settings")]
    [SerializeField] private Transform _tip;
    [Range(1, 50)]
    [SerializeField] private int _penSize = 10;
    [Range(0.001f, 0.5f)]
    [SerializeField] private float _interpolationLevel = 0.1f;

    [Header("Points Settings")]
    [SerializeField] private Transform _pointsParent;
    [Range(0.05f, 0.5f)]
    [SerializeField] private float _pointsTreshold = 0.15f;

    [Header("Whiteboard Settings")]
    [SerializeField] private float _moveDuration = 1f; // Duração da transição em segundos
    [SerializeField] private GameObject _whiteboardObject;

    [Header("Success Settings")]
    [SerializeField] private ParticleSystem _particlesSuccess1;
    [SerializeField] private ParticleSystem _particlesSuccess2;
    [SerializeField] private ParticleSystem _particlesSuccess3;
    [SerializeField] private ParticleSystem _particlesSuccess4;
    AudioSource audioData;



    [Header("MoveOVRCameraRig Settings")]
    [SerializeField] private MoveOVRCameraRig _moveCameraScript;

    private Vector3 _initialPosition; // Posição inicial do quadro
    private Vector3 _targetPosition; // Posição final do quadro
    private float _moveTimer = 10f; // Cronômetro para controlar o tempo da transição

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;
    private List<Transform> _sequencePoints = new List<Transform>();
    private int _currentPointIndex = 0;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;
    private Color[] _penColors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow
    };

    private void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;

        // if (_pointsParent != null)
        // {
        //     _sequencePoints = new List<Transform>(_pointsParent.GetComponentsInChildren<Transform>());
        //     _sequencePoints.Remove(_pointsParent);
        // }

        StartCoroutine(StartWithDelay(1.0f));

        _initialPosition = _whiteboardObject.transform.position;
        Debug.Log("posicao inicial no start: " + _initialPosition);

        audioData = GetComponent<AudioSource>();
    }

    IEnumerator StartWithDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        GetPointsInChildren(_pointsParent);
    }

     void GetPointsInChildren(Transform parent)
    {
        // Percorre todos os filhos do objeto pai (letras)
        foreach (Transform letter in parent)
        {
            // Percorre todos os filhos dos filhos do objeto pai (pontos)
            foreach (Transform point in letter)
            {
                // Adiciona o ponto atual à lista de pontos
                _sequencePoints.Add(point);
                Debug.Log("Adicionei o ponto " + point);
            }
        }
        _sequencePoints[0].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        CheckPointCollision();

        // função abaixo reseta posição da caneta sempre que passar da altura máxima permitida, voltando pra região permitida \/ (desabilitado)
        
        //var maxY = Mathf.Max(_tip.transform.position.y, _whiteboardObject.transform.position.y); // Obter a altura máxima permitida
        //Verificar se a altura atual é menor que a altura máxima permitida
        //    if (_tip.transform.position.y < maxY)
        //{
        //    Definir a posição da caneta na altura máxima permitida
        //   var newPosition = transform.position;
        //    Debug.Log("newposy 1: " + newPosition.y);
        //    newPosition.y = (float)maxY + 0.1025f;
        //    Debug.Log("newposy 2: " + newPosition.y);
        //    transform.position = newPosition;
        //}

        Draw();

    
        // Verificar se está em transição
        if (_moveTimer < _moveDuration)
        {
            _moveTimer += Time.deltaTime;

            // Calcular a interpolação linear entre a posição inicial e final
            float t = Mathf.Clamp01(_moveTimer / _moveDuration);
            _whiteboardObject.transform.position = Vector3.Lerp(_initialPosition, _targetPosition, t);
        }
    }

    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                    _renderer.material.color = GetNextPenColor();
                    _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                // if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                // {
                //     return;
                // }

                

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += _interpolationLevel)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);

                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }



    IEnumerator ResetMoveTimerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _moveTimer = 0f;
    }

    IEnumerator StartSuccessWithDelay()
    {
        yield return new WaitForSeconds(3f);
        _particlesSuccess1.Play();
        _particlesSuccess2.Play();
        _particlesSuccess3.Play();
        _particlesSuccess4.Play();
    }

    private void CheckPointCollision()
    {
        // Transform currentPoint = _sequencePoints[_currentPointIndex];
        // Transform nextPoint = null;
        if (_currentPointIndex >= _sequencePoints.Count)
        {
            if (_sequencePoints.Count > 1)
            {
                 _sequencePoints[_currentPointIndex-1].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                // Todos os pontos da sequência foram atravessados
                Debug.Log("Sequencia completada em ordem!");
                _currentPointIndex = 0;

                audioData.Play(0);
                
                _moveCameraScript.StartMovement2();

                StartCoroutine(StartSuccessWithDelay());

                //// Mover o quadro para cima suavemente
                //_targetPosition = _whiteboardObject.transform.position + Vector3.up * 0.25f;
                //_initialPosition = _whiteboardObject.transform.position;
                //StartCoroutine(ResetMoveTimerCoroutine());
            }
            return;
        }

        // Transform currentPoint = _sequencePoints[_currentPointIndex];
        // if (_currentPointIndex < (_sequencePoints.Count-1))
        // {
        //     nextPoint = _sequencePoints[_currentPointIndex+1];
        //     Debug.Log("SETOU NEXTPOINT PRA: " + (_currentPointIndex+1) + " E O TOTAL É: " + _sequencePoints.Count);
        // }
            

        if (_sequencePoints[_currentPointIndex].GetComponent<Renderer>().material.HasProperty("_Color"))
        {
            float distance = Vector3.Distance(_tip.position, _sequencePoints[_currentPointIndex].position);
            if (distance <= _pointsTreshold / 10)
            {
                // A ponta da caneta colidiu com o ponto atual da sequência
                _sequencePoints[_currentPointIndex].GetComponent<Renderer>().material.color = _renderer.material.color;
                // Faça aqui o que for necessário quando a ponta da caneta colidir com o ponto
                if (_sequencePoints[_currentPointIndex].GetComponent<Renderer>().material.HasProperty("_EmissionColor"))
                {
                    Color originalEmissionColor = _sequencePoints[_currentPointIndex].GetComponent<Renderer>().material.GetColor("_EmissionColor");
                    // Color newEmissionColor = new Color(1f, 1f, 1f); // Cor de emissão desejada

                    _sequencePoints[_currentPointIndex].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    if (_currentPointIndex < (_sequencePoints.Count-1))
                    {  
                        _sequencePoints[_currentPointIndex+1].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    }
                    // currentPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", newEmissionColor);
                }
                

                _currentPointIndex++; // Avança para o próximo ponto na sequência
                Debug.Log("Ponto atravessado, proximo: " + _currentPointIndex);
            }
        }
    }

    private Color GetNextPenColor()
    {
        Color currentColor = _renderer.material.color;
        int colorIndex = Array.IndexOf(_penColors, currentColor);
        int nextColorIndex = (colorIndex + 1) % _penColors.Length;
        return _penColors[nextColorIndex];
    }
}
