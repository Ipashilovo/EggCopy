using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpownerButton : MonoBehaviour
{
    [SerializeField] private SpawnerHeatDrop _heatDropSpawner;
    [SerializeField] private DropSpawner _dropSpawner;
    [SerializeField] private float _spownCooldownMax = 0.2f;
    [SerializeField] private HeatViewValue _heatViewValue;
    private bool _isHeat = false;
    private DropSpawner _currentSpawner;
    private float _heatValue;
    private float _heatMax = 3f;
    private float _spownCooldown;

    private void Start()
    {
        _currentSpawner = _dropSpawner;
        _heatViewValue.SetMaxValue(_heatMax);
    }

    private void OnMouseDrag()
    {
        _heatViewValue.SetCurrentValue(_heatValue);
        if (!_isHeat)
        {
            _heatValue += Time.deltaTime;
            
            if (_heatValue >= _heatMax)
            {
                _isHeat = true;
                _currentSpawner = _heatDropSpawner;
                StartCoroutine(CoolHeat());
            }
        }

        if (_spownCooldown >= _spownCooldownMax)
        {
            _spownCooldown = 0f;
            _currentSpawner.Spawn();
        }
        else
            _spownCooldown += Time.deltaTime;
    }

    private void OnMouseUp()
    {
        if (!_isHeat)
        {
            StartCoroutine(CoolHeat());
        }
    }

    private void OnMouseDown()
    {
        _currentSpawner.Spawn();
    }

    private IEnumerator CoolHeat()
    {
        while (_heatValue > 0)
        {
            yield return new WaitForSeconds(0.05f);
            _heatValue -= 0.1f;
            _heatViewValue.SetCurrentValue(_heatValue);
        }
        _heatValue = 0;
        _currentSpawner = _dropSpawner;
        _isHeat = false;
    }
}
