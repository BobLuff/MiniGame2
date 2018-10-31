using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


/// <summary>
/// 控制人物行走投掷攻击等行为
/// </summary>

[RequireComponent(typeof(TrackGraphic))]
public class Player_Control : MonoBehaviour {

    public enum MySlime
    {
        Slime_Red,
        Slime_Yellow,
    }
    [Header("player准备投掷的slime")]

    [SerializeField] private MySlime mySlime1;

    [Header("player运动的最大速度")]
    public float MaxSpeed=8;//移动速度
    //[Header("player的运动速度")]
   // public float playerMoveForce = 20f;

    [Header("player的跳跃高度")]
    public float jumpForce=10;//跳跃高度


 
   // bool isGround;//判断是否在地面

    [Header("史莱姆的预制体")]
    [Header("0-Yellow；1-Red；")]
    public GameObject[] SlimePrefab;// 史莱姆
    [Header("史莱姆的出生点")]
    public GameObject SpawnPoint;

    //public float force = 10f;
    public bool facingRight = true;

   // [Header("UI-> Image，需手动拖拽进来")]
   [HideInInspector]
    public Red_CoolingTime UI_Red;
    [HideInInspector]
    public Yellow_CoolingTime UI_Yellow;

    private bool jump = false;
   


  //  public bool Having_Slime_Blue = true;
    public bool Having_Slime_Yellow = true;
    public bool Having_Slime_Red = true;
    private bool IsGround = false;

    private Rigidbody2D rb;

    [Header("红色史莱姆的sprite，0-灰色，1-红色")]
    public Sprite[] redImages;
    private bool chooseSlime = false;
    private Animator anim;


    //角色动画
  //  private PlayerAnimator myState;
   // private Rigidbody2D rb;



    public MySlime MySlime1
    {
        get
        {
            return MySlime11;
        }

        set
        {
            MySlime11 = value;
        }
    }

    public MySlime MySlime11
    {
        get
        {
            return mySlime1;
        }

        set
        {
            mySlime1 = value;
        }
    }


    // Use this for initialization
 
    private void Start()
    {
        chooseSlime = false;
        rb = GetComponent<Rigidbody2D>();
        MySlime1 = MySlime.Slime_Red;
        if(!UI_Red)
        {
            UI_Red = GameObject.Find("Canvas/Slime_Red").GetComponent<Red_CoolingTime>();
      
        }
        if (!UI_Yellow)
        {
            UI_Yellow = GameObject.Find("Canvas/Slime_Yellow").GetComponent<Yellow_CoolingTime>();
       
        }
        UI_Yellow.enabled = true;
        UI_Red.enabled = true;

        if (SceneManager.GetActiveScene().buildIndex==3)
        {
            UI_Red.m_normalImage.sprite = redImages[1];
            chooseSlime = true;
            MySlime1 = MySlime.Slime_Yellow;
        }
        else
        {
            MySlime1 = MySlime.Slime_Yellow;
            UI_Red.enabled = false;
            UI_Red.m_normalImage.sprite = redImages[0];
            UI_Yellow.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        }

        anim = GetComponent<Animator>();
      //  myState=GetComponent<PlayerAnimator>();

    }



    private void Update()
    {

        Having_Slime_Yellow = UI_Yellow.HavingYellow;
        Having_Slime_Red = UI_Red.HavingRed;
       // Physics2D.gravity = new Vector3(0, -35, 0);  // gravity= -35 其他的默认

        #region player进行跳跃，对地面和天花板进行的判断
        // Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //  IsGround = Physics2D.Raycast(transform.position, groundCheck.position, 0.5f, 1 << LayerMask.NameToLayer("Platform"));
        // IsGround = Physics2D.Linecast(startPoint.position, groundCheck.position, 1 << LayerMask.NameToLayer("Platform"));

        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
          
            jump = true;
       

            /*
            if (Physics2D.Raycast(transform.position, Vector2.up, 2f, LayerMask.GetMask("ceiling")))
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector3.down * jumpForce, ForceMode2D.Impulse);
                Debug.Log("IsCeiling");
            }
            */
            IsGround = false;

        }
  
