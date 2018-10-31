using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 当player跳到该物体上，给player一个向上的弹力
/// 
/// </summary>


public class Bounce_Slime : MonoBehaviour {
    [Header("向上跳的力的大小:1-20")]
    [Range(1, 20)] public float UpForce = 5f;
    [Header("X方向的力：0-5")]
    [Range(0, 5)] public float xForce;
    [Header("Y方向力：0-10")]
    [Range(0, 10)] public float yForce;
  //  public bool IsBounce = false;     //是否进行弹跳
    public GameObject m_Player;

    public Slime_Yellow m_SlimeYellow;

    [Header("2段跳上限速度")]
    public float speed=1;

    private float speeder;

    // Use this for initialization
    void Start () {
        m_SlimeYellow = transform.GetComponentInParent<Slime_Yellow>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
      
        if(m_Player==null)
        {
            Debug.LogError("Player is null");
            return;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        speeder = (-1) * m_Player.GetComponent<Rigidbody2D>().velocity.y;

        // Debug.DrawLine(transform.position + new Vector3(0, 1f, 0), transform.position + new Vector3(0, 0.5f, 0), Color.red);
        // Physics2D.LinecastNonAlloc()
        /*
        if (Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("ground")))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("IsGround");
        }
        */
        //  IsBounce = 
        //  Debug.DrawRay(transform.position, Vector2.up, Color.red);
        // IsBounce = Physics2D.Linecast(transform.position+new Vector3(0,3f,0), transform.position + new Vector3(0,4f,0), 1<<LayerMask.GetMask("Player"), 2f);

        //   Debug.Log(IsBounce);



    }

    /// <summary>
    /// 进行弹跳
    /// </summary>

    void PlayerBounce()
    {
        Debug.Log("Player is bouncing");

            //   Debug.Log(m_Player.GetComponent<Player_Control>().facingRight);

            if (m_Player.GetComponent<Player_Control>().facingRight)
            {
           // Debug.DrawLine()
                m_Player.GetComponent<Rigidbody2D>().velocity=new Vector2( xForce*UpForce,yForce*UpForce+speeder);
            }
            else
            {
                m_Player.GetComponent<Rigidbody2D>().velocity= new Vector2(-xForce * UpForce, yForce * UpForce + speeder);
        }


    }




    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jump"))
        {
            //  Debug.Log("Player is bounce!!");
           
            Debug.Log(speeder);
            //this.gameObject.GetComponent<AudioSource>().Play();
            if (speeder <= speed)
            {
                Debug.Log("nomal" + speeder);
                PlayerBounce();
                m_SlimeYellow.DestorySelf();
            }
            else {
                speeder = speed;
                Debug.Log("big" + speeder);
                PlayerBounce();
                m_SlimeYellow.DestorySelf();
            }
           


            // m_Player.transform.position += new Vector3(0, 0.5f, 0);
        }
    }

    

    
}
