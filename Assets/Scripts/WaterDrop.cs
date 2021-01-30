using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _meshRenderer;
    private bool _isHeat = false;
    public bool IsHeat => _isHeat;
    public Rigidbody Rigidbody => _rigidbody;

    public void SetHeat()
    {
        _meshRenderer.material.color = Color.red;
        _isHeat = true;
    }
}
