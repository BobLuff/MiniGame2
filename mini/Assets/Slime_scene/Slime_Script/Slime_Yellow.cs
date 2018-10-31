using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制黄色史莱姆的喷溅效果,其中粘性效果使用的插件脚本，可在黄色史莱姆的预制体的参数面板中查看
/// </summary>

    [RequireComponent(typeof(DestroyObject))]
public class Slime_Yellow : MonoBehaviour {
    [Header("出生点偏移量")]
    public Vector3 offsetPos;
    [Header("0bao1diu")]
    public AudioClip[] clip;
    [Header("黄色史莱姆死亡音效")]
    public GameObject sound;


   // public GameObject Bounce_Brick;
    SplatterSystem.AbstractSplatterManager splatter;
  //  private Bounce_Slime m_BounceSlime;
   // private bool IsShowPartile=false;   //是否展示喷溅效果



    // Use this for initialization
    void Awake () {
        splatter = GameObject.FindGameObjectWithTag("splatterMgr").GetComponent<SplatterSystem.MeshSplatterManager>();
      //  m_BounceSlime = transform.GetComponentInChildren<Bounce_Slime>();



    }   



    void Start()
    {
        float timer = GetComponent<DestroyObject>().timer;
        StartCoroutine(ShowSplatter(timer));
        this.gameObject.GetComponent<AudioSource>().clip = clip[1];
        this.gameObject.GetComponent<AudioSource>().Play();
    }


    /// <summary>
    /// 销毁自己并生成喷溅效果
    /// </summary>
    public void DestorySelf()
    {
        ShowSplatter();
        
        Destroy(gameObject);
    }

    public void DestorySelf( Vector3 pos)
    {
        ShowSplatter(pos);

        Destroy(gameObject);
    }



    /// <summary>
    /// 用于自然死亡时生成喷溅效果
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    IEnumerator ShowSplatter(float timer)
    {
        yield return new WaitForSeconds(timer-0.1f);
        ShowSplatter();
        
    }


    /// <summary>
    /// 生成喷溅效果的粒子
    /// </summary>

    void ShowSplatter()
    {
        //Debug.Log("11");
        splatter.Spawn(transform.position - offsetPos, Color.yellow);
        Instantiate(sound);
        // ShowSplatter();
    }
    /// <summary>
    /// 重载一个参数，便于控制喷溅效果的出生点
    /// </summary>
    /// <param name="pos"></param>

    void ShowSplatter(Vector3 pos)
    {
        //Debug.Log("11");
        splatter.Spawn(pos, Color.yellow);
        Instantiate(sound);
        // ShowSplatter();
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag( "Spine"))
        {
            print("Spine");
            DestorySelf();
            //this.gameObject.GetComponentInChildren<AudioSource>().Play();
        }
        if(coll.gameObject.CompareTag("Pumpkin"))
        {
            print("Pumpkin");
            DestorySelf(coll.gameObject.transform.position);
            //this.gameObject.GetComponentInChildren<AudioSource>().Play();
        }
        if(coll.gameObject.CompareTag("Platform"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

     
    }

    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Boom");
     
        splatter.Spawn(transform.position-new Vector3(0,0.5f,0),Color.red);
        //Instantiate(Bounce_Brick, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
    }

    */


}
