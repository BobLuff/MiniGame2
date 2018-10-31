using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    [Header("金币判断标准")]

    public int coinStandard;
    [Header("死亡次数判断标准")]
    public int deathStandard;
    [Header("通关时间判断标准,单位分钟")]
    [Range(5,60)] public  float passTimeStandard;
    [Header(" 开启无敌")]

    public bool IsCheating=false ;//开挂

 
    private bool isDead = false;   //判断角色是否死亡

    private Vector2 archivePoint=Vector2.zero;      //存档点坐标
 //   private Transform playerObj;


    public int coinsNum = 0;     //金币收集的数量
    private int deathsNum = 0;    //死亡的次数
    private float passTime = 0f;   //通关时间
    private bool passSign = false; //是否通关，进入下一关卡
    private int starNum = 0;

    private int Second;
    private float Minute;
    private int Hour;

    private XMLManager m_xmlManager;


    public bool PassSign
    {
        set
        {
            passSign = value;
        }
    }




    #region 输出评分标准到UI
    public string CoinText
    {
        get
        {
            print("coinsNum:  " + coinsNum);
            return "5" + "/" + coinStandard.ToString();
        }
    }

    public string DeathText
    {
        get
        {
           // return "死亡次数<4";
            return "死亡次数<"+ deathStandard.ToString();
        }
    }
    public string TimeText
    {
        get
        {
            Second = (int)passTime;
            return Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString() + " (通关用时)" + "\n" + "0:" + passTimeStandard.ToString() + ":0";
        }
    }



    #endregion

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }      


   public void Initize()
    {
        coinsNum = 0;
        deathsNum = 0;
        passTime = 0;
        starNum = 0;
        Second = 0;
        Minute = 0f;
        Hour = 0;
    }

	// Use this for initialization
	void Start () {
       
        m_xmlManager = GetComponent<XMLManager>();
        Initize();

    }
	
	// Update is called once per frame
	void Update () {
      //  print("coin+:" + coinsNum);
        passTime += Time.deltaTime;
       // Debug.Log(passTime);
        if(passTime>=60f)
        {
            passTime = 0;
            Minute++;
            

        }
        if(Minute>=60f)
        {
            Minute = 0;
            Hour++;
        }


        if(isDead&&!IsCheating)
        {
            print("角色死亡，回到存档点");
           // BackArchivePoint();
            deathsNum += 1;
            isDead = false;
        }
        if(passSign)
        {
            GetComponent<UIManager>().ShowResultText(CoinText, DeathText, TimeText);
            ComputingResult();
      
            //Time.timeScale = 0;
            passSign = false;
        }

        if(IsCheating)
        {
            coinsNum = 100;
            deathsNum = 1;
            passTime = 5f;
        }
		
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

    void ComputingResult()
    {
        bool s1 = false;
        bool s2 = false;
        bool s3 = false;
        if(coinsNum>=coinStandard)
        {
            starNum += 1;
            s1 = true;
        }
        if(deathsNum<=deathStandard)
        {
            starNum += 1;
            s2 = true;
        }
        if(Second<=passTimeStandard&&Hour==0f)
        {
            starNum++;
            // Mathf.Approximately()
            s3 = true;
        }

       // ShowResultText();
        print("星星的数量：" + starNum);
        GetComponent<UIManager>().ShowResultUI(s1,s2,s3);
        GetComponent<UIManager>().ClearAllListeners();
        GetComponent<UIManager>().AddNextBtnListener();

        //调写入xml的函数，保存当前通关的关卡id和星星的数量
        m_xmlManager.WriteXml(SceneManager.GetActiveScene().buildIndex, starNum);
   

    }

  
}
