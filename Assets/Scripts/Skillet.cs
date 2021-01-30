using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skillet : MonoBehaviour
{
    [SerializeField] private Piece[] _pieces;
    [SerializeField] private SkilletDestroyParticle _particle;
    private Color _color;
    private int[] _usefullPieces;

    public UnityAction<Skillet> OnUsefullPieceTriggered;
    public UnityAction OnUslessPieceTriggered;

    private void Start()
    {
        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].SetColor(_color);
            _pieces[i].SetSkillet(this);
        }

        for (int i = 0; i < _usefullPieces.Length; i++)
        {
            _pieces[_usefullPieces[i]].SetColor(Color.black);
            _pieces[_usefullPieces[i]].SetUseless();
        }
    }


    public void SetColor(Color color)
    {
        _color = color;
    }

    public void SetUsefullPieces(int[] numbers)
    {
        _usefullPieces = numbers;
    }


    public void PlayParticle()
    {
        var newParticle = Instantiate(_particle, transform.position,  Quaternion.Euler(-90,0,0));
    }
}

