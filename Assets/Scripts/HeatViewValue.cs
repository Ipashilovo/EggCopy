using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatViewValue : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float _maxValue;

    private void Start()
    {
        _image.fillAmount = 0;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }

    public void SetCurrentValue(float value)
    {
        _image.fillAmount = value / _maxValue;
    }
}
