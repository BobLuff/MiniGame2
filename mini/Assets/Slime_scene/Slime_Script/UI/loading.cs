using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour {
    [HideInInspector]
    public int sceneID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject.activeInHierarchy)
        {
            if(SceneManager.GetActiveScene().buildIndex==sceneID)
            {
                gameObject.SetActive(false);
            }

        
        }
		
	}
 
}
