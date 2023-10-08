using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarEquip : MonoBehaviour
{
    public GameObject objectToSync;
    public PhotonView pv;

    public void ToggleObjectState()
    {
        if (pv.IsMine)
        {
            // ���� �÷��̾ �� ������Ʈ�� �����ϰ� ���� ���� ���¸� �����մϴ�.
            objectToSync.SetActive(!objectToSync.activeSelf);

            // RPC�� ���� ���� ������ �ٸ� �÷��̾�� �˸��ϴ�.
            pv.RPC("SyncObjectState", RpcTarget.All, objectToSync.activeSelf);
        }
    }

    [PunRPC]
    private void SyncObjectState(bool isActive)
    {
        // ��� �÷��̾ ���� ȣ��Ǹ�, ���¸� ����ȭ�մϴ�.
        objectToSync.SetActive(isActive);
    }
}
