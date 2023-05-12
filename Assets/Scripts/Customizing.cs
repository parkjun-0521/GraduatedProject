using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ProBuilder.Shapes;
using Photon.Pun;

public class Customizing : MonoBehaviour
{
    public GameObject character_body;
    private Color[] color = new Color[5];
    [Header("Materials")]
    [Tooltip("Customizing your character")]
    private Material[] body_mat;
    private Material hair_mat;
    private Material hair_mat2;
    public Material cloth_mat;
    public Texture[] cloth = new Texture[3];

    public PhotonView PV;



    void Start()
    {
        character_body = this.gameObject;
        body_mat = character_body.GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat = body_mat[0];
        hair_mat2 = body_mat[7];
        // cloth_mat = Instantiate(body_mat[8]);
        cloth_mat = body_mat[8];


        color[0] = new Color(150 / 255f, 0f, 0f);  // red
        color[1] = new Color(0f, 20 / 255f, 150 / 255f); // blue
        color[2] = new Color(200 / 255f, 200 / 255f, 80 / 255f);  // gold
        color[3] = new Color(80 / 255f, 30 / 255f, 90 / 255f);  // violet
        color[4] = new Color(130 / 255f, 130 / 255f, 130 / 255f);  // gray


    }

    public void Update()
    {
        if (Input.GetButtonDown("interact"))
        {
            if (this.hair_mat.color == color[0])
            {
                this.hair_mat.color = color[1];
                this.hair_mat2.color = color[1];
            }
            else if (this.hair_mat.color == color[1])
            {
                this.hair_mat.color = color[2];
            }
            else if (this.hair_mat.color == color[2])
            {
                this.hair_mat.color = color[3];
            }
            else if (this.hair_mat.color == color[3])
            {
                this.hair_mat.color = color[4];
            }
            else if (this.hair_mat.color == color[4])
            {
                this.hair_mat.color = new Color(0f, 0f, 0f, 255f);
            }
            else
                this.hair_mat.color = color[0];
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (this.body_mat[8] == cloth[0])
            {
                this.body_mat[8].SetTexture("_MainTex", cloth[1]);
                //  cloth_mat.SetTexture("_MainTex", cloth[1]);
            }
            else if (this.body_mat[8] == cloth[1])
            {
                this.body_mat[8].SetTexture("_MainTex", cloth[2]);
             //   cloth_mat.SetTexture("_MainTex", cloth[2]);
            }
            else
            {
                this.body_mat[8].SetTexture("_MainTex", cloth[0]);
               // cloth_mat.SetTexture("_MainTex", cloth[0]);
            }
        }

        //  PV.RPC("customizing", RpcTarget.All);
        Debug.Log(this.hair_mat.color);
        Debug.Log(this.cloth_mat);
        Debug.Log(this.body_mat[8]);
    }

    [PunRPC]
    public void customizing()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetButtonDown("interact"))
                this.hair_mat.color = color[i];
        }
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
