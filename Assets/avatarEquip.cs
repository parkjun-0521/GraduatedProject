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
            // 현재 플레이어가 이 오브젝트를 소유하고 있을 때만 상태를 변경합니다.
            objectToSync.SetActive(!objectToSync.activeSelf);

            // RPC를 통해 상태 변경을 다른 플레이어에게 알립니다.
            pv.RPC("SyncObjectState", RpcTarget.All, objectToSync.activeSelf);
        }
    }

    [PunRPC]
    private void SyncObjectState(bool isActive)
    {
        // 모든 플레이어에 의해 호출되며, 상태를 동기화합니다.
        objectToSync.SetActive(isActive);
    }
}
