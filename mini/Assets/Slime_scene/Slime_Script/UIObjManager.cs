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
        if (_resultPanel.activeInHierarchy)
        {
            _resultPanel.SetActive(false);
        }
        if (_escPanel.activeInHierarchy)
        {
            _escPanel.SetActive(false);
        }
        if (_levelSelectPanel.activeInHierarchy)
        {
            _levelSelectPanel.SetActive(false);
        }
        if(_canvas.activeInHierarchy)
        {
            _canvas.SetActive((false));
        }
        if(_loadingPanel.activeInHierarchy)
        {
            _loadingPanel.SetActive(false);
        }

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


