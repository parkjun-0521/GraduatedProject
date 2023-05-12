using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    //   public GameObject player;
    //   GameObject[] plchild;
    public static ChangeCharacter charac;

    public GameObject[] player = new GameObject[7];
  
    public Transform playerposition;
    
    public int CharacterCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(charac == null)
        {
            charac = this;
        }
        playerposition = GameObject.Find("nowposition").transform;
      /*  for(int i=0; i<4; i++)
        {
            plchild[i] = player.transform.GetChild(i).gameObject;
        }*/

       // player[0] = GameObject.Find("Admin1");
        player[0] = GameObject.Find("PlayerCharacter").transform.Find("Admin1").gameObject;
        player[1] = GameObject.Find("PlayerCharacter").transform.Find("Man1").gameObject;
        player[2] = GameObject.Find("PlayerCharacter").transform.Find("Man2").gameObject;
        player[3] = GameObject.Find("PlayerCharacter").transform.Find("Man3").gameObject;
        player[4] = GameObject.Find("PlayerCharacter").transform.Find("Female1").gameObject;
        player[5] = GameObject.Find("PlayerCharacter").transform.Find("Female2").gameObject;
        player[6] = GameObject.Find("PlayerCharacter").transform.Find("Female3").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    private void LateUpdate()
    {
       if(GameObject.FindWithTag("Player").activeSelf == true)
        {
            playerposition.position = GameObject.FindWithTag("Player").transform.position;
        }

    }

    public void Changepl()
    {
        if(CharacterCount != 1)
        {
            for(int i = 0; i < player.Length; i++)
            {
                //Debug.Log(player[i].name);
                player[i].SetActive(false);
                CharacterCount = 1;
            }
        }
    }

    public void OnClick()
    {
        CharacterCount++;
        Changepl();
    }

    public void admin1()
    {
        OnClick();
        player[0].transform.position = playerposition.position;
        player[0].SetActive(true);
        
    }
    public void man1()
    {
        OnClick();
        player[1].transform.position = playerposition.position;
        player[1].SetActive(true);
    }
    public void man2()
    {
        OnClick();
        player[2].transform.position = playerposition.position;
        player[2].SetActive(true);
    }
    public void man3()
    {
        OnClick();
        player[3].transform.position = playerposition.position;
        player[3].SetActive(true);
    }
    public void Female1()
    {
        OnClick();
        player[4].transform.position = playerposition.position;
        player[4].SetActive(true);
    }
    public void Female2()
    {
        OnClick();
        player[5].transform.position = playerposition.position;
        player[5].SetActive(true);
    }
    public void Female3()
    {
        OnClick();
        player[6].transform.position = playerposition.position;
        player[6].SetActive(true);
    }
}
