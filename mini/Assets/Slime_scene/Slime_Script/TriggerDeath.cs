using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触发死亡
/// </summary>
public class TriggerDeath : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {

        // Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.CompareTag("Player"))
        {
            print("Player Death");
        }
    }
}
