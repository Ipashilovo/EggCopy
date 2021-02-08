using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    private LevelSetting _currentLevel;
    private int _AngleSpeed = 0;
    private float _speed;
    private Vector3 _newPosition;

    private List<Skillet> _skillets;

    private void Start()
    {
        _newPosition = transform.position;
        _currentLevel = _levelGenerator.GetCurrentLevel();

        _AngleSpeed = _currentLevel.Speed;
    }

    private void Update()
    {
        transform.Rotate(0, -_AngleSpeed * Time.deltaTime, 0);
        transform.position = Vector3.Lerp(transform.position, _newPosition, Math.Abs(_speed));
        _speed += Time.deltaTime / 3;
    }

    public void SetNewPosition(Skillet skillet)
    {
        _speed = 0;
        skillet.OnUsefullPieceTriggered -= SetNewPosition;
        _skillets.Remove(skillet);
        _newPosition += new Vector3(0, StaticFields.SkilletStep, 0);
    }

    public void SetSkillets(List<Skillet> skillets)
    {
        _skillets = skillets;

        foreach (Skillet skillet in _skillets)
        {
            skillet.OnUsefullPieceTriggered += SetNewPosition;
        }
    }
    
    private void OnDisable()
    {
        foreach (Skillet skillet in _skillets)
        {
            skillet.OnUsefullPieceTriggered -= SetNewPosition;
        }
    }

    public void RemoveSpeed()
    {
        _AngleSpeed = 0;
    }
}
