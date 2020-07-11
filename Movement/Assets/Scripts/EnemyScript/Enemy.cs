using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject gameObject;
    public int index { get; set; }
    public Vector3 currentPosition { get; set; }


    public bool activeThreat=false;
    bool isTargeted = false;

    private BoxCollider boxCollider;         //The BoxCollider2D component attached to this object.
    private Rigidbody rb;                //The Rigidbody2D component attached to this object.
    





    public void Awake()
    {
        gameObject = this.transform.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody>();
    }



    private void Update()
    {
        if(isTargeted)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }


    public void Move()
    {
       

    }

    public void Die()
    {

    }
    
    public void Targeted(bool active)
    {
        isTargeted = active;
    }











}
