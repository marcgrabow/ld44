using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    float m_ForwardAmount;
    private Rigidbody rb;

public float MaxInput = 0.5f;
public Vector3 MaxVelocity ;
public int LastFullZ = 0;

public float ZmodifierForAge = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow)){
            //some punishment
            Game.Events.Post(this, EventEnum.PlayerLifeRemoved, 1);
            Game.Events.Post(this, EventEnum.TextEvent, "lost 1 year by thinking about the past");
        }
        var newZ = Mathf.CeilToInt(transform.position.z*ZmodifierForAge);
        if(newZ>LastFullZ){
            Game.Events.Post(this, Assets.EventEnum.PlayerAged, 1);
            
            LastFullZ=newZ;

            if(newZ % 10 == 0){
                Game.Events.Post(this, Assets.EventEnum.TextEvent, newZ + "th birthday!");
            }

        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = Mathf.Clamp(CrossPlatformInputManager.GetAxis("Horizontal"), MaxInput*-1, MaxInput);
        float v = Mathf.Clamp(CrossPlatformInputManager.GetAxis("Vertical"), 0, MaxInput);
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character

        // we use world-relative directions in the case of no main camera
        var m_Move = v * Vector3.forward + h * Vector3.right;

#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }


    public void Move(Vector3 move, bool crouch, bool jump)
    {

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        m_ForwardAmount = move.z;

        if(this.rb.velocity.magnitude < MaxVelocity.magnitude) {
            this.rb.AddForce(move, ForceMode.VelocityChange);
        }


    }

}
