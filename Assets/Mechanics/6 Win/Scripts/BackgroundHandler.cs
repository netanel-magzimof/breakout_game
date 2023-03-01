using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundHandler : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.color = Color.red;;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            _renderer.color = Color.green;
        }
    }
}
