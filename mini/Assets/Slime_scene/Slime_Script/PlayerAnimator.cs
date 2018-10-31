using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum  playerState
{
    idle,
    move,
    jump,
    attack
}

public class PlayerAnimator : MonoBehaviour {
    public playerState currentState;
    Animator m_Animator;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        currentState = playerState.idle;
  

    }
	
	// Update is called once per frame
	void Update () {
        switch(currentState)
        {
            case playerState.idle:
               //  m_Animator.SetBool("idle", true);
               // m_Animator.SetBool("jump", false);

                break;
            case playerState.move:
        
              //  m_Animator.SetBool("move", true);
       
                break;
            case playerState.jump:
               // print("jump");
          
               // m_Animator.SetBool("jump", true);

         
                break;
            case playerState.attack:
          
               // m_Animator.SetBool("attack", true);
         
                break;
            default:
                break;

        }

    }
}
