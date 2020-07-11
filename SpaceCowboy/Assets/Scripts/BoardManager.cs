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

    public int columns = 9;
    public int rows = 10;


    public GameObject[] floortiles;

    public Transform boardholder;
    private List<Vector3> gridPositions = new List<Vector3>();

    public void InitialiseGridSpaces()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    public void PlayAreaSetup()
    {

        boardholder = new GameObject("GameGrid").transform;

        for (int x = 1; x < columns + 1; x++)
        {
            for (int y = 1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floortiles[Random.Range(0, floortiles.Length)];


                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardholder);

            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {

        PlayAreaSetup();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
