﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public void GetInput()
    {
        //A and D keys
        horizontalInput = Input.GetAxis("Horizontal");
        //W and S keys
        frontDriverWhee.steerAngle = steeringAng;
        frontPassengerWhee.steerAngle = steeringAng; 
    }

    private void Acceleration()
    {
        frontDriverWhee.motorTorque = verticalInput * motorForce;
        frontPassengerWhee.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverWhee, frontDriverTran);
        UpdateWheelPose(frontPassengerWhee, frontPassengerTran);
        UpdateWheelPose(rearDriverWhee, rearDriverTran);
        UpdateWheelPose(rearPassengerWhee, rearPassengerTran);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        //returns the values on its own
        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steering();
        Acceleration();
        UpdateWheelPoses();
    }

    private float horizontalInput;
    private float verticalInput;
    //steering will work with the horizontalInput
    private float steeringAng;

    public WheelCollider frontDriverWhee, frontPassengerWhee;
    public WheelCollider rearDriverWhee, rearPassengerWhee;
    //Modifies the rotation of the wheels
    public Transform frontDriverTran, frontPassengerTran;
    public Transform rearDriverTran, rearPassengerTran;

    //How fast the car can U-Turn
    public float maxSteerAngle = 30;
    //How fast the car can go
    public float motorForce = 50;
}