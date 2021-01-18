using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    private LevelSetting _currentLevel;
    private int _speed = 0;
    private Vector3 _newPosition;

    private List<Skillet> _skillets;

    private void Start()
    {
        _newPosition = transform.position;
        _currentLevel = _levelGenerator.GetCurrentLevel();

        _speed = _currentLevel.Speed;
    }

    private void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
        transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);
    }

    public void SetNewPosition(Skillet skillet)
    {
        skillet.OnUsefullPieceTriggered -= SetNewPosition;
        _skillets.Remove(skillet);
        _newPosition += new Vector3(0, StaticFields.SkilletStep, 0);
    }

    public void SetSkillets(List<Skillet> skillets)
    {
        _skillets = skillets;

        foreach (Skillet skillet in _skillets)
            skillet.OnUsefullPieceTriggered += SetNewPosition;
    }
    
    private void OnDisable()
    {
        foreach (Skillet skillet in _skillets)
            skillet.OnUsefullPieceTriggered -= SetNewPosition;
    }
}
