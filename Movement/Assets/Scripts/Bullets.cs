using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum BulletType
    {
        Fire,
        Explosive,
        Hollow,
        Rickochet,
        Piercing,
        Homing,
        Sheild
    }
public class Bullets : MonoBehaviour
{



    BulletType[] chamber = new BulletType[6];
    BulletType[] bulletsToShoot = new BulletType[6];
    GameObject[] enemiesToShoot = new GameObject[6];
    int bulletsShot=0;

    void ReloadChamber()
    {

        Array.Clear(chamber, 0, chamber.Length);//clears the array
        for(int count = 0; count <chamber.Length; count++)
        {
            int randBulletType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(BulletType)).Length);
            chamber[count] = (BulletType)randBulletType;
        }

    }

    public BulletType[] GetBulletsToShoot()
    {
        return bulletsToShoot;
    }
    public Enemy GetSelectedEnemies()
    {

    }
}
