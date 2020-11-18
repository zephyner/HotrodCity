using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManage : MonoBehaviour
{
    public float throttle;
    public float steering;
    public bool li;
    public bool braking;

    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");

        li = Input.GetKeyDown(KeyCode.L);
        //braking = Input.GetKey(KeyCode.Space);
    }
}
