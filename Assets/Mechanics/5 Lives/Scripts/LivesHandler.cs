using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesHandler : MonoBehaviour
{
    private GameObject[] lives;
    private int livesLeft;

    private void Start()
    {
        livesLeft = transform.childCount;
        lives = new GameObject[livesLeft];
        for (int i = 0; i < livesLeft; i++)
        {
            lives[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && livesLeft >= 1)
        {
            livesLeft--;
            Destroy(lives[livesLeft]);
            if (livesLeft == 0)
            {
                print("GAME OVER");
            }
        }
    }
}
