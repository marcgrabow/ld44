using System.Collections;
using System.Collections.Generic;
using _42.Events;
using UnityEngine;
using DG.Tweening;

public class SimpleCamFollow : MonoBehaviour
{

    public Transform target;
    public float MaxDistanceDelta = 1;
    public Vector3 Offset;
    private bool follow=true;
    private Vector3 startPos;

    void Awake(){
        startPos=transform.position;
        Game.Events.Register(this);
    }

    void LateUpdate()
    {
        if(!follow) return;
        transform.position = Vector3.MoveTowards(transform.position, Offset+ target.transform.position, MaxDistanceDelta*Time.deltaTime);
    }

    
    [EventListener(Assets.EventEnum.PlayerDied)]
    void OnPlayerDied () {
        
        Offset /= 2;

        Invoke("ZoomOut", 5);
	}

    [EventListener(Assets.EventEnum.PlayerCorpseSpawned)]
    void OnCorpse(object corpseTransform){
        target = (Transform)corpseTransform;
    }

    void ZoomOut(){
        follow=false;
        transform.DOMove(startPos, 30);
    }


}
