using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManage : MonoBehaviour
{
    public List<Light> lights;
    //public List<Light> backLight;

    public virtual void HeadLights()
    {
        foreach(Light light in lights)
        {
            light.intensity = light.intensity == 0 ? 2 : 0;
        }
    }

    /*public virtual void BackLight()
    {
        foreach(Light backLi in backLight)
        {
            backLi.intensity = backLi.intensity == 0 ? 2 : 0;
        }
    }*/
}
