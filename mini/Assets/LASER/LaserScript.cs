using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
  [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    private GameObject start;
    private GameObject middle;
    private GameObject end;
    private PlayerManager m_PlayerManager;

    void Start()
    {
        m_PlayerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    void Update()
    {
        // Create the laser start from the prefab
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
            start.transform.localPosition = new Vector2(0, -0.035f);
            Vector3 xx = this.transform.localRotation.eulerAngles;
            xx.z += 90;
            start.transform.localRotation = Quaternion.Euler(xx);
        }

        // Laser middle
        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
            Vector3 xx = this.transform.localRotation.eulerAngles;
            xx.z += 90;
            middle.transform.localRotation=Quaternion.Euler(xx);
        }

        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 30f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize);

        if (hit.collider != null)
        {
            // We touched something!

            // -- Get the laser length
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);
        //    Debug.Log(hit.point);
            // -- Create the end sprite
            if (end == null)
            {
                end = Instantiate(laserEnd) as GameObject;
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
                Vector3 yy = this.transform.localRotation.eulerAngles;
                yy.z -= 90;
                end.transform.localRotation=Quaternion.Euler(yy);
            }
            if (hit.transform.tag == "Player") { m_PlayerManager.IsDead = true; }
        }
        else
        {
            // Nothing hit
            // -- No more end
            if (end != null) Destroy(end);
        }
        // Place things
        // -- Gather some data
        float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.x;
        float endSpriteWidth = 0f;
        if (end != null) endSpriteWidth = end.GetComponent<Renderer>().bounds.size.x;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        middle.transform.localScale = new Vector3(middle.transform.localScale.x,currentLaserSize*3.75f, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2((currentLaserSize*0.77f), -0.02f);

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(currentLaserSize*1.54f, 0f);
        }

    }
}
