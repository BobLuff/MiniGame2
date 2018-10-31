using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Des_Button : MonoBehaviour {
    //[Header("绑定ESC脚本的物体")]
    //public GameObject Esc_scripts;
    [Header("跳转的场景编号")]
    public int i;
    //private Get_Pausebutton Stop;

    // Use this for initialization
    private int index;//获取当前场景编号
	void Start () {
        index = SceneManager.GetActiveScene().buildIndex;
        //Stop = GameObject.Find("MainCamera").GetComponent<Get_Pausebutton>();
        //Stop =Esc_scripts.GetComponent<Get_Pausebutton>();
       
    }
    public void goon()
    {
        
        Destroy(this.transform.parent.gameObject);
    }
    public void otherscene()
    {
        Destroy(this.transform.parent.gameObject);
        SceneManager.LoadScene(i);
    }
    public void agin() {
        Destroy(this.transform.parent.gameObject);
        SceneManager.LoadScene(index);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
