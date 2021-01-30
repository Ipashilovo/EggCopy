using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHeatDrop : DropSpawner
{
    public override void Spawn()
    {
        WaterDrop newDrop = Instantiate(_waterDrop, transform.position, Quaternion.identity);
        newDrop.transform.Rotate(-90, 0, 0);
        newDrop.Rigidbody.AddForce(new Vector3(0, -_force, 0), ForceMode.Impulse);
        newDrop.SetHeat();
    }
}
