using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEdgesCollider : MonoBehaviour
{
    private bool isBallTouching;

    private void Start()
    {
        isBallTouching = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isBallTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isBallTouching = false;
    }

    public bool IsBallTriggered()
    {
        return isBallTouching;
    }
}