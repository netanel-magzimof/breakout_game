using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Second Hit");
    }
}
