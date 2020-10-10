﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabs : MonoBehaviour
{

    private Item grabbedItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetItemGrabbed()
    {

        return grabbedItem;

    }
    
    public void GrabItem(Item item)
    {

        grabbedItem = item;

    }

    public Item UseItem()
    {

        return grabbedItem;

    }

    public void RemoveItem()
    {

        grabbedItem = null;

    }
    
}
