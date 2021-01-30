using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilletDestroyParticle : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
