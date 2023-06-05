using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParentMaterial : MonoBehaviour
{
    [SerializeField] private Renderer _parentRenderer;
    private Renderer _childRenderer;

    void Start()
    {
        _childRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_parentRenderer != null && _parentRenderer.sharedMaterial != _childRenderer.sharedMaterial)
        {
            _childRenderer.sharedMaterial = _parentRenderer.sharedMaterial;
        }
    }
}