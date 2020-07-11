using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{

    float animationTime;
    Vector3 startingPosition;
    float time;
    float direction = 1;
    float lerpPercent;

    // Start is called before the first frame update
    void Start()
    {
        animationTime = GameManager.manager.getIntervals();
        GameManager.manager.stepForward += MoveVisuals;
        startingPosition = this.transform.position;
    }

    void Update()
    {
        if(direction == 1)
        {
            time += Time.deltaTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
        Debug.Log(time);
        MoveVisuals();
    }

    void MoveVisuals()
    {
        lerpPercent = time/(animationTime/2);
        this.transform.position = Vector3.Lerp(startingPosition, startingPosition + Vector3.up * .25f, lerpPercent);
        if(time < 0)
        {
            direction = 1;
            animationTime = GameManager.manager.getIntervals();
        }
        if(time>animationTime/2)
        {
            direction = -1;
        }
    }
}
