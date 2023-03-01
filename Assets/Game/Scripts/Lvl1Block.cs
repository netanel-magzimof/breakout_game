using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1Block : Brick
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }
        gameObject.SetActive(false);
    }
    
    override 
    public void Restart()
    {
        gameObject.SetActive(true);
    }
}
