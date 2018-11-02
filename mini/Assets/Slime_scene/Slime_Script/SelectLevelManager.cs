using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 该段代码只是在开始关卡中使用，是从UIManager中拷贝的一部分，以后有时间可以重写这部分
/// 该代码是为了防止DontDestory无线调用自己，陷入死循环
/// </summary>
public class SelectLevelManager : MonoBehaviour {
    public static bool IsFirstLoad = true;
    [SerializeField]
    private GameObject[] _dontDestoryObjs;



    private void Awake()
    {
        if(IsFirstLoad)
        {
            foreach (var obj in _dontDestoryObjs)
            {
                DontDestroyOnLoad(obj);
            }
           IsFirstLoad = false;
        } 
    }

    public void OnClickStartBtn()
    {
        UIObjManager.Instance.IsReloadXml = true;
        UIObjManager.Instance.SetLevelSelectState(true);
    }

}
