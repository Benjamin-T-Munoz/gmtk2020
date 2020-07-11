using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // The Values controling enemy spawn counts
    public int MaxNumberOfEnemies = 10;
    public int MinimumNumberOfEnemies = 6;
    public int ThreatCount = 6;


    //The Values controlling board size
    public int columns = 18;
    public int rows = 20;

    // The Number Of Spaces infront of an enemy that must be free for it to be an active threat;
    public int LOSSpacing = 1;


    

    public List<GameObject> enemiesOnBoard = new List<GameObject>();
    public List<GameObject> activeThreats= new List<GameObject>();

    public Transform boardholder;
    public Transform enemyholder;

    public GameObject[] floortiles;
    public GameObject[] enemies;

    private List<Vector3> gridPositions = new List<Vector3>();
    private List<Vector3> gridSpawnPositions= new List<Vector3>();
    private List<Vector3> occupideGridPositions = new List<Vector3>();
    public void InitialiseGridSpaces()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int z = 1; z < rows - 1; z++)
            {
                if(z>rows/2&& x!=columns-1&&x!=0)
                {
                    gridSpawnPositions.Add(new Vector3(x, 0f, z));
                }
                gridPositions.Add(new Vector3(x, 0f, z));
            }
        }
    }


    public void DisplayPlayAreaSetup()
    {

        boardholder = new GameObject("GameGrid").transform;

        for (int x = 1; x < columns + 1; x++)
        {
            for (int z = 1; z < rows + 1; z++)
            {
                GameObject toInstantiate = floortiles[Random.Range(0, floortiles.Length)];


                GameObject instance = Instantiate(toInstantiate, new Vector3(x, z, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardholder);


            }
        }
    }


    #region Enemy Spawning and Movement

    public void SpawnEnemies()
    {
        enemyholder = new GameObject("Enemies").transform;

        for (int i=0;i < Random.Range(MinimumNumberOfEnemies,MaxNumberOfEnemies);i++)
        {
            SpawnEnemy();
        }

        ChooseActiveThreat();
    }

    public void SpawnEnemy()
    {

        GameObject toInstantiate = enemies[Random.Range(0, enemies.Length)];
        var gridPosition = RandomPosition();
        
        GameObject instance = Instantiate(toInstantiate,gridPosition, Quaternion.identity) as GameObject;
        instance.transform.SetParent(enemyholder);
        occupideGridPositions.Add(gridPosition);
        enemiesOnBoard.Add(instance);
        Debug.Log(instance.name);

    }


    public GameObject SpawnEnemyAt(Vector3 location)
    {

        GameObject toInstantiate = enemies[Random.Range(0, enemies.Length)];
       

        GameObject instance = Instantiate(toInstantiate,location, Quaternion.identity) as GameObject;
        instance.transform.SetParent(enemyholder);
        occupideGridPositions.Add(location);
        enemiesOnBoard.Add(instance);
        Debug.Log(instance.name);
        return instance;

    }


    public void ChooseActiveThreat()
    {
        List<GameObject> activeThreatsPool= new List<GameObject>();
        activeThreats.Clear();

        for(int i=0;i< enemiesOnBoard.Count;i++)
        {
            var currentposition = enemiesOnBoard[i].GetComponent<Enemy>().currentPosition;
            var losFree = new Vector3(currentposition.x , currentposition.y, currentposition.z - LOSSpacing);

            bool free = CheckLOS(currentposition, losFree);

            Debug.Log(enemiesOnBoard[i].name.ToString()+free);

            if (free)
            {
                activeThreatsPool.Add(enemiesOnBoard[i]);
            }
            else
            {
                Debug.Log(enemiesOnBoard[i].name.ToString());
            }
        }


        if(activeThreatsPool.Count>=ThreatCount)
        {

            for (int i = 0; i < ThreatCount; i++)
            {

                int randomIndex = Random.Range(0, activeThreatsPool.Count);

                activeThreats.Add(activeThreatsPool[randomIndex]);
                
            
                activeThreats[i].GetComponent<Enemy>().activeThreat = true;
                activeThreatsPool.RemoveAt(randomIndex);
            }

        }
        else
        {
            var diffrence = ThreatCount - activeThreatsPool.Count;
            var freeSpaces = gridSpawnPositions;
            for (int i = 0; i < occupideGridPositions.Count; i++)
            {
                freeSpaces.Remove(occupideGridPositions[i]);
            }

            for(int i=0; i<freeSpaces.Count;i++)
            {

                var losFree = new Vector3(freeSpaces[i].x, freeSpaces[i].y, freeSpaces[i].z - LOSSpacing);

                if (!CheckLOS(freeSpaces[i], losFree))
                {
                    freeSpaces.RemoveAt(i);
                }
            }

            for (int i = 0; i<diffrence;i++)
            {

                int randomIndex = Random.Range(0, freeSpaces.Count);

                var enemy=SpawnEnemyAt(freeSpaces[randomIndex]);

                freeSpaces.RemoveAt(randomIndex);

                activeThreatsPool.Add(enemy);

            }

            for (int i = 0; i < ThreatCount; i++)
            {

                int randomIndex = Random.Range(0, activeThreatsPool.Count);

                activeThreats.Add(activeThreatsPool[randomIndex]);
                activeThreatsPool.RemoveAt(randomIndex);

                activeThreats[i].GetComponent<Enemy>().activeThreat = true;
            }





        }
    }


    public bool BangAction(GameObject[] targets, GameObject[] bullets)
    {

       
        for(int i=0;i<targets.Length ; i++)
        {



            var hitposition = targets[i].GetComponent<Enemy>().currentPosition;

            

            switch(bullets[i])
            {

            }

        }
        

        if( activeThreats.Count>0)
        {

        }

         




        return true;
    }


    #region Checking Location

    // Checks to see if their is another enemy directly infroint of the point
    public bool CheckLOS(Vector3 position, Vector3 target)
    {
        int layerMask = 1 << 9;
        RaycastHit raycastHit ;
       
        if (Physics.Linecast(position, target, out raycastHit,layerMask) )
        {
            Debug.DrawLine(position, target);
            Debug.Log(raycastHit.ToString());
            return false;// LOS blocked cant be the shooter
        }
        else
            return true; // LOS not Blocked Can be shooter
    }



    #endregion







    #endregion


    

    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridSpawnPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridSpawnPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridSpawnPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        InitialiseGridSpaces();
        SpawnEnemies();

       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
