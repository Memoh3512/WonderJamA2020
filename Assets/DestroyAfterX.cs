﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterX : MonoBehaviour
{
    public float temp;

    private float curr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curr += Time.deltaTime;
        if (curr >= temp)
        {
            Destroy(gameObject);
        }
    }
}