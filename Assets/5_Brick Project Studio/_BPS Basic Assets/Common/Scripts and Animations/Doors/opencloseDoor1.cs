using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SojaExiles

{
	public class opencloseDoor1 : MonoBehaviour
	{

		public Animator openandclose1;
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
    }
}