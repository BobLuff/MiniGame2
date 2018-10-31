using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotation : MonoBehaviour {
    [Header("旋转速度")]
    public float rotateSpeed=5;
    [Header("停止所需时间")]
    public float stopTime = 5;
    // Use this for initialization
    void Start () {
		
	}
    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(Vector3.forward*rotateSpeed);
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void SotpRotate() {
        DOTween.To(() => rotateSpeed, x => rotateSpeed = x, 0, stopTime);
    }
}
