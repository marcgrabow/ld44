using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTextBehaviour : MonoBehaviour
{
    public float FreezeDelay = 2;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(FreezeDelay);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;    
    }

}
