using Assets._42BYTES.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    public AbstractPool Pool;
    public Transform SpawnPos;

	public void Spawn () {
        Pool.Spawn(SpawnPos.transform.position, SpawnPos.rotation);
	}
	
}
