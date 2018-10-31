using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Un_Setactive : MonoBehaviour {
    public float timer=3;
    private float time=0;
	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= timer) {
            this.gameObject.SetActive(false);
        }
	}
}
