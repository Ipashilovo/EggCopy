using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] protected WaterDrop _waterDrop;
    [SerializeField] protected float _force = 3f;

    public virtual void Spawn()
    {
        WaterDrop newDrop = Instantiate(_waterDrop, transform.position, Quaternion.identity);
        newDrop.transform.Rotate(-90, 0, 0);
        newDrop.Rigidbody.AddForce(new Vector3(0, -_force, 0), ForceMode.Impulse);
    }
}
