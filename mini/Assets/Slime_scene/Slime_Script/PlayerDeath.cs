using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    private PlayerManager m_PlayerManager;
    private Player_Control m_PlayerControl;


    // Use this for initialization
    void Start() {
        m_PlayerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
        m_PlayerManager.enabled = true;
        m_PlayerManager.GetComponent<UIManager>().enabled = true;
        m_PlayerControl = GetComponent<Player_Control>();

    }

    // Update is called once per frame
    void Update() {

    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Spine")|| coll.gameObject.CompareTag("Enemy"))
        {
            print("Spine!!!");
            m_PlayerManager.IsDead = true;
            m_PlayerManager.BackArchivePoint(gameObject);
            StartCoroutine(ShowEffect());
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Spine"))
        {
            print("Spine!!!");
            m_PlayerManager.IsDead = true;
            m_PlayerManager.BackArchivePoint(gameObject);
            StartCoroutine(ShowEffect());
        }
        if(coll.gameObject.CompareTag("Coin"))
        {
         //   print("Coin");
            m_PlayerManager.coinsNum++;
            Destroy(coll.gameObject);
        }

        if(coll.gameObject.CompareTag("PassSign"))
        {
            print("通关");
            m_PlayerManager.PassSign = true;
            m_PlayerControl.UI_Red.enabled = false;
            m_PlayerControl.UI_Yellow.enabled = false;

        }
    }

    /// <summary>
    /// 死亡的效果函数，防止在角色返回存档点的时候，出现一些意外情况，如按下鼠标右键
    /// </summary>
    /// <returns></returns>

    IEnumerator  ShowEffect()
    {
        m_PlayerControl.enabled = false;
        transform.GetComponent<TrackGraphic>().enabled = false;
      //  changeTransparent = true;
       // Invoke("ChangeTran", 0.8f, 0.2f);
       // yield return new WaitForSeconds(0.8f);


        yield return new WaitForSeconds(0.5f);
        m_PlayerControl.enabled = true;
        transform.GetComponent<TrackGraphic>().enabled = true;


    }

 
}
