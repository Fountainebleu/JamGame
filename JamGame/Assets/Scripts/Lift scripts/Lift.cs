using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float liftspeed = 3;

    [SerializeField] private GameObject lift1;

    [SerializeField] private GameObject lift2;

    [SerializeField] private GameObject lift1IsActive;

    [SerializeField] private GameObject lift2IsActive;

    private Rigidbody2D rb1;
    private CapsuleCollider2D boxCollider1;

    private Rigidbody2D rb2;
    private CapsuleCollider2D boxCollider2;

    [SerializeField] private float lift1StartCoord;
    [SerializeField] private float lift2StartCoord;

    void Awake()
    {
        rb1 = lift1.GetComponent<Rigidbody2D>();
        boxCollider1 = lift1.GetComponent<CapsuleCollider2D>();

        rb2 = lift2.GetComponent<Rigidbody2D>();
        boxCollider2 = lift2.GetComponent<CapsuleCollider2D>();
        
        lift1StartCoord = Math.Abs(rb1.position.y);
        lift2StartCoord = Math.Abs(rb2.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (lift1IsActive.activeSelf || lift2IsActive.activeSelf || IsLiftWorking)
        {
            LiftMove(WhatLiftToUse());
        }
        IsLiftCanWorkAgain();
    }

    [SerializeField] private float lift1coord;
    [SerializeField] private float lift2coord;

    [SerializeField] private bool IsWorkLiftEnd;

    private bool IsLiftWorking;

    private void LiftMove(int whatlift)
    {
        lift1coord = Math.Abs(rb1.position.y);

        lift2coord = Math.Abs(rb2.position.y);

        IsLiftWorking = true;

        if (Math.Abs(lift2StartCoord - lift1coord) < 1e-4 || Math.Abs(lift1StartCoord - lift2coord) < 1e-4 || IsWorkLiftEnd)
        {
            rb1.velocity = new Vector2(0, 0);
            rb2.velocity = new Vector2(0, 0);
            lift1IsActive.SetActive(false);
            lift2IsActive.SetActive(false);
            IsWorkLiftEnd = true;
        }

        if (whatlift == 1 && !IsWorkLiftEnd)
        {
            rb1.velocity = new Vector2(0, liftspeed);
            rb2.velocity = new Vector2(0, liftspeed * -1);
        }

        else if (whatlift == 2 && !IsWorkLiftEnd)
        {
            rb1.velocity = new Vector2(0, liftspeed * -1);
            rb2.velocity = new Vector2(0, liftspeed);
        }
    }

    private int WhatLiftToUse()
    {
        if (lift1StartCoord > lift2StartCoord && lift1IsActive.activeSelf)
        {
            return 1;
        }

        else if(lift2StartCoord > lift1StartCoord && lift2IsActive.activeSelf)
        {
            return 2;
        }

        else
        {
            return 0;
        }
    }

    private void IsLiftCanWorkAgain()
    {
        if (IsWorkLiftEnd)
        {
            var a = lift1StartCoord;
            var b = lift2StartCoord;
            lift1StartCoord = b;
            lift2StartCoord = a;
            IsWorkLiftEnd = false;
        }
    }
}