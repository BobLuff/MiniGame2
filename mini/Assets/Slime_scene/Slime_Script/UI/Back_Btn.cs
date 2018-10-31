using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back_Btn : MonoBehaviour {
    public Button backBtn;
    public GameObject hideObj;
 //   public GameObject showObj;

    void Start()
    {
        backBtn.onClick.AddListener(Back);

    }

    

    public void Back()
    {
        hideObj.SetActive(false);
      //  showObj.SetActive(true);

    }
}
