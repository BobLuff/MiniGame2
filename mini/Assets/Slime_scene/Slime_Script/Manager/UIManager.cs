using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class UIManager : MonoSingleton<UIManager>{

    [Header("评分面板")]
    [SerializeField]
    private GameObject ResultPannel;
    [SerializeField]
    private Text _flashText;
    [SerializeField]
    private Text _heartText;
    [SerializeField]
    private Text _timeText;
    [SerializeField]
    [Tooltip("黄色星星")]
    private Image[] starImagesUI;                //黄色星星
    [SerializeField]
    private GameObject levelSelectPanel;
    [SerializeField]
    public RectTransform ESCPannel;
    [SerializeField]
    private UIAnims _escAnims;

    [Header("史莱姆图标")]
    [SerializeField]
    private Yellow_CoolingTime _yellowCD;
    [SerializeField]
    private Red_CoolingTime _redCD;
    [Header("红色史莱姆的sprite，0-灰色，1-红色")]
    [SerializeField]
    private Sprite _redSlime_Black;
    [SerializeField]
    private Sprite _redSlime_Normal;

    private Button[] resultBtns;
    private XMLManager m_xmlManager;
    private Player_Enable _playerEnable = new Player_Enable();
    private const int MainPageScene = 0;                             //主页场景索引号




    public bool HaveRedSlime
    {
        get { return _redCD.HavingRed; }
    }


    public bool HaveYellowSlime
    {
        get { return _yellowCD.HavingYellow; }
    }

    public bool IsGetRedSlime { get; private set; }






    void Initize()
    {
        for (int i=0;i<3;i++)
        {
            starImagesUI[i].gameObject.SetActive(false);
        }
        _flashText.text = "";
        _heartText.text = "";
        _timeText.text = "";

        ResultPannel.SetActive(false);

    }




 
    // Use this for initialization
    void Start () {

        Initize();
        IsGetRedSlime = false;
        m_xmlManager = GetComponent<XMLManager>();
        resultBtns = ResultPannel.GetComponentsInChildren<Button>();
    

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) && (!ESCPannel.gameObject.activeInHierarchy)&&!levelSelectPanel.activeInHierarchy)
        {
            if(SceneManager.GetActiveScene().buildIndex!=MainPageScene)                   //开始界面不显EscPanel
            {
                OnClickPauseBtn();
                ESCPannel.gameObject.SetActive(true);
                _escAnims.PlayAnims();
            }
        }
    }

    #region 结算面板

    /// <summary>
    /// 重玩本关
    /// </summary>
    public void OnClickRetryBtn()
    {
        UIObjManager.Instance.Init();
        LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    /// <summary>
    /// 下一关
    /// </summary>
    public void OnClickNextBtn()
    {
        var curIndex = SceneManager.GetActiveScene().buildIndex;
        if(curIndex>=3)
        {
            OnClickMainPageBtn();
        }
        else
        {
            LevelManager.Instance.LoadScene(curIndex + 1);
        }


    }
    
    #endregion



    /// <summary>
    /// 选择关卡
    /// </summary>
    public void OnClickLevelSelectBtn()
    {
        levelSelectPanel.SetActive(true);
        ESCPannel.gameObject.SetActive(false);
    }


    /// <summary>
    /// 暂停游戏，隐藏控制player和绘制轨迹的脚本，并设置时间为0
    /// </summary>
    public void OnClickPauseBtn()
    {
        _playerEnable = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Enable>();
        _playerEnable.SetPlayerState(false);
    }
    
    /// <summary>
    /// 继续游戏
    /// </summary>
    public void OnClickContinueBtn()
    {
        ESCPannel.gameObject.SetActive(false);
        _escAnims.InitizeAnim();
        _playerEnable.SetPlayerState(true);

    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void OnClickExitBtn()
    {
        Application.Quit();
    }

    /// <summary>
    /// 返回主页
    /// </summary>
    public void OnClickMainPageBtn()
    {
        Initize();
        UIObjManager.Instance.Init();
        SceneManager.LoadScene(MainPageScene);
    }


    /// <summary>
    /// 结算
    /// </summary>
    /// <param name="s1"></param>
    public void ShowResultUI(bool s1,bool s2,bool s3)
    {
        ResultPannel.SetActive(true);
        OnClickPauseBtn();
        ShowYellowStars(s1,s2,s3);
    }

    /// <summary>
    /// 在评价面板中，显示评价的文字
    /// </summary>
    public void ShowResultText(string coin,string death,string timeText)
    {
        _flashText.text = coin;
        _heartText.text = death;
        _timeText.text = timeText;
    }




    #region 播放黄色星星动画 
    /// <summary>
    /// 评价面板中，每种情况获得星星的情况
    /// 播放获得黄色星星的动画
    /// s1表示收集的金币是否达标
    /// s2---死亡次数
    /// s3---通关时间
    /// </summary>
    void ShowYellowStars(bool s1, bool s2, bool s3)
    {

        if(s1)
        {
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





#region slime的UI
   
    /// <summary>
    /// 设置红色slime的状态图标
    /// </summary>
    /// <param name="isShowNormal"></param>
    private void SetResSlimeState(bool isShowNormal)
    {
        _redCD.m_normalImage.sprite=isShowNormal?_redSlime_Normal:_redSlime_Black;
    }

    public void PlaySlimeUiAnim(SlimeStatus curSlime,Vector3 endScale)
    {
        switch (curSlime)
        {
            case SlimeStatus.Slime_Red:
                _redCD.transform.DOScale(endScale, 0.3f);
                break;
            case SlimeStatus.Slime_Yellow:
                _yellowCD.transform.DOScale(endScale, 0.3f);
                break;
            default:
                break;
        }
    }




    public void SetSlimeCD(SlimeStatus curSlime)
    {
        switch (curSlime)
        {
            case SlimeStatus.Slime_Red:
                _redCD.HavingRed = false;
                break;
            case SlimeStatus.Slime_Yellow:
                _yellowCD.HavingYellow = false;
                break;
            default:
                break;
        }   
    }

    /// <summary>
    /// 是否解锁红色slime
    /// </summary>
    /// <param name="index"></param>
    public void GetRedSlime(int index)
    {
        if(index>1)
        {
            IsGetRedSlime = true;
            SetResSlimeState(true);
        }
        else
        {
            IsGetRedSlime = false;
            SetResSlimeState(false);
        }
    }
    

#endregion



}
