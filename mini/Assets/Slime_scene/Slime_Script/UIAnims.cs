using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 该代码用于控制ESC面板中，几个按钮的动画的播放，
/// 动画包括放大，图片透明度改变
/// </summary>

public class UIAnims : MonoBehaviour {
    [Header("---控制ESC面板中按钮动画的播放---")]

    [Header("缩放动画的时间")]
    [Range(0,2)]  public float scaleTime=0.6f;
    [Header("透明度渐变的时间")]
    [Range(0, 2)] public float fadeTime=0.4f;

    private Image[] imgs;      //会获取到父物体中的Image

    /// <summary>
    /// 对按钮进行初始化操作，包括设置按钮初始大小，透明度
    /// </summary>

    public void InitizeAnim()
    {
        imgs[1].transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        for(int i=1;i<5;i++)
        {
            imgs[i+1].transform.localScale = new Vector3(1 - i * 0.2f, 1 - i * 0.2f, 1);
        }
        for (int i = 1; i < 6; i++)
        {
            imgs[i].DOFade(0.1f, 0.1f);

        }


    }


    public void PlayAnims()
    {
        imgs = GetComponentsInChildren<Image>();
        InitizeAnim();

        for (int i = 1; i < 6; i++)
        {
            StartCoroutine(PlayAnims(imgs[i].transform));

        }

    }


    /// <summary>
    /// 播放Esc面板中按钮的动画
    /// 使用的Dotween插件
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    IEnumerator PlayAnims( Transform obj)
    {  
        obj.DOScale(new Vector3(1, 1, 1), scaleTime).SetUpdate(true);
        obj.GetComponent<Image>().DOFade(1f, fadeTime);
        yield return new WaitForSeconds(0.4f);

    }
}
