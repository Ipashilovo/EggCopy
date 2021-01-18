using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorArray : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    
    public Color GetColor(int number)
    {
        return _colors[number];
    }
}
