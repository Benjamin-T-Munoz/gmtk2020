﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public BoardManager boardManager;
    public WallManager wallManager;

    public Player player;
    public Bullets playerGun;

    public event Action stepForward;
    float time;
    [SerializeField]
    float intervals;

    // Start is called before the first frame update
    void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }
        time = intervals;
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = intervals;
            StepForward();
        }
    }



    public void StepForward()
    {

        wallManager.StepForward();
        boardManager.StepForward();

    }


    public float getIntervals()
    {
        return intervals;
    }
}
