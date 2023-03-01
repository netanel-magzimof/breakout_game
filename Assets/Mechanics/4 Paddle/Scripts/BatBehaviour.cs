using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BatBehaviour : MonoBehaviour
{
    private float speed = 10;
    private Rigidbody2D _physics;
    private bool rightClicked, leftClicked;
    
    

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rightClicked = Input.GetKey(KeyCode.RightArrow);
        leftClicked = Input.GetKey(KeyCode.LeftArrow);
    }

    private void FixedUpdate()
    {
        if (rightClicked)
        {
            _physics.AddForce(new Vector2(speed,0));
        }

        if (leftClicked)
        {
            _physics.AddForce(new Vector2(-speed,0));
        }
        
    }
}
