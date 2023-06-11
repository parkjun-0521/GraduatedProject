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

            // ī�޶� ���� ����
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting) {
                // ī�޶� ���� ������ ��Ʈ���� ����
                stream.SendNext(mainCamera.transform.position);
                stream.SendNext(mainCamera.transform.rotation);
            }
            else {
                // �ٸ� �÷��̾�� ���޵� ī�޶� ���� ���� �б�
                Vector3 position = (Vector3)stream.ReceiveNext();
                Quaternion rotation = (Quaternion)stream.ReceiveNext();

                // ���� ������ ����
                mainCamera.transform.position = position;
                mainCamera.transform.rotation = rotation;
            }
        }
}
