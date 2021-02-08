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
        _meshRenderer.material.color = new Color(0.93f, 0.38f, 0.33f);
        _isHeat = true;
    }
}
