using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    // �ε� ȭ���� �����ϴ� ���� 
    // ������ ����ȯ�� �°� �ε�ȭ���� �����ϴ� ���� �ƴϴ�. 
    // ���� �ʱ���� ���� ������ �۾��� ���� ���� ſ�� ���� ��ȯ�Ǵ� �� ������ �ð��� ����� �� �� ������. 
    // ���� �̰��� ���Ƿ� ȭ���� �����ֵ��� �����. ( �ε�ȭ�鵵 ���� �� �ִٴ� ���� �����ذ� ) 
    public GameObject lodingPanel;

    [SerializeField] 
    Image progressBar;
    
    public float timer = 0.0f;      // ������ �� Ÿ�̸Ӵ�ſ� ���� ����Ǵ� �ð��� �־�����Ѵ�. 

    private void Update()
    {
        timer += Time.deltaTime;
        // �� �ε��� 40% ������ ��
        if (progressBar.fillAmount < 0.4f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.6f, timer);
        }
        // �� �ε� 40% �̻� 70% �̸� �϶� 
        else if (progressBar.fillAmount >= 0.4f && progressBar.fillAmount < 0.7f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.8f, timer);
        }
        // �� �ε� 70% �̻� 98% �̸� �϶� 
        else if (progressBar.fillAmount >= 0.7f && progressBar.fillAmount < 0.98f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
        }
        // �� �ε��� �Ϸ�Ǿ��� �� 
        else {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 2f, timer);
            StartCoroutine(CloseLoding());
        }
    }
    IEnumerator CloseLoding()
    {
        yield return new WaitForSeconds(0.8f);
        // �ε� �ٰ� �� á�� ��� 
        if(progressBar.fillAmount == 1) {
            // ���� �⺻�� �Ǵ� Server ���� UI�� Ȱ��ȭ 
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            timer = 0.0f;
            progressBar.fillAmount = 0.0f;
            lodingPanel.SetActive(false);
            networkManager.Server.SetActive(true);

            // �ε� ���� �濡 �����Ͽ��� �� ������ ������Ʈ 
            if (networkManager.checkRoom) {
                networkManager.RoomPanel.SetActive(true);
                networkManager.Server.SetActive(false);
            }
        }
    }
}