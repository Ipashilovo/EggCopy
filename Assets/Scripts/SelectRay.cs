using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectRay : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private Ray _ray;
    private Piece _currentPiece;
    private Vector3 _direction;

    private void Start()
    {
        _direction = new Vector3(0, -1, 0);
        
    }

    private void Update()
    {
        _ray = new Ray(transform.position, _direction);
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, _layerMask))
        {
            if (hit.collider.TryGetComponent<Piece> (out Piece piece))
            {
                if (piece != _currentPiece && _currentPiece != null)
                {
                    _currentPiece.Deselect();
                    _currentPiece = piece;
                    _currentPiece.Select();
                }
                else if (piece != _currentPiece && _currentPiece == null)
                {
                    _currentPiece = piece;
                    _currentPiece.Select();
                }
            }
        }
    }
}
