using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager: MonoSingleton<LevelManager>{


    public void LoadScene(int index)
    {
        UIObjManager.Instance.Init();
        UIObjManager.Instance.SetLoadingPanelState(true);
        Debug.Log("加载关卡:" + index);
        SceneManager.LoadScene(index);
        UIManager.Instance.PlaySlimeUiAnim(SlimeStatus.Slime_Yellow, new Vector3(1f, 1f, 1f));
        UIManager.Instance.GetRedSlime(index);
        PlayerManager.Instance.Initize();
    }
	

}
