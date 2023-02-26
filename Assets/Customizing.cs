using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Customizing : MonoBehaviour
{
    [Header("Hair Materials")]
    [Tooltip("Customizing hair color")]
    public Material[] hairMt;


    // color
    private Color[] color = new Color[9];
    private int colors = 0;


    // Start is called before the first frame update
    void Start()
    {
     //   if()
     //  hairMt = this.
    }

    // Update is called once per frame
    void Update()
    {
      //  customizing();
    }

   /* public void customizing()
    {
        if (colors <= 8)
        {
            hairMt[0].color = color[++colors];
            if (hairs2)
            {
                hairs2.color = color[++colors];

            }
        }
        else
        {
            colors = 0;

        }
    }*/
}
