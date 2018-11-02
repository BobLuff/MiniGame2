using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectPanel : MonoBehaviour {



    public void OnClickBtn(GameObject sender)
    { 
        var index = int.Parse(sender.name);
        UIObjManager.Instance.SetLevelSelectState(false);
        LevelManager.Instance.LoadScene(index);
    }



}
