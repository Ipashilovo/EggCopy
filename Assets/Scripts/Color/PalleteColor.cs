using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalleteColor : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    public Color GetCoolor(int number)
    {
        return number < _colors.Length ? _colors[number] : Color.black;
    }
}
