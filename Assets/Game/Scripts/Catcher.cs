using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public Manager manager;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball")) manager.DidLoseBall();
        if (col.CompareTag("LVL 3")) col.gameObject.SetActive(false);
    }
}