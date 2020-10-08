using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Seek : MonoBehaviour
{
    public GameObject target_;
    public GameObject go_;

    int speed_ = 10;
    float roationspeed = 15;
    float slowingradius;
    float distance;
    Vector3 closestSurfacePoint1;
    Vector3 closestSurfacePoint2;
    public Collider coll;
    public Collider coll2;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        

        coll = go_.GetComponentInChildren<Collider>();
        coll2 = target_.GetComponent<Collider>();
        slowingradius = 20f;

        closestSurfacePoint1 = coll.ClosestPointOnBounds(target_.transform.position);
        closestSurfacePoint2 = coll2.ClosestPointOnBounds(go_.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        seekTarget(go_.transform, target_.transform, speed_, roationspeed, slowingradius);
        Debug.Log(distance);
    }

    public void seekTarget(Transform go_,Transform target_,int speed_, float rotationspeed_, float slowingradius_)
    {
        //distnace between A and B
        distance = Mathf.Abs(Vector3.Distance(closestSurfacePoint1, closestSurfacePoint2));
        //inside the radius
        if (distance < slowingradius)
        {
            direction = (target_.position - go_.position).normalized;
            go_.position = Vector3.MoveTowards(go_.position, target_.position, speed_ * Time.deltaTime * (distance/slowingradius_));
        }
        //outside our slowing radius 
        else
        {
            direction = (target_.position - go_.position).normalized;
            go_.position = Vector3.MoveTowards(go_.position, target_.position, speed_ * Time.deltaTime);
        }
        //Rotate to the target
        go_.rotation = Quaternion.RotateTowards(go_.rotation, Quaternion.LookRotation(direction), rotationspeed_ * Time.deltaTime);
    }
}

