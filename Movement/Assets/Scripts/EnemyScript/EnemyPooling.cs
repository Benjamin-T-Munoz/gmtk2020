using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPooling : MonoBehaviour
{

    public List<GameObject> Enemies = new List<GameObject>();
    public List<bool> EnemiesActive = new List<bool>();
    public GameObject[] enemies;
    public Vector3 deathPosition;

    public GameObject enemyholder;

    public GameObject Spawn(Vector3 spawnlocation)
    {
        GameObject enemy=null;
        Enemy brain;
        for (int i=0;i<Enemies.Count;i++)
        {
            if(!EnemiesActive[i])
            {
                brain = Enemies[i].GetComponent<Enemy>();
                EnemiesActive[i] = true;
                Enemies[i].SetActive(true);
                Enemies[i].transform.position = spawnlocation;

                brain.RespawnAt(spawnlocation);

                return Enemies[i];
            }
        }
        if (enemy == null)
        {
            int index = MakeNewEnemy();
            enemy = Enemies[index];
            Enemies[index].SetActive(true);
            Enemies[index].transform.position = spawnlocation;
            brain = Enemies[index].GetComponent<Enemy>();
            brain.RespawnAt(spawnlocation);
            
        }

        return enemy;
    }


    public int MakeNewEnemy()
    {
        GameObject toInstantiate = enemies[Random.Range(0, enemies.Length)];
        GameObject instance = Instantiate(toInstantiate,new Vector3(0,0,0), Quaternion.identity) as GameObject;
        
        instance.transform.SetParent(enemyholder.transform);
        Enemies.Add(instance);
        EnemiesActive.Add(true);

        return Enemies.IndexOf(instance);
    }


    public void Kill(GameObject enemyDied)
    {
        
        Enemy brain = enemyDied.GetComponent<Enemy>();
        brain.deathPosition = deathPosition;
        brain.Kill();
        EnemiesActive[Enemies.IndexOf(enemyDied)] = false;
    }

    
    

    



}
