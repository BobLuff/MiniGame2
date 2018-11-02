using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    private PlayerManager _playerManager;
    private Player_Control m_PlayerControl;


    // Use this for initialization
    void Start() {
        _playerManager = PlayerManager.Instance;
        m_PlayerControl = GetComponent<Player_Control>();

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Spine")|| coll.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Spine!!!,角色死亡");
            _playerManager.PlayerDead();
            _playerManager.BackArchivePoint(gameObject);
            StartCoroutine(ShowEffect());
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Spine"))
        {
            Debug.Log("Spine!!!,角色死亡");
            _playerManager.PlayerDead();
            _playerManager.BackArchivePoint(gameObject);
            StartCoroutine(ShowEffect());
        }
        if(coll.gameObject.CompareTag("Coin"))
        {
            _playerManager.AddCoin();
            Destroy(coll.gameObject);
        }

        if(coll.gameObject.CompareTag("PassSign"))
        {
            Debug.Log("通关");
            _playerManager.PassLevel();

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
        yield return new WaitForSeconds(0.5f);
        m_PlayerControl.enabled = true;
        transform.GetComponent<TrackGraphic>().enabled = true;


    }

 
}
