using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


[RequireComponent(typeof(SpriteRenderer))]
public class Life : MonoBehaviour
{
    public Sprite emptyHeart, fullHeart; 
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public void changeSprite()
    {
        spriteRenderer.sprite = emptyHeart;
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        spriteRenderer.sprite = fullHeart;
        
    }
}
