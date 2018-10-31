﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArchivePoint : MonoBehaviour {
    public Transform sign;
    private PlayerManager m_PlayerManager;
    [Header("光效")]
    public GameObject light;

   // private bool IsRotate = false;
    [Range(0.1f,4)]public float rotateTime;

	// Use this for initialization
	void Start () {
        m_PlayerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();

    }
	
	// Update is called once per frame
	void Update () {
     
		
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            RotateSign();
          
        }

       
    }

    void RotateSign()
    {

        m_PlayerManager.SaveArchivePos(transform.position);
        sign.DOLocalRotate(new Vector3(0f, 0f, 0f), rotateTime);
        Light();
        // sign.Rotate(transform.up, 90f);
        Destroy(gameObject, rotateTime+0.2f);

    }
    void Light() {
        light.SetActive(false);
     }
}