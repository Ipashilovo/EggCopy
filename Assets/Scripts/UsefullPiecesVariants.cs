using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefullPiecesVariants : MonoBehaviour
{
    [SerializeField] private UsefullPieces[] _variants;

    public int[] GetUsefullPiecesArray(int number)
    {
        return _variants[number].Pieces;
    }
}

[System.Serializable]
public class UsefullPieces
{
    public int[] Pieces;
}
