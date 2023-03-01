using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Lvl2Block : Brick
{
    private AudioSource audioClip;
    public Sprite startingBlock;
    private int _amountOfHitsToBreak = 2;
    public Sprite brokenBlock;
    private SpriteRenderer _spriteRenderer;
    
    
    
    private void Start()
    {
        audioClip = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    override 
    public void Restart()
    {
        _amountOfHitsToBreak = 2;
        _spriteRenderer.sprite = startingBlock;
        gameObject.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ball"))
        {
            return;
        }
        _amountOfHitsToBreak--;
        switch (_amountOfHitsToBreak)
        {
            case 1:
                audioClip.Play();
                _spriteRenderer.sprite = brokenBlock;
                break;
            case 0:
                gameObject.SetActive(false);
                break;
        }
    }
}
