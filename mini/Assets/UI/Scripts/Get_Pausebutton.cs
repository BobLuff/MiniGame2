using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Pausebutton : MonoBehaviour {
    [Header("暂停界面")]
    public RectTransform pauseUI;
    private RectTransform packagePanel;
    //[Header("暂停界面")]
    //public GameObject pauseUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& (packagePanel == null)) {
            packagePanel = Instantiate(pauseUI) as RectTransform;
            packagePanel.SetParent(this.transform.parent);
            packagePanel.position = new Vector3(0, 0, 0);
            packagePanel.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            packagePanel.localScale = new Vector3(1, 1, 1);
            packagePanel.anchoredPosition = new Vector2(0, 0);
        }
    }
}
