using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletHitBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<Light>().DOIntensity(0, 0.5f);
        Destroy(this, 0.6f);
    }

}
