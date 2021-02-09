using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private PalleteColor _pallete;
    [SerializeField] private MeshRenderer _renderer;

    private void Start()
    {
        _renderer.material.color = _pallete.GetColor(_number);
    }
}