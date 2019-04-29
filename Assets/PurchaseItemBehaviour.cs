using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets;

public class PurchaseItemBehaviour : MonoBehaviour
{

    public int AgeCost = 0;
    public int AgeGain = 0;
    private bool triggered;

    public GameObject Text;

    public int WeaponId = -1;
    public bool IsAmmo;

    public string TextEvent;
    private GameObject player;

    void Awake(){
        Text.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){

        if(!Text.activeInHierarchy) {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance < 10) Text.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(triggered)return;
        if(!other.CompareTag("Player"))return;
        triggered=  true;
        
        if(AgeCost>0){
            Game.Events.Post(this, EventEnum.PlayerLifeRemoved, AgeCost);
        }
        if(AgeGain>0){
            Game.Events.Post(this, EventEnum.PlayerGainedLife, AgeGain);
        }

        transform.DORotate(Vector3.zero, 0.5f);
        GetComponentInChildren<SpriteRenderer>().DOFade(0.5f, 0.5f);
        
        //Debug.Log(other.name);
        Destroy(Text);

        if(WeaponId>-1){
            Game.Events.Post(this, EventEnum.BoughtWeapon, WeaponId);
        }
        if(IsAmmo){
            Game.Events.Post(this, EventEnum.AmmoReload);
        }

        if(!string.IsNullOrWhiteSpace(TextEvent)){
            Game.Events.Post(this, EventEnum.TextEvent, TextEvent);
        }

        
        GetComponent<AudioSource>().Play();
        
        
    }
}
