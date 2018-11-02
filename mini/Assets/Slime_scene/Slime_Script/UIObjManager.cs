using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///管理UI物体显示状态
/// </summary>

public class UIObjManager : MonoSingleton<UIObjManager> {

    [SerializeField]
    private GameObject _levelSelectPanel;
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private GameObject _loadingPanel;
    [SerializeField]
    private GameObject _resultPanel;
    [SerializeField]
    private GameObject _escPanel;

    public bool IsReloadXml = false;

    public void Init()
    {
        _levelSelectPanel.SetActive(false);
        _canvas.SetActive((false));
        _loadingPanel.SetActive(false);
        _resultPanel.SetActive(false);
        _escPanel.SetActive(false);
    }

    public void SetResultPanelState(bool isShow)
    {
        _resultPanel.SetActive(isShow);
    }


    public void SetLevelSelectState(bool isShow)
    {
        _levelSelectPanel.SetActive(isShow);
        if(isShow&&IsReloadXml)
        {
            XMLManager.Instance.ReadXml();
        }
    }

    public void SetCanvasState(bool isShow)
    {
        _canvas.SetActive(isShow);
    }


    public void SetLoadingPanelState(bool isShow)
    {
        _loadingPanel.SetActive(isShow);
        if (isShow == true)
        {
            StartCoroutine(HideLoading());
        }
    }

    private IEnumerator HideLoading()
    {
        yield return new WaitForSeconds(0.2f);
        _loadingPanel.SetActive(false);
        if(_canvas.activeInHierarchy==false)
        {
            _canvas.SetActive(true);
        }
    }


    }


