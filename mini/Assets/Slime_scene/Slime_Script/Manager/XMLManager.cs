using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XMLManager : MonoSingleton<XMLManager> {
    XmlDocument Xdoc = new XmlDocument();
    XmlElement root = null;
    XmlNode dataNode = null;
    private int levelCount;
    private  readonly string _path = "/levelSelect.xml";

    [Header("灰色和黄色星星的prefab")]
    [SerializeField] GameObject[] starImages;

    [Header("选择关卡界面的关卡按钮")]
    [SerializeField] GameObject[] levelsBtn;

    private void Awake()
    {
        string xmlpath = Application.dataPath + _path;
        if (File.Exists(xmlpath)==false)
        {
            Debug.Log("找不到XML文件，自动生成一份");
            CreateXml();
        }
    }

    private void CreateXml()
    {
        XmlDocument xml = new XmlDocument();
        //创建根节点
        XmlElement root = xml.CreateElement("root");
        //创建子节点
        XmlElement data = xml.CreateElement("data");
        //创建子节点
        XmlElement element = xml.CreateElement("level");
        element.SetAttribute("id", "1");
        XmlElement elementChild1 = xml.CreateElement("star");
        elementChild1.InnerText = "0";
        //把节点一层一层的添加至xml中，注意他们之间的先后顺序，这是生成XML文件的顺序
        element.AppendChild(elementChild1);
        data.AppendChild(element);
        root.AppendChild(data);
        xml.AppendChild(root);
        //最后保存文件
        xml.Save(Application.dataPath+_path);
        Debug.Log("创建成功！");
    }


    public void ReadXml () {
        int starNum = 0;
        levelCount = 0;
        Xdoc.Load(Application.dataPath+_path);                                //加载XML 文件                     
        root = Xdoc.DocumentElement;                                         //获取根节点
        dataNode = root.SelectSingleNode("data");                            //获取根节点下的子节点
        levelCount = dataNode.ChildNodes.Count;
        Debug.Log("当前可玩关卡总数： "+levelCount);
        for (int i=0;i<levelCount; i++)
        {
            var btn = levelsBtn[i];
            int index = i + 1;
            btn.SetActive(true);
            btn.name = index.ToString();
            starNum = int.Parse(dataNode.ChildNodes[i].InnerText);
            CreateStar(starNum, levelsBtn[i]);
        }
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
            XmlNodeList nodeLis = dataNode.ChildNodes;
            var starNum = nodeLis[level - 1].FirstChild.InnerText;
            if(int.Parse(starNum)<star)
            {
                nodeLis[level - 1].FirstChild.InnerText = star.ToString();
            }
        }
        Xdoc.Save(Application.dataPath + _path);
        Debug.Log("save level XML!!!");
    }
}
