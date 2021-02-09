using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalleteColor : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    
    public Color GetColor(int number)
    {
        return number < _colors.Length ? _colors[number] : Color.black;
    }

    public int GetColorArrayLength()
    {
        return _colors.Length;
    }
}
