using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRoll : MonoBehaviour
{
    public WheelCollider Left;
    public WheelCollider Right;
    private Rigidbody carBody;

    public float antiRoll = 5000.0f;

    // Start is called before the first frame update
    void Start()
    {
        carBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WheelHit hit = new WheelHit();
        float travelLeft = 1.0f;
        float travelRight = 1.0f;

        bool groundedLeft = Left.GetGroundHit(out hit);
        if (groundedLeft)
        {
            travelLeft = (-Left.transform.InverseTransformDirection(hit.point).y - Left.radius) / Left.suspensionDistance;
        }

        bool groundedRight = Right.GetGroundHit(out hit);
        if (groundedRight)
        {
            travelRight = (-Right.transform.InverseTransformDirection(hit.point).y - Right.radius) / Right.suspensionDistance;
        }

        var antiRforce = (travelLeft - travelRight) * antiRoll;

        if (groundedLeft)
        {
            carBody.AddForceAtPosition(Left.transform.up * -antiRforce, Left.transform.position);
        }
        if (groundedRight)
        {
            carBody.AddForceAtPosition(Right.transform.up * antiRforce, Right.transform.position);
        }
    }
}
