using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blood_control : MonoBehaviour {
    private _2dxFX_Blood blood;
    //public float timer = 1;
   // private float current = 0;
    [Header("停止所需时间")]
    public float stopTime = 1f;

    // Use this for initialization
    void Start () {
        blood=GetComponent<_2dxFX_Blood>();
        

    }


    public void test()
    {
        DOTween.To(() => blood.TurnToBlood, x => blood.TurnToBlood = x, 1, stopTime);

    }
    public void test2()
    {
        DOTween.To(() => blood.TurnToBlood, x => blood.TurnToBlood = x, 0, stopTime);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
