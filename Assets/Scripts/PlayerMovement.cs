using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float EngineForce, SteeringForce, BrakingForce;
    public WheelCollider FD_Wheel, FP_Wheel, RD_Wheel, RP_Wheel;
    public Rigidbody rb;

    void Start()
    {
      
    }
    void Update()
    {
        //WASD inputs
        float vertical = Input.GetAxis("Vertical") * EngineForce;
        float horizontal = Input.GetAxis("Horizontal") * SteeringForce;

        //rear wheel 
        RD_Wheel.motorTorque = vertical;
        RP_Wheel.motorTorque = vertical;

        //Front wheel
        FD_Wheel.steerAngle = horizontal;
        FP_Wheel.steerAngle = horizontal;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            RD_Wheel.brakeTorque = BrakingForce;
            RP_Wheel.brakeTorque = BrakingForce;
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RD_Wheel.brakeTorque = 0f;
            RP_Wheel.brakeTorque = 0f;
        }

    }
    private void FixedUpdate()
    {
    }
}

