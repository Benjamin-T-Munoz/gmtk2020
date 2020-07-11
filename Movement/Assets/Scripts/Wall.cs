using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    float timeToRotate;
    float time;
    float rotateDirection;
    bool needsRotateFix;
    WallManager wallmanager;

    // Start is called before the first frame update
    void Start()
    {
        wallmanager = this.transform.GetComponentInParent<WallManager>();
        needsRotateFix = false;
        rotateDirection = this.transform.position.x > 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        float intervals = GameManager.manager.getIntervals();
        timeToRotate = (10* intervals)/wallmanager.getUnitsToMove();
        //Debug.Log("timeToRotate: " + intervals + ", " + wallmanager.getUnitsToMove() + "= " + timeToRotate);
        if(this.transform.position.z <0)
        {
            MoveToBackAndRotate();
            time = timeToRotate;
            needsRotateFix = true;
        }

        if(time<=0 && needsRotateFix)
        {
            FixRotate();
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    void MoveToBackAndRotate()
    {
        Vector3 movement = new Vector3(1.5f * -rotateDirection ,0,330);
        this.transform.position += movement;
        this.transform.Rotate(Vector3.forward, 45 * rotateDirection);
    }

    void FixRotate()
    {
        Vector3 movement = new Vector3(1.5f * rotateDirection, 0, 0);
        this.transform.position += movement;
        this.transform.Rotate(Vector3.forward, 45 * -rotateDirection);
        needsRotateFix = false;
    }
}
