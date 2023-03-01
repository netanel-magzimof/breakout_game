using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Brick[] bricks;

    public void Restart()
    {
        foreach (Brick brick in bricks)
        {
            brick.Restart();
        }
        
    }
}
