using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SojaExiles

{
    public class autodooropen : MonoBehaviour
    {
        public Animator autodoor;
        public GameObject[] Player;

        public PhotonView PV;
        public float adoor;

        void Start()
        {
            Player = GameObject.FindGameObjectsWithTag("Player");
           // adoor = Animator.StringToHash("rev");
        }

        void Update()
        {
            autodoorwork();
        }

        public void autodoorwork()
        {
            for (int i = 0; i < Player.Length; i++)
            {
                float dist = Vector3.Distance(Player[i].transform.position, this.transform.position);
                print(dist);
                if (dist <= 4.5)
                {
                    PV.RPC("aopening", RpcTarget.All);
                }
                else if (dist >= 9)
                {
                    PV.RPC("aclosing", RpcTarget.All);
                }

            }
        }

        [PunRPC]
        public void aopening()
        {
            autodoor.SetFloat("open",1.0f);
            autodoor.SetFloat("open2",1.0f);
            autodoor.SetFloat("rev", 1.0f);          
        }
        [PunRPC]
        public void aclosing()
        {
            autodoor.SetFloat("open", -1.0f);
            autodoor.SetFloat("open2",-1.0f);
            autodoor.SetFloat("rev", -1.0f);
        }
    }
}