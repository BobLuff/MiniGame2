using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flip_Flop : MonoBehaviour {

    public UnityEvent Touch;
    public UnityEvent Disappeart;
    public string Tagname;
    public float time=5;
    [Header("是否过段时间恢复原样")]
    public bool cover=true;
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tagname))
        {
            Touch.Invoke();
            if (cover) {
                StartCoroutine(test());
            }
          
        }
        

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tagname))
        {
            //Disappeart.Invoke();
            //StartCoroutine(test());
        }
       
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(time);
        Disappeart.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Tagname))
        {
            Touch.Invoke();
        }
    }
    private void OnTriggerOut2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tagname))
        {
            Disappeart.Invoke();
        }
    }
}
