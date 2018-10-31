using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制敌人行走跳跃及其范围
/// </summary>
public class Enemy_Control : MonoBehaviour {
    [Tooltip("移动速度")]
    public float MaxSpeed = 1;//移动速度
    [Tooltip("左侧移动边界")]
    public Transform leftMove;//左侧移动边界
    [Tooltip("右侧移动边界")]
    public Transform rightMove;//右侧移动边界
    private Vector3 moveDirection ;//角色行走方向
    private bool turnLeft=true;
    private bool turnRight=true;
    [Tooltip("跳跃开关")]
    public bool jump = false;//跳跃开关
    [Tooltip("飞行开关")]
    public bool fly = false;//飞行开关
    [Tooltip("跳跃高度")]
    public float jumpForce = 10;//跳跃高度
    [Tooltip("跳跃时间间隔")]
    public float jumpTimer = 5;//跳跃时间间隔
    private float jumpTime;//跳跃计时器
    private Rigidbody2D rig_player;//获取自身刚体
    [Tooltip("行走动画")]
    public Animator moveAnim;//行走动画
    [Tooltip("跳跃动画")]
    public Animator jumpAnim;//跳跃动画
    [Tooltip("Player—底部位置，用于检测是否在地面")]
    public Transform checkGround;//Player底部位置，用于检测是否在地面
    [Tooltip("检测可以向上跳跃的层级")]
    public LayerMask groundLayer;//检测可以向上跳跃的层级
    bool isGround;//判断是否在地面
    [Tooltip("获取敌人刚体")]
    public Rigidbody2D rig_enemy;//获取敌人刚体
    [Tooltip("敌人死亡延时")]
    public float die_timmer=0.1f;//敌人死亡延时
    [Tooltip("敌人死亡效果出现延时")]
    public float exp_timmer = 0.0f;//敌人死亡效果出现延时
    [Tooltip("死亡效果")]
    public GameObject exp ;//敌人死亡效果

    //获取敌人比例
    float scale_x;
    float scale_y;
    float scale_z;
    
    private void Awake()
    {
        rig_player = GetComponent<Rigidbody2D>();
        moveDirection = Vector3.right;//角色行走方向
        //anim = GetComponent<Animator>();
        scale_x = GetComponent<Transform>().localScale.x;
        scale_y = GetComponent<Transform>().localScale.y;
        scale_z = GetComponent<Transform>().localScale.z;
        
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(moveDirection * MaxSpeed * Time.deltaTime);
        isGround = Physics2D.OverlapCircle(checkGround.position, 0.1f, groundLayer);
        if (fly == true)
        {
            if (this.gameObject.transform.position.x <= leftMove.position.x && turnRight)
            {
                moveDirection = moveDirection * (-1);
                scale_x = scale_x * (-1);
                transform.localScale = new Vector3(scale_x, scale_y, scale_z);
                turnRight = false;
                turnLeft = true;
            }
            if (this.gameObject.transform.position.x >= rightMove.position.x && turnLeft)
            {
                moveDirection = moveDirection * (-1);
                scale_x = scale_x * (-1);
                transform.localScale = new Vector3(scale_x, scale_y, scale_z);
                turnLeft = false;
                turnRight = true;
            }
        }
        else {
            //行走部分
            //anim.SetFloat("Speed", Mathf.Abs(rig_enemy.velocity.x));
            
            if (this.gameObject.transform.position.x <= leftMove.position.x && isGround & turnRight)
            {
                moveDirection = moveDirection * (-1);
                scale_x = scale_x * (-1);
                transform.localScale = new Vector3(scale_x, scale_y, scale_z);
                turnRight = false;
                turnLeft = true;
            }
            if (this.gameObject.transform.position.x >= rightMove.position.x && isGround & turnLeft)
            {
                moveDirection = moveDirection * (-1);
                scale_x = scale_x * (-1);
                transform.localScale = new Vector3(scale_x, scale_y, scale_z);
                turnLeft = false;
                turnRight = true;
            }
            //跳跃部分
            //anim.SetFloat("JumpSpeed", Mathf.Abs(rig_enemy.velocity.y));
            jumpTime += Time.deltaTime;
            if (jumpTime >= jumpTimer && isGround && jump)//跳跃
            {
                rig_player.velocity = new Vector2(rig_player.velocity.x, jumpForce);
                jumpTime = 0;
            }
        }
             
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void des() {
        Destroy(transform.parent.gameObject, die_timmer);
        
    }

    public void Exp() {
        StartCoroutine(AutoGem());
    }
    IEnumerator AutoGem()
    {
        yield return new WaitForSeconds(exp_timmer);
        explo();
    }
    private void explo() {
        Instantiate(exp, transform.position, Quaternion.identity);

    }
}
