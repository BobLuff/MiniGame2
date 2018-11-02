using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// 记录玩家相关信息
/// </summary>
public class PlayerManager : MonoSingleton<PlayerManager> {
    [Header("金币判断标准")]
    [SerializeField]
    private int coinStandard;
    [Header("死亡次数判断标准")]
    [SerializeField]
    private int deathStandard;
    [Header("通关时间判断标准,单位分钟")]
    [Range(5,60)]
    [SerializeField]
    private float passTimeStandard;
    private Vector2 archivePoint=Vector2.zero;                   //存档点坐标

    private int coinsNum = 0;                                    //金币收集的数量
    private int deathsNum = 0;                                   //死亡的次数

    private TimeSpan _endTime;
    private TimeSpan _startTime;
    private TimeSpan _useTime;



    public bool PassSign
    {
        get;
        set;
    }


    #region 输出评分标准到UI
    public string CoinText
    {
        get
        {
            print("coinsNum:  " + coinsNum);
            return String.Format("{0}/{1}",coinsNum,coinStandard);
        }
    }

    public string DeathText
    {
        get
        {
            return String.Format("死亡次数<{0}",deathStandard);
        }
    }
    public string TimeText
    {
        get
        {
            return String.Format("推荐时间:{0}分钟\n通关时间:{1}分钟", passTimeStandard, (int)_useTime.TotalMinutes);
        }
    }



    #endregion
 

   /// <summary>
   /// 初始化玩家数据
   /// </summary>
   public void Initize()
    {
        coinsNum = 0;
        deathsNum = 0;
        PassSign = false;
        GetStartTime();
    }


    /// <summary>
    /// 通关，结算
    /// </summary>
    public void PassLevel()
    {
        UIObjManager.Instance.SetResultPanelState(true);
        UIManager.Instance.ShowResultText(CoinText, DeathText, TimeText);
        GetEndTime();
        ComputingResult();

    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayerDead()
    {
        Debug.Log("玩家死亡，返回出生点!");
        deathsNum += 1;
    }


    /// <summary>
    /// 返回到出生点，
    /// 在PlayerDeath中调用
    /// </summary>
    /// <param name="playerObj"></param>
    public void BackArchivePoint(GameObject playerObj)
    {
        playerObj.transform.position = archivePoint;
        Debug.Log("返回存档点");
    }

    /// <summary>
    /// 保存存档点的坐标
    /// </summary>
    /// <param name="pos"></param>
    public void SaveArchivePos(Vector2 pos)
    {
        print("存档成功！");
        archivePoint = pos;
    }


    /// <summary>
    /// 通关，统计结果
    /// </summary>
    private void ComputingResult()
    {
        var starNum = 0;
        bool s1 = false;
        bool s2 = false;
        bool s3 = false;
        if(coinsNum>=coinStandard)
        {
            starNum ++;
            s1 = true;
        }
        if(deathsNum<=deathStandard)
        {
            starNum ++;
            s2 = true;
        }
        if(_useTime.TotalMinutes<=passTimeStandard)
        {
            starNum++;
            s3 = true;
        }

        UIManager.Instance.ShowResultUI(s1,s2,s3);
        //调写入xml的函数，保存当前通关的关卡id和星星的数量
        XMLManager.Instance.WriteXml(SceneManager.GetActiveScene().buildIndex, starNum);
        UIObjManager.Instance.IsReloadXml = true;

    }

#region  计算通关时间

    private void GetStartTime()
    {
        _startTime =DateTime.Now- DateTime.MinValue;
    }

    private void GetEndTime()
    {
        _endTime = DateTime.Now - DateTime.MinValue;
    }

#endregion


    public void AddCoin()
    {
        coinsNum++;
    }


}
