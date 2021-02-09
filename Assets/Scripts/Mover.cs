using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, ISpawnLookerReaction
{
    [SerializeField] private SkilletLooker _skilletLooker;
    [SerializeField] private LevelGenerator _levelGenerator;
    private LevelSetting _currentLevel;
    private int _AngleSpeed = 0;
    private float _speed;
    private Vector3 _newPosition;

    private void Start()
    {
        _skilletLooker.SetNewLookerReaction(this);
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

    public void InitValue(int value){}

    public void OnLevelLoose()
    {
        RemoveSpeed();
    }
    
    public void OnChangeValue()
    {
        SetNewPosition();
    }
    
    public void OnLevelWin()
    {
        Destroy(this.gameObject);
    }

    private void SetNewPosition()
    {
        _speed = 0;
        _newPosition += new Vector3(0, StaticFields.SkilletStep, 0);
    }

    private void RemoveSpeed()
    {
        _AngleSpeed = 0;
    }
}
