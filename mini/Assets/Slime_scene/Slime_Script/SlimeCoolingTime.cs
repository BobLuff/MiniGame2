using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 该脚本用来控制史莱姆抛出的冷却间隔
/// </summary>

public class SlimeCoolingTime : MonoBehaviour {

    [Header("史莱姆的冷却时间")]
    public float coolingTime = 5f;
 
    public Image m_normalImage;   //遮罩图片
    public Image m_maskImage;
    public float currentTime = 0;
   


	// Use this for initialization
	void Start () {
        SetImageActiveState(true, false);
        m_normalImage.fillAmount = 1;
	}
	

    public bool  UpdateImageNormal()
    {
        bool HavingSlime = false;
        if (m_maskImage.fillAmount >= 1)
        {

            currentTime = 0;
            SetImageActiveState(true, false);
            m_maskImage.fillAmount = 0;
            HavingSlime = true;
           
        }
       
        return HavingSlime;

    }

    public void SetImageActiveState(bool normalImage,bool maskImage)
    {
        m_normalImage.gameObject.SetActive(normalImage);
        m_maskImage.gameObject.SetActive(maskImage);
    }


    /// <summary>
    /// 该函数用来控制图标的刷新时间
    /// </summary>

    public void UpdateSlimeCreate()
    {
        if (currentTime <= coolingTime)
        {
            // 更新冷却
            currentTime += Time.deltaTime;
            // 显示冷却动画
            m_maskImage.fillAmount = currentTime / coolingTime;
        }
    }
}
