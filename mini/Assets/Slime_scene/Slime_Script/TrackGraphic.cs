﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用来绘制史莱姆抛出的轨迹
/// </summary>
 [RequireComponent(typeof(Player_Control))]
public class TrackGraphic : MonoBehaviour {
    [Header("史莱姆扔的最远距离")]
    public float distance = 3;                                        //抛物线的最远距离

    public LineRenderer TrajectoryLineRenderer;

    private Player_Control m_playerControl;
    private Vector3 mousePos;
    // public Vector2 segVelocity;
    public Vector2 SlinghotMiddleVector;

    private void Start()
    {
        TrajectoryLineRenderer.enabled = false;
        m_playerControl = transform.GetComponent<Player_Control>(); 

    }


    private void FixedUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TrajectoryLineRenderer.enabled = true;
     
           
       
            if (m_playerControl.facingRight)
            {
                Debug.DrawLine(mousePos, transform.position, Color.yellow);
               
                if (Vector2.Angle(Vector2.right,mousePos-transform.position)>=90)
                {
                    m_playerControl.Flip();
                }
                DisplayTrajectoryLineRenderer(distance, 1);

            }
            else
            {
                Debug.DrawLine(mousePos, transform.position, Color.yellow);
                if (Vector2.Angle(Vector2.right, mousePos - transform.position) <90)
                {
                    m_playerControl.Flip();
                }
                DisplayTrajectoryLineRenderer(distance, 1);
            }
        }
        else
        {
            TrajectoryLineRenderer.enabled = false;
        }
   

    }



    /// <summary>
    /// 显示轨迹的函数
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="flag"></param>
    void DisplayTrajectoryLineRenderer(float distance,float flag)
    {
        int segmentCount = 5;
    
        Vector2[] segments = new Vector2[segmentCount];
        Vector2 mouse_Pos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        segments[0] = TrajectoryLineRenderer.gameObject.transform.position;     //线的起始位置
        Vector2 v2 =(mouse_Pos - segments[0]).normalized;
        v2 = v2 * flag;
        SlinghotMiddleVector = v2;

        Debug.DrawLine(segments[0], mousePos, Color.red);
        Vector2 segVelocity = new Vector2(v2.x, v2.y) * 2 * distance;

        for (int i = 1; i < segmentCount; i++)
        {
            float time2 = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
        }

        TrajectoryLineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            TrajectoryLineRenderer.SetPosition(i, segments[i]);
        }
    }



}





