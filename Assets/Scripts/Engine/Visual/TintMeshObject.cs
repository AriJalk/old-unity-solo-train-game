using Engine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TintMeshObject : MonoBehaviour
{
    // TODO: use global tint
    static float GlobalTint = 0.5f;
    static float TintSpeed = 1f;
    static float MinTint = 0.2f;
    static float MaxTint = 1f;
    static bool _isIncreasing;

    private MeshRenderer _meshRenderer;
    private Material _originalMaterial;
    private Material _currentMaterial;
    private Color _color;


    
    private bool _isTinting;

    static public void UpdateGlobalTint()
    {
        float delta = Time.deltaTime * TintSpeed; ;
        if (_isIncreasing)
        {
            GlobalTint += delta;
            GlobalTint = Mathf.Clamp(GlobalTint, MinTint, MaxTint);
            if (GlobalTint == 1f)
            {
                _isIncreasing = false;
            }
        }
        else
        {
            GlobalTint -= delta;
            GlobalTint = Mathf.Clamp(GlobalTint, MinTint, MaxTint);
            if (GlobalTint == MinTint)
            {
                _isIncreasing = true;
            }
        }
    }
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
            _color.a = GlobalTint;
            _currentMaterial.color = _color;
        }
    }

    private void OnDestroy()
    {
        _meshRenderer.material = _originalMaterial;
    }
}
