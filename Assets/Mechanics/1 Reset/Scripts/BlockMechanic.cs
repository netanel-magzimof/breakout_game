using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class BlockMechanic : MonoBehaviour
{
    private bool _isFalling = false;
    private Rigidbody2D _physics;

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        _physics.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isFalling)
            {
                _physics.MovePosition(Vector2.zero);;
                _physics.gravityScale = 0;
                _physics.velocity = Vector3.zero;
                _isFalling = false;
            }
            else
            {
                _physics.gravityScale = 1;
                _isFalling = true;
            }
        }
    }
    
}
