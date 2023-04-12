using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Photon.Pun;

public class Customizing : MonoBehaviour
{
    public GameObject character_body;
    public Color[] color;
    [Header("Materials")]
    [Tooltip("Customizing yout character")]
    public Material[] body_mat;
    public Material hair_mat;
    public Material cloth_mat;

    public PhotonView PV;



    void Start()
    {
        character_body = this.gameObject;
        body_mat = character_body.GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat = body_mat[0];
        cloth_mat = body_mat[8];
        color[0] = new Color(150,0,0);  // red
        color[1] = new Color(0,20,150); // blue
        color[2] = new Color(200,200,80);  // gold
        color[3] = new Color(80,30,90);  // violet
        color[4] = new Color(100,100,100);  // gray
    }

    public void Update()
    {
        PV.RPC("customizing", RpcTarget.All);
    }

    [PunRPC]
    public void customizing()
    {
       /* if ()
        {
           
            {
               

            }
        }
        else
        {
            

        }*/
    }
}
