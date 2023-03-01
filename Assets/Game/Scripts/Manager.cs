using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Manager : MonoBehaviour
{
    public BlockManager blockManager;
    public Button restartButton;
    
    private Vector2 DEFAULT_BAT_POSITION = new Vector2(0,-4.2f);

    public Life[] livesUI;

    private int lives;
    
    public Ball ball;
    
    public Rigidbody2D batPhysics;

    public Bat bat;
    [SerializeField]
    private TextMeshProUGUI wonLabel = default;
    [SerializeField]
    private TextMeshProUGUI lostLabel = default;

    private void Start()
    {
        lives = livesUI.Length;
    }

    private void DecreaseLife()
    {
        lives--;
        if (lives >= 0)
        {
            livesUI[lives].changeSprite();
            batPhysics.position = DEFAULT_BAT_POSITION;
            if (lives>0) ball.OutOfBounds();
            else
            {
                ball.gameObject.SetActive(false);
                lostLabel.enabled = true;
                restartButton.gameObject.SetActive(true);
            }
        }
        
    }
    
    public void DidLoseBall()
    {
        bat.ActivateBat(false);
        DecreaseLife();
    }

    private void Update()
    {
        if (ActiveChildCount() <= 0)
        {
            wonLabel.enabled = true;
            ball.gameObject.SetActive(false);
            bat.ActivateBat(false);
            restartButton.gameObject.SetActive(true);
        }
    }

    private int ActiveChildCount()
    {
        int counter = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf) counter++;
        }
        return counter;
    }

    public void Restart()
    {
        blockManager.Restart();
        lives = livesUI.Length;
        foreach (Life life in livesUI)
        {
            life.Restart();
        }
        ball.Restart();
        wonLabel.enabled = false;
        lostLabel.enabled = false;
        restartButton.gameObject.SetActive(false);
        batPhysics.position = DEFAULT_BAT_POSITION;
    }
}