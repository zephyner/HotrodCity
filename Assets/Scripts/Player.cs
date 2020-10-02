using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    
        void Start()
    {
        
    }

    [Client]
    void Update()
    {
        // has authority currently checked in networktransform on PCar prefab
        if (!hasAuthority)
        {
            return;
        }
    }
}
