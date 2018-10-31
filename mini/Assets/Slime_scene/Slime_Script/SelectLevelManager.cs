using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 该段代码只是在开始关卡中使用，是从UIManager中拷贝的一部分，以后有时间可以重写这部分
/// 该代码是为了防止DontDestory无线调用自己，陷入死循环
/// </summary>
public class SelectLevelManager : MonoBehaviour {
    public static bool IsHaving = false;
    public static bool IsFirstLoad = true;
    public GameObject levelSelect;
    public GameObject gameControll;
    public GameObject canvas;
    public GameObject loading;
    public Button start;
    private GameObject obj;




    // Use this for initialization
    void Start () {
        if (!levelSelect)
        {
            Debug.LogError("LevelSelect对象为空");
            return;
        }
        if (!IsFirstLoad)
        {
            Destroy(levelSelect);
            Destroy(gameControll);
            Destroy(canvas);
            Destroy(loading);
            start.onClick.AddListener(show);
            obj = GameObject.Find("LevelSelect");
            obj.SetActive(false);

            /*
            levelSelect.SetActive(false);
            gameControll.SetActive(false);
            canvas.SetActive(false);
            loading.SetActive(false);
            */
        }

        if (!IsHaving&&IsFirstLoad)
        {
            gameControll.SetActive(true);
            levelSelect.SetActive(true);
         //   levelSelect.SetActive(false);
            DontDestroyOnLoad(gameControll);
            DontDestroyOnLoad(levelSelect);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(loading);
            IsHaving = true;
            IsFirstLoad = false;
        }
        levelSelect.SetActive(false);
    
  

    }

    void show()
    {
        
        obj.SetActive(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
