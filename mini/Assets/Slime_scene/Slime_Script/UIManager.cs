using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour {
  //  public GameObject canvas;
   // public GameObject gameController;
    [Header("评分面板")]
    public GameObject ResultPannel;
    [Header("Result中YellowStar的Image")]
    public Image[] starImagesUI;
    public Text[] resultTexts;
   // public Button continueBtn;

    public RectTransform ESCPannel;

  //  private Transform m_Player;    //
    [HideInInspector]
    //是否取消对主角的控制
    public bool IsEnablePlayer=false;

    private Button[] EscButtons;

    public GameObject levelSelect;
   // [Header("两种类型的星星图片 0-为灰色，1-为黄色")]
  //  public Sprite[] starSpites;

    private Button[] resultBtns;

    private XMLManager m_xmlManager;





    void Initize()
    {

        for (int i=0;i<3;i++)
        {
            starImagesUI[i].gameObject.SetActive(false);
            resultTexts[i].text = "";
        }

        ResultPannel.SetActive(false);
        ESCPannel.gameObject.SetActive(false);
       


    }

    public void ClearAllListeners()
    {
        for(int i=0;i<resultBtns.Length;i++)
        {
            resultBtns[i].onClick.RemoveAllListeners();
        }
      
    }

    public  void AddNextBtnListener()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        resultBtns[0].onClick.AddListener(delegate () { this.LoadScene(index); });
        resultBtns[1].onClick.AddListener(delegate () { this.LoadScene(0); });
       
        resultBtns[2].onClick.AddListener(delegate () { this.LoadScene(index + 1); });  //这里可能存在越界，需要注意
                                                                                        // Initize();
    }
 
    // Use this for initialization
    void Start () {
     
        
     
       
        Initize();
     
        EscButtons = ESCPannel.GetComponentsInChildren<Button>();
        m_xmlManager = GetComponent<XMLManager>();

      //  levelSelect = GameObject.Find("LevelSelect");
        if (!levelSelect)
        {
            Debug.LogError("LevelSelect对象为空");
            return;
        }
        levelSelect.SetActive(false);
        resultBtns = ResultPannel.GetComponentsInChildren<Button>();
    

        //print(index);
     

        EscButtons[0].onClick.AddListener(ContinueGame);
        EscButtons[1].onClick.AddListener(LevelSelectPannel);
        EscButtons[2].onClick.AddListener(delegate() { this.LoadScene(0); });
        EscButtons[3].onClick.AddListener(ExitGame);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) && (!ESCPannel.gameObject.activeInHierarchy)&&!levelSelect.activeInHierarchy)
        {
            ESCPannel.gameObject.SetActive(true);
            ESCPannel.GetComponent<UIAnims>().PlayAnims();
            PauseGame();

        }



    }


    void LevelSelectPannel()
    {
        levelSelect.SetActive(true);
        ESCPannel.gameObject.SetActive(false);
    }


    /// <summary>
    /// 暂停游戏，隐藏控制player和绘制轨迹的脚本，并设置时间为0
    /// </summary>
    void PauseGame()
    {
        IsEnablePlayer = true;      
       
                                                                  
      //  Time.timeScale = 0;

    }
    
    /// <summary>
    /// 继续游戏
    /// </summary>
    void ContinueGame()
    {
     //  Time.timeScale = 1;
        ESCPannel.gameObject.SetActive(false);
        ESCPannel.GetComponent<UIAnims>().InitizeAnim();
        IsEnablePlayer = false;

    }

    void ExitGame()
    {
        Application.Quit();
    }



    /// <summary>
    /// 游戏结算函数，最后显示获得了多少分
    /// </summary>
    /// <param name="star"></param>

    public void ShowResultUI(bool s1,bool s2,bool s3)
    {
       // Time.timeScale = 1;

        // print("star的数量: " + star);
        ResultPannel.SetActive(true);
        PauseGame();
        //  StartCoroutine(ShowYellowStars(star));
    
        ShowYellowStars(s1,s2,s3);
      //  GetComponent<PlayerManager>().ShowResultText();
     //   GetComponent<PlayerManager>().ShowResultText


        // continueBtn.onClick.AddListener(LoadNextScene);

    }

    /// <summary>
    /// 在评价面板中，显示评价的文字
    /// </summary>

    public void ShowResultText(string coin,string death,string timeText)
    {

        resultTexts[0].text = coin;
        resultTexts[1].text = death;
        resultTexts[2].text = timeText;


    }




    #region 播放黄色星星动画 
    /// <summary>
    /// 评价面板中，每种情况获得星星的情况
    /// 播放获得黄色星星的动画
    /// s1表示收集的金币是否达标
    /// s2---死亡次数
    /// s3---通关时间
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <param name="s3"></param>
    void ShowYellowStars(bool s1, bool s2, bool s3)
    {
       
      //  Debug.Log("111");
      //  yield return new WaitForSeconds(0.01f);
        if(s1)
        {

            //starImages[0].sprite = starSpites[1];
            starImagesUI[0].gameObject.SetActive(true);
            starImagesUI[0].transform.DOLocalMove(new Vector3(0, -3.7f, 0), 0.2f);
        }
        if (s2)
        {
            starImagesUI[1].gameObject.SetActive(true);
            starImagesUI[1].transform.DOLocalMove(new Vector3(0, -3.7f, 0), 0.4f);
        }
        if (s3)
        {
            starImagesUI[2].gameObject.SetActive(true);
            starImagesUI[2].transform.DOLocalMove(new Vector3(0, -3.7f, 0), 0.6f);
     
        }

    }

    #endregion

    void LoadScene( int sceneId)
    {
        print("scene id :" + sceneId);
        ResultPannel.SetActive(false);
        DontDestoryObj obj;
      //  SceneManager.LoadScene(index);
        obj = GetComponent<DontDestoryObj>();
        obj.DontDestroy(sceneId);
       // ESCPannel = GameObject.Find("Canvas/ESCPannel").GetComponent<RectTransform>();

    }



}
