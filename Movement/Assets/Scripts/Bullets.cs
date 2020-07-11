﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    enum BulletType
    {
        Fire,
        Explosive,
        Hollow,
        Rickochet,
        Piercing,
        Homing
    }

    BulletType[] chamber = new BulletType[6];
    BulletType[] bulletsShot = new BulletType[6];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReloadChamber()
    {

        Array.Clear(chamber, 0, chamber.Length);//clears the array
        for(int count = 0; count <chamber.Length; count++)
        {
            int randBulletType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(BulletType)).Length);
            chamber[count] = (BulletType)randBulletType;
        }

    }
}