        #endregion


        if(IsGround)
        {
           // myState.currentState = playerState.idle;
        }
    }
    private void FixedUpdate()
    {
      
        #region 跳跃
        if(jump)
        {
          
            Debug.Log("Jump");

            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //float x = transform.GetComponent<Rigidbody2D>().velocity.x;
            //transform.GetComponent<Rigidbody2D>().velocity=new Vector2(x, jumpForce);
            //myState.currentState = playerState.jump;
            jump = false;
           // rb.velocity = new Vector2(0, 10);
        }
  
        #endregion


        #region 选择不同的史莱姆
        if (chooseSlime)
        {
            if (Input.GetKey(KeyCode.Alpha1))                 //1--选择红色Slime
            {
                MySlime1 = MySlime.Slime_Yellow;
                UI_Red.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
                UI_Yellow.transform.DOScale(new Vector3(1, 1, 1), 0.3f);

                // Having_Slime_Red = false;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                MySlime1 = MySlime.Slime_Red;
                UI_Yellow.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
                UI_Red.transform.DOScale(new Vector3(1, 1, 1), 0.3f);

            }

        }

        #endregion


       /*
        if (Input.GetKey(KeyCode.A) ) //左
        {
            this.transform.Translate(Vector2.right * -playerMoveForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) //右
        {
            this.transform.Translate(Vector2.right * playerMoveForce * Time.deltaTime);
        }
        */
        
     

    
        float hor = Input.GetAxis("Horizontal");//行走
        rb.velocity = new Vector2(hor * MaxSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));



        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));
       




        //    anim.SetFloat("SPeed", Mathf.Abs(rig_player.velocity.x));
        // float h = Input.GetAxis("Horizontal");
        /*
         if (!hor.Equals(0))
         {
             rb.velocity = new Vector2(hor * playerMoveForce, rb.velocity.y);
         }
         if (Input.GetKeyDown(KeyCode.Space))
         {
             if (rb.velocity.y.Equals(0))
                 rb.velocity = new Vector2(rb.velocity.x, jumpForce);
         }
         */




        //  Vector2 direction = new Vector2(hor, 0f);

        // rb.velocity = playerMoveForce * direction;


        /*
   
        if (hor* rb.velocity.x<=MaxSpeed)
        {
            //print("111");
            rb.AddForce(Vector2.right * hor * playerMoveForce);
          //  myState.currentState = playerState.move;
        }
        if(Mathf.Abs(rb.velocity.x)>MaxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
           // myState.currentState = playerState.move;
        }

    */




        #region    进行player的转向
        if (hor>0&&!facingRight) {
       
            Flip();
           // print(1);
        }
        if (hor < 0&&facingRight)
        {
            Flip();
          //  print(2);
 
        }
        #endregion

        #region 生成slime的prefabs，并投掷
        if (Input.GetMouseButton(0))//投掷
        {
            //myState.currentState = playerState.attack;
            switch(MySlime1)
            {
                case MySlime.Slime_Red:  
                    if(Having_Slime_Red)
                    {
                        //  PlayAttackAnim();
                        StartCoroutine(PlayAttackAnim());
                        Instantiate(SlimePrefab[1], SpawnPoint.transform.position, Quaternion.identity);
         
                        //  mySlime = MySlime.idle;
                        Having_Slime_Red = false;
                        UI_Red.HavingRed = false;
                    }     
                    break;
                case MySlime.Slime_Yellow:
                    if(Having_Slime_Yellow)
                    {
                        StartCoroutine(PlayAttackAnim());
                       // PlayAttackAnim();
                        Instantiate(SlimePrefab[0], SpawnPoint.transform.position, Quaternion.identity);
                   
                        Having_Slime_Yellow = false;
                        UI_Yellow.HavingYellow = false;
                    }
                    break;
  
                default:
                    //return;
                    break;

            }

        }
        #endregion
    }

    IEnumerator PlayAttackAnim()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Attack", false);



    }

    /// <summary>
    /// Flip()用左右翻转player
    /// </summary>
   public  void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
	

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Platform"))
        {
            IsGround = true;
        }

    }

  



}
