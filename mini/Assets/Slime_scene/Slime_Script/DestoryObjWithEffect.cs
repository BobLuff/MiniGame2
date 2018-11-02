using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 销毁物体并生成特效，继承至Destiryobject
/// </summary>

public class DestoryObjWithEffect : DestroyObject {

    [Header("特效物体")]
    public GameObject effectObj;

	
	// Update is called once per frame
	void Update () {
        if(timer<0.2f)
        {
            Instantiate(effectObj, transform.position, Quaternion.identity);
        }
		
	}
}
