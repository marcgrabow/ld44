using System.Collections;
using System.Collections.Generic;
using _42.Events;
using Assets;
using Assets._42BYTES.Pool;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public StandardPool[] BulletPool;

    public float FireRate = 1f;

    public Transform SpawnPoint;

    public int WeaponId = -1;

    public int Bullets  = 5;
    public int MaxBullets = 5;

    public TextMeshProUGUI WeaponTitleUI;
    public HealthUiBehaviour BulletAmountUI;
    public GameObject MuzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        Game.Events.Register(this);
        foreach(var pool in BulletPool){
            pool.Prewarm(15);
        }
    }

    [EventListener(EventEnum.BoughtWeapon)]
    void OnBoughtWeapon(object weaponId){
        WeaponId = (int)weaponId;
        MaxBullets = 10 + (WeaponId*5);
        if(WeaponId==4) MaxBullets=10;
        BulletAmountUI.MaxPoints = MaxBullets;
        WeaponTitleUI.text = "Weapon "+(WeaponId+1);
        BulletAmountUI.gameObject.SetActive(true);
        WeaponTitleUI.gameObject.SetActive(true);
    }

    [EventListener(EventEnum.AmmoReload)]
    void OnAmmoReload(){
        Bullets = MaxBullets;
        BulletAmountUI.CurrentPoints = MaxBullets;
        BulletAmountUI.UpdateUi();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(WeaponId < 0 || Bullets <=0) return;

            var bullet = BulletPool[WeaponId].Spawn(SpawnPoint.position);

            Game.Events.Post(this, EventEnum.BulletFired);
            Bullets--;
            BulletAmountUI.CurrentPoints=Bullets;
            BulletAmountUI.UpdateUi();
            MuzzleFlash.SetActive(true);
            Invoke("MuzzleOff", 0.1f);

        }
    }

    void MuzzleOff(){
        MuzzleFlash.SetActive(false);
    }

}
