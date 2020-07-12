using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum BulletType
    {
        Fire,
        Explosive,
        Hollow,
        Ricochet,
        Piercing,
        Homing,
        Shield
    }
public class Bullets : MonoBehaviour
{

    BulletType[] chamber = new BulletType[6];
    BulletType[] bulletsToShoot = new BulletType[6];
    List<BulletType> allBullets = new List<BulletType>();
    List<GameObject> enemiesToShoot = new List<GameObject>();
    int bulletsShot=0;

    private void Start()
    {
        Reset();
    }

    void ReloadChamber()
    {
        allBullets.Clear();
        for (int count = 0; count < Enum.GetNames(typeof(BulletType)).Length; count++)
        {
            allBullets.Add((BulletType)count);
        }
        for(int count = 0; count<chamber.Length; count++)
        {
            int randomBulletType = UnityEngine.Random.Range(0,allBullets.Count);
            chamber[count] = allBullets[randomBulletType];
            allBullets.RemoveAt(randomBulletType);
        }
    }

    public void Shoot(GameObject enemyShot)
    {
        if (bulletsShot < chamber.Length)
        {
            Debug.Log("BANG BANG MUTHAFUKA");
            bulletsToShoot[bulletsShot] = chamber[bulletsShot];
            bulletsShot++;
            enemiesToShoot.Add(enemyShot);
        }
        
    }

    public BulletType[] GetBulletsToShoot()
    {
        return bulletsToShoot;
    }
    public List<GameObject> GetSelectedEnemies()
    {
        return enemiesToShoot;
    }

    public int getBulletsShot()
    {
        return bulletsShot;
    }

    public void Reset()
    {
        bulletsShot = 0;
        ReloadChamber();
        enemiesToShoot.Clear();
        Array.Clear(bulletsToShoot,0,bulletsToShoot.Length);
    }
}
