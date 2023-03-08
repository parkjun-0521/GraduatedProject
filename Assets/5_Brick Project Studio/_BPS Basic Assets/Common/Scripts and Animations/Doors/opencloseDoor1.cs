using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SojaExiles

{
	public class opencloseDoor1 : MonoBehaviour
	{

		public Animator openandclose1;
		public Animator autodoor;
		public bool open = false;
		public GameObject[] Player;

		public PhotonView PV;
		public int anime;

		void Start()
		{
			open = false;
			Player = GameObject.FindGameObjectsWithTag("Player");
			anime = Animator.StringToHash("isopen");
		}

		void Update()
		{
			dooropen();
			autodooropen();
		}

		public void dooropen()
		{
			for (int i = 0; i < Player.Length; i++) {
				float dist = Vector3.Distance(Player[i].transform.position, transform.position);
				if (dist <= 2) {				
					if (open == false) {
						if (Input.GetButtonDown("interact"))
							PV.RPC("opening", RpcTarget.All);
					}
					else {
						if (Input.GetButtonDown("interact"))
							PV.RPC("closing", RpcTarget.All);
					}					
				}
			}
		}
        public void autodooropen()
        {
            for (int i = 0; i < Player.Length; i++)
            {
                float dist = Vector3.Distance(Player[i].transform.position, transform.position);
                if (dist <= 2)
                {
                    PV.RPC("aopening", RpcTarget.All);
                }
                else
                {
                    PV.RPC("aclosing", RpcTarget.All);
                }

            }
        }

        [PunRPC]
		public void opening()
		{			
			openandclose1.SetBool(anime, true);
			open = true;
		}
		[PunRPC]
		public void closing()
		{		
			openandclose1.SetBool(anime, false);
			open = false;
		}
        [PunRPC]
        public void aopening()
        {          
            autodoor.SetFloat("rev",1.0f);
        }
        [PunRPC]
        public void aclosing()
        {
            autodoor.SetFloat("rev", -1.0f);
        }
    }
}