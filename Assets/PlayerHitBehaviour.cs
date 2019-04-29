using System.Collections;
using System.Collections.Generic;
using _42.Events;
using Assets;
using UnityEngine;

public class PlayerHitBehaviour : MonoBehaviour
{

    public bool GodMode = false;

    public GameObject DeadPlayerPrefab;
    public Vector3 HitPushbackForce;
    private Rigidbody rb;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        Game.Events.Register(this);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(int damage)
    {
        if(GodMode)return;

        Game.Events.Post(this, EventEnum.PlayerLifeRemoved, damage);

        GetComponent<AudioSource>().Play();

        rb.AddForce(HitPushbackForce, ForceMode.Impulse);

    }


    [EventListener(EventEnum.PlayerDied)]
    public void Die()
    {
        if(dead)return;
        dead=true;
        //yield return new WaitForSeconds(0.25f);
        var corpse = Instantiate(DeadPlayerPrefab, transform.position+(Vector3.up*0.5f), DeadPlayerPrefab.transform.rotation);
        Destroy(gameObject);
        //yield return null;

        Game.Events.Post(this, EventEnum.PlayerCorpseSpawned, corpse.transform);

    }

}
