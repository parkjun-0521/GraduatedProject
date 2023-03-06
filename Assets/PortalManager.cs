using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager ptmgr;

    [Header("Portals")]
    public Transform portal1;
    public Transform portal2;
    public Transform portal3;
    public Transform portal4;
    public Transform portal5;
    public Transform portal6;

    // Start is called before the first frame update
    public void Start()
    {
        if(PortalManager.ptmgr == null)
        {
            PortalManager.ptmgr = this;
        }
       
    }

    // Update is called once per frame
    public void Update()
    {      

    }
          
}
