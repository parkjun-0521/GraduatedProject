using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    // 로딩 화면을 구현하는 로직 
    // 실제로 씬전환에 맞게 로딩화면이 등작하는 것이 아니다. 
    // 개발 초기부터 씬을 나누어 작업을 하지 않은 탓에 씬이 전환되는 그 시점의 시간을 계산을 할 수 없었다. 
    // 따라서 이것은 임의로 화면을 보여주도록 만든것. ( 로딩화면도 만들 수 있다는 것을 보여준것 ) 
    public GameObject lodingPanel;

    [SerializeField] 
    Image progressBar;
    
    public float timer = 0.0f;      // 원래는 이 타이머대신에 씬이 변경되는 시간을 넣어줘야한다. 

    private void Update()
    {
        timer += Time.deltaTime;
        // 씬 로딩이 40% 이하일 때
        if (progressBar.fillAmount < 0.4f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.6f, timer);
        }
        // 씬 로딩 40% 이상 70% 미만 일때 
        else if (progressBar.fillAmount >= 0.4f && progressBar.fillAmount < 0.7f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.8f, timer);
        }
        // 씬 로딩 70% 이상 98% 미만 일때 
        else if (progressBar.fillAmount >= 0.7f && progressBar.fillAmount < 0.98f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
        }
        // 씬 로딩이 완료되었을 때 
        else {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 2f, timer);
            StartCoroutine(CloseLoding());
        }
    }
    IEnumerator CloseLoding()
    {
        yield return new WaitForSeconds(0.8f);
        // 로딩 바가 다 찼을 경우 
        if(progressBar.fillAmount == 1) {
            // 가장 기본이 되는 Server 관련 UI를 활성화 
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            timer = 0.0f;
            progressBar.fillAmount = 0.0f;
            lodingPanel.SetActive(false);
            networkManager.Server.SetActive(true);

            // 로딩 이후 방에 입장하였을 때 보여줄 오브젝트 
            if (networkManager.checkRoom) {
                networkManager.RoomPanel.SetActive(true);
                networkManager.Server.SetActive(false);
            }
        }
    }
}