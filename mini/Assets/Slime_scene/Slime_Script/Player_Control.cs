using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


/// <summary>
/// 控制人物行走投掷攻击等行为
/// </summary>
/// 
public enum SlimeStatus
{
    Slime_Red,
    Slime_Yellow,
}

[RequireComponent(typeof(TrackGraphic))]
public class Player_Control : MonoBehaviour {


    [Header("player准备投掷的slime")]


    [Header("player运动的最大速度")]
    [SerializeField]
    private float MaxSpeed=8;                           //移动速度

    [Header("player的跳跃高度")]
    [SerializeField]
    private float jumpForce=10;                        //跳跃高度


    [Header("史莱姆的预制体")]
    [Header("0-Yellow；1-Red；")]
    public GameObject[] SlimePrefab;                  // 史莱姆
    [Header("史莱姆的出生点")]
    public GameObject SpawnPoint;

    [HideInInspector]
    public bool facingRight = true;




   

    public SlimeStatus CurSlime { get; set; }

    private bool IsGround = false;
    private bool jump = false;

    private Rigidbody2D rb;
    private Animator anim;

    private readonly Vector3 _normalScale = new Vector3(0.8f, 0.8f, 1f);
    private readonly Vector3 _enlargeScale = new Vector3(1f, 1f, 1f);








    // Use this for initialization

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CurSlime = SlimeStatus.Slime_Yellow;

    }



    private void Update()
    {


        #region player进行跳跃，对地面和天花板进行的判断


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
            jump = false;
        }

        #endregion

        #region 选择不同的史莱姆
        if (Input.GetKey(KeyCode.Alpha1))                                   //1--选择黄色Slime
        {
            if(UIManager.Instance.HaveYellowSlime)
            {
                CurSlime = SlimeStatus.Slime_Yellow;
                UIManager.Instance.PlaySlimeUiAnim(CurSlime, _enlargeScale);
                UIManager.Instance.PlaySlimeUiAnim(SlimeStatus.Slime_Red, _normalScale);
            }

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if(UIManager.Instance.HaveRedSlime&&UIManager.Instance.IsGetRedSlime)
            {
                CurSlime = SlimeStatus.Slime_Red;
                UIManager.Instance.PlaySlimeUiAnim(CurSlime, _enlargeScale);
                UIManager.Instance.PlaySlimeUiAnim(SlimeStatus.Slime_Yellow, _normalScale);
            }
        }
        #endregion




        float hor = Input.GetAxis("Horizontal");//行走
        rb.velocity = new Vector2(hor * MaxSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));
       



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
            switch(CurSlime)
            {
                case SlimeStatus.Slime_Red:  
                    if(UIManager.Instance.HaveRedSlime)
                    {
                        StartCoroutine(PlayAttackAnim());
                        InstantiateSlime(SlimePrefab[1]);
                        
                    }     
                    break;
                case SlimeStatus.Slime_Yellow:
                    if(UIManager.Instance.HaveYellowSlime)
                    {
                        StartCoroutine(PlayAttackAnim());
                        InstantiateSlime(SlimePrefab[0]);
                    }
                    break;
  

            }

        }
        #endregion
    }

    private void InstantiateSlime(GameObject slime)
    {
        Instantiate(slime, SpawnPoint.transform.position, Quaternion.identity);
        UIManager.Instance.SetSlimeCD(CurSlime);
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
