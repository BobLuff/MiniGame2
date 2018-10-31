using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {
    [Header("旋转速度(负为顺时针)")]
    public float rotate = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward * rotate * Time.deltaTime, Space.World);
    }
}
