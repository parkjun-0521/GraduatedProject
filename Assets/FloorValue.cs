using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorValue : MonoBehaviour
{
    public TMP_Text ftext;

    Elevator elevator;
    
    void Start()
    {
        elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        ftext.text = "1F"; 
    }

    void Update()
    {
        floortext();   
    }

    public void floortext()
    {
        if (elevator.floorvalue == 1)
        {
            ftext.text = "1F";
        }
        else if(elevator.floorvalue == 2)
        {
            ftext.text = "2F";
        }
    }
}
