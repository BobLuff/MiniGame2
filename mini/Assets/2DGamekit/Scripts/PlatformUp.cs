using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 触发父物体的上升，需放在子物体中
/// </summary>
public class PlatformUp : MonoBehaviour {
    [Header("移动速度")]
    public float speed = 10;
    [Header("上升边界")]
    public Transform up;
    [Header("下降边界")]
    public Transform down;
    //[Header("上界停止位置")]
    //public Transform up_stop;
    //[Header("下界停止位置")]
    //public Transform down_stop;

    void FixedUpdate()
    {
        if (up) {
            if (this.gameObject.transform.position.y < up.transform.position.y)
            {
                this.gameObject.transform.parent.gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
        if (down) {
            if (this.gameObject.transform.position.y > down.transform.position.y)
            {
                this.gameObject.transform.parent.gameObject.transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
        //if (down_stop)
        //{
        //    if (this.transform.position.y <= down_stop.position.y)
        //    {
        //        this.gameObject.transform.parent.gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
        //    }
        //}
    }
        
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
