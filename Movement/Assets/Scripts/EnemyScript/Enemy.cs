using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject gameObject;
    public int index { get; set; }
    public Vector3 currentPosition { get; set; }
    public Vector3 deathPosition { get; set; }

    public bool activeThreat=false;
    bool targeted = false;
    public bool selected = false;

    private BoxCollider boxCollider;         //The BoxCollider2D component attached to this object.
    private Rigidbody rb;                //The Rigidbody2D component attached to this object.


    SpriteRenderer renderer;



    public void Awake()
    {
        gameObject = this.transform.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody>();
        renderer = this.GetComponent<SpriteRenderer>();
    }

    internal void RespawnAt(Vector3 spawnlocation)
    {
        currentPosition = spawnlocation;
    }

    private void Update()
    {
        if(targeted || selected)
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.white;
        }
    }

    internal void Kill()
    {
        gameObject.SetActive(false);

        currentPosition = Vector3.zero;

        activeThreat = false;
        targeted = false;
        selected = false;
        gameObject.transform.position = deathPosition;
    }

    public void Targeted(bool isTargeted)
    {
        targeted = isTargeted;
    }
    public void Selected(bool isSelected)
    {
        selected = isSelected;
    }
}
