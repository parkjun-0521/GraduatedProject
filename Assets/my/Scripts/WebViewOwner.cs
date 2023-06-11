using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebViewOwner : MonoBehaviourPun, IPunObservable {

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (!photonView.IsMine) return;

            // 카메라 제어 로직
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting) {
                // 카메라 시점 정보를 스트림에 쓰기
                stream.SendNext(mainCamera.transform.position);
                stream.SendNext(mainCamera.transform.rotation);
            }
            else {
                // 다른 플레이어에게 전달된 카메라 시점 정보 읽기
                Vector3 position = (Vector3)stream.ReceiveNext();
                Quaternion rotation = (Quaternion)stream.ReceiveNext();

                // 시점 정보를 적용
                mainCamera.transform.position = position;
                mainCamera.transform.rotation = rotation;
            }
        }
}
