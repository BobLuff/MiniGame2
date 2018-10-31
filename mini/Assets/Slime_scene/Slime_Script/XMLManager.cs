using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XMLManager : MonoBehaviour {
    XmlDocument Xdoc = null;
    XmlElement root;
    XmlNode dataNode;
    private int levelCount;
    [Header("灰色和黄色星星的prefab")]
    public GameObject[] starImages;

    [Header("选择关卡界面的关卡按钮")]
    public Button[] levelsBtn;




    // Use this for initialization
   public  void Start () {
        int starNum = 0;

        levelCount = 0;
        Xdoc = new XmlDocument();
        //Debug.Log("当前目录是：" + Application.dataPath);
        Xdoc.Load(Application.dataPath + "/Resources/levelSelect.xml");  //加载XML 文件
        root = Xdoc.DocumentElement;   //获取跟节点
       // Debug.Log("根元素是：" + root.Name);
        dataNode = root.SelectSingleNode("data");  //获取根节点下的子节点
        levelCount = dataNode.ChildNodes.Count;
        Debug.Log("当前可玩关卡总数： "+levelCount);
        for (int i=0;i<levelCount; i++)
        {
            int index = i + 1;
            levelsBtn[i].gameObject.SetActive(true);
            levelsBtn[i].onClick.AddListener(delegate () { this.LoadScene(index); });
            starNum = int.Parse(dataNode.ChildNodes[i].InnerText);
            CreateStar(starNum, levelsBtn[i].gameObject);
        }
        // Debug.Log("节点名称" + dataNode.Name);

        //WriteXml(3, 3);

    }

 





    #region 生成选择关卡界面星星的UI图片
    /// <summary>
    /// 该函数用于初始化时，在选择关卡的UI中生成星星
    /// </summary>
    /// <param name="index"></param>
    /// <param name="obj"></param>
    void CreateStar(int index,GameObject obj)
    {
        Transform star = obj.transform.parent;
        if (index==0)
        {
            for(int i=0;i<3;i++)
            {
                Instantiate(starImages[0], obj.transform.position + new Vector3(-40, -40, 0)+ new Vector3(i * 40, 0, 0), Quaternion.identity).transform.SetParent(star);
                // star.transform.parent = obj.transform.parent;
            }
            
        }
        else if(index==1)
        {
             Instantiate(starImages[1], obj.transform.position + new Vector3(-40, -40, 0), Quaternion.identity).transform.SetParent(star);

            for (int i=1;i<3;i++)
            {
                Instantiate(starImages[0], obj.transform.position + new Vector3(-40, -40, 0) + new Vector3(i * 40, 0, 0), Quaternion.identity).transform.SetParent(star);
           

            }
        }
        else if(index==2)
        {
            for(int i=0;i<index;i++)
            {
              Instantiate(starImages[1], obj.transform.position + new Vector3(-40, -40, 0) + new Vector3(i * 40, 0, 0), Quaternion.identity).transform.SetParent(star);

            }
            Instantiate(starImages[0], obj.transform.position + new Vector3(-40, -40, 0) + new Vector3(2* 40, 0, 0), Quaternion.identity).transform.SetParent(star);
     

        }
        else if (index == 3)
        {
            for (int i = 0; i < index; i++)
            {
               Instantiate(starImages[1], obj.transform.position + new Vector3(-40, -40, 0) + new Vector3(i * 40, 0, 0), Quaternion.identity).transform.SetParent(star);

            }

        }

    }
    #endregion


    /// <summary>
    /// 向XML中写入已通关的关卡编号，获得的星星数量
    /// </summary>
    /// <param name="level"></param>
    /// <param name="star"></param>
    public void WriteXml(int level,int star)
    {
        print("data: " + dataNode.ChildNodes.Count);
        print("star的数量 : " + star);
        if(level> dataNode.ChildNodes.Count)
        {
            XmlElement user = Xdoc.CreateElement("level");
            dataNode.AppendChild(user);
            user.SetAttribute("id", level.ToString());
            XmlElement userStar = Xdoc.CreateElement("star");
            user.AppendChild(userStar);
            userStar.InnerText = star.ToString();
        }
        else
        {
            XmlNodeList tt = dataNode.ChildNodes;
          //  tt[level - 1].FirstChild.InnerText = "";
            tt[level - 1].FirstChild.InnerText = star.ToString();
            Image[] newStars = levelsBtn[level - 1].transform.parent.GetComponentsInChildren<Image>();
            for(int i=0;i<star;i++)
            {
                newStars[i + 2].sprite = starImages[1].GetComponent<Image>().sprite;
            }

        }
        Xdoc.Save(Application.dataPath + "/Resources/levelSelect.xml");
        print("save level!!!");
        GetComponent<PlayerManager>().Initize();  //初始化星星的数值

        // dataNode.FirstChild.InnerText = levelCount.ToString();
    }


    void LoadScene(int index)
    {
        print("加载关卡为:" + index);
       
  

        GetComponent<DontDestoryObj>().DontDestroy(index);

    }



}
