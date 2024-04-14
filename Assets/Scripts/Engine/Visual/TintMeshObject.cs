using Engine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TintMeshObject : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 2f)]
    private float _tintSpeed = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float _minTint = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    private float _maxTint = 1f;

    private MeshRenderer _meshRenderer;
    private Material _originalMaterial;
    private Material _currentMaterial;
    private Color _color;


    private bool _isIncreasing;
    private bool _isTinting;
    // Start is called before the first frame update
    void Start()
    {

        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMaterial = _meshRenderer.material;
        _color = _originalMaterial.color;
        // Set material to transperant
        _meshRenderer.material = ServiceLocator.MaterialManager.GetColorMaterial(Enums.GameColor.Transperant);
        // Apply same color
        _currentMaterial = _meshRenderer.material;
        _currentMaterial.color = _color;
        _isTinting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTinting)
        {
            float delta = Time.deltaTime * _tintSpeed; ;
            if (_isIncreasing)
            {
                _color.a += delta;
                _color.a = Mathf.Clamp(_color.a, _minTint, _maxTint);
                if (_color.a == 1f)
                {
                    _isIncreasing = false;
                }
            }
            else
            {
                _color.a -= delta;
                _color.a = Mathf.Clamp(_color.a, _minTint, _maxTint);
                if (_color.a == _minTint)
                {
                    _isIncreasing = true;
                }
            }
            _currentMaterial.color = _color;
        }
    }

    private void OnDestroy()
    {
        _meshRenderer.material = _originalMaterial;
    }
}
