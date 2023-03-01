using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class Lvl3Block : Brick
{
    private AudioSource audioClip;
    private Vector2 initalPos;
    private Rigidbody2D physics;
    public Sprite brokenBlock, startingBlock;
    private SpriteRenderer spriteRenderer;
    private int amountOfHitsToBreak = 2;
    
    public void Start()
    {
        audioClip = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        initalPos = physics.position;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            if (physics.gravityScale != 0)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),col.collider);
            }
            amountOfHitsToBreak--;
            switch (amountOfHitsToBreak)
            {
                case 1:
                    audioClip.Play();
                    spriteRenderer.sprite = brokenBlock;
                    break;
                case 0:
                    physics.gravityScale = 1;
                    break;
            }
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    
    override 
    public void Restart()
    {
        amountOfHitsToBreak = 2;
        spriteRenderer.sprite = startingBlock;
        gameObject.SetActive(true);
        physics.position = initalPos;
        physics.gravityScale = 0;
    }

    
}
