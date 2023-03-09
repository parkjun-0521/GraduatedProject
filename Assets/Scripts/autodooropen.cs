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

        void Start()
        {
            Player = GameObject.FindGameObjectsWithTag("Player");
            autodoor = GetComponent<Animator>();
        }

        void Update()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                PV.RPC("aopen", RpcTarget.All);
            }
        }

      
        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                PV.RPC("aclose", RpcTarget.All);
            }
        }

        [PunRPC]
        public void aopen()
        {
            autodoor.SetBool("isopen", true);
        }

        [PunRPC]
        public void aclose()
        {
            autodoor.SetBool("isopen", false);
        }
    }
}