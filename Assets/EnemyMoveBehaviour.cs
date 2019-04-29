using System.Collections;
using System.Collections.Generic;
using _42.Events;
using UnityEngine;

public class EnemyMoveBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;
    public float Speed = 1;
    public Vector3 MaxVelocity ;
    public int Damage = 1;

bool isMoving = false;

    void Awake(){
        Game.Events.Register(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody>();
    }

void Update(){
    if(!isMoving) {
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if(distance < 20) isMoving=true;
    }
}
    // Update is called once per frame
    void LateUpdate()
    {
        if(isMoving)
        Move();
    }
    
    public void Move()
    {

//        var move = Vector3.MoveTowards(transform.position, _player.transform.position, Speed*Time.deltaTime).normalized;
        var move = (_player.transform.position - transform.position).normalized;
        // if (move.magnitude > 1f) move.Normalize();
       //  move = transform.InverseTransformDirection(move);
        
        if(_rb.velocity.magnitude < MaxVelocity.magnitude) {
            _rb.AddForce(move, ForceMode.VelocityChange);
        }

    }


    [EventListener(Assets.EventEnum.PlayerDied)]
    void OnPlayerDied () {
        enabled=false;
	}

    void OnDestroy(){
        Game.Events.Unregister(this);
    }


// reached the player
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.CompareTag("Player")){
            collision.transform.gameObject.SendMessage("Hit", Damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

}
