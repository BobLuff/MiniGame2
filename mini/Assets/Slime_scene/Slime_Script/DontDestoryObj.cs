using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载场景时调用这个函数，使的一些gameobject直接从上个场景继承过来，
/// 同时，如果没能成功继承，进行错误检测，并生成这些gameobject
/// </summary>

public class DontDestoryObj : MonoBehaviour {
   // private GameObject gameControll;
    public GameObject levelSelect;
    public GameObject canvas;
    public GameObject loading;
    // public int index;
   // private int current=-1;



    void Start()
    {
      
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GetComponent<PlayerManager>().enabled = true;
            GetComponent<UIManager>().enabled = true;
        }

    }

  


    public  void DontDestroy(int index)
    {
        print("scene index: " + index);
        if (index > 3)
        {
            index = 0;
        }



        GetComponent<PlayerManager>().enabled = false;
        GetComponent<UIManager>().ESCPannel.gameObject.SetActive(false);
        GetComponent<UIManager>().enabled = false;
       
        loading.GetComponent<loading>().sceneID = index;
       // print("curr: " + current);
        loading.SetActive(true);

     

        if (index == 0)
        {
            canvas.SetActive(false);
            levelSelect.SetActive(true);
            loading.SetActive(true);
        }
        else
        {
            canvas.SetActive(true);
            levelSelect.SetActive(false);


        }
        /*
        if (SceneManager.GetActiveScene().buildIndex == index)
        {
            levelSelect.SetActive(false);

        }

    */
       



     //   Time.timeScale = 1;
    
        SceneManager.LoadScene(index);
       // levelSelect.SetActive(false);
        

        GetComponent<UIManager>().IsEnablePlayer = false;


        // loading.SetActive(false);


    }



}
