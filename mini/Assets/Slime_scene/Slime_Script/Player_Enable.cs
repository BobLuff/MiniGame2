using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enable : MonoBehaviour {
    private UIManager m_UIManager;

	// Use this for initialization
	void Start () {
        m_UIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();


    }
	
	// Update is called once per frame
	void Update () {
        if(m_UIManager.IsEnablePlayer)
        {
            GetComponent<Player_Control>().enabled = false;
            GetComponent<TrackGraphic>().enabled = false;
        }
        else
        {
            if (GetComponent<TrackGraphic>().enabled == false)
            {
                GetComponent<TrackGraphic>().enabled = true;
            }
            if (GetComponent<Player_Control>().enabled == false)
            {
                GetComponent<Player_Control>().enabled = true;
            }
          
         
        }
		
	}
}
