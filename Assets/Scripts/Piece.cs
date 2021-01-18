using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Piece : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private Skillet _skillet;
    private bool _isUseful = true;
    public void SetColor(Vector4 color)
    {
        _meshRenderer.material.color = color;
    }

    public void SetUseless()
    {
        _isUseful = false;
    }

    public void SetSkillet(Skillet skillet)
    {
        _skillet = skillet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WaterDrop>(out WaterDrop waterDrop))
        {
            Destroy(waterDrop.gameObject);
            if (_isUseful)
            {
                _skillet.OnUsefullPieceTriggered?.Invoke(_skillet);
                Destroy(_skillet.gameObject);
            }
            else
                _skillet.OnUslessPieceTriggered?.Invoke();
            
        }
    }
}


