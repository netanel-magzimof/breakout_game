using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
