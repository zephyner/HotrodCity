using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManage))]
[RequireComponent(typeof(LightManage))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public InputManage inMan;
    public LightManage LiMan;
    public List<WheelCollider> throttWheels;
    public List<GameObject> steerWheels;
    public List<GameObject> meshes;
    public Rigidbody rb;
    public Transform massCent;

    public float brakePower;
    public float strengthCoefficient = 20000f;
    public float maxTurning = 20f;
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        inMan = GetComponent<InputManage>();
    }

    void Update()
    {
        if (inMan.li)
        {
            LiMan.HeadLights();
            //LiMan.BackLight();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            strength = strengthCoefficient * 10f;
        }
        else
        {
            strength = strengthCoefficient;
        }

        //Car movement and braking
        foreach (WheelCollider wheel in throttWheels)
        {
            if (inMan.braking)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakePower * Time.deltaTime;
            }
            else 
            { 
                wheel.motorTorque = strength * Time.deltaTime * inMan.throttle;
                wheel.brakeTorque = 0f;
            }

        }

        //Wheel turning
        foreach (GameObject wheel in steerWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurning * inMan.steering;
            wheel.transform.localEulerAngles = new Vector3(0f, inMan.steering * maxTurning, 0f);
        }

        //Wheel rotate
        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.17f), 0f, 0f);
        }
    }
}
