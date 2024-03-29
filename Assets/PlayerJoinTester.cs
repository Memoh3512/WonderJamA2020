﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinTester : MonoBehaviour
{
    private PlayerInput playerInput { get; set; }
    [Range(1,4)]
    public int playerNb = 0;

    private void Update()
    {

        if (PlayerExists(playerNb-1))
        {
            
            if (PlayerInputs.GetPlayerController(playerNb-1).gp.enabled)
            {
            
                //playerJoined
                transform.Find("PressStart").GetComponent<PressStartBlinker>().StopAllCoroutines();
                transform.Find("PressStart").GetComponent<TextMeshProUGUI>().enabled = false;
                transform.Find("Player").GetComponent<Image>().enabled = true;
                Color col = GetComponent<Image>().color;
                float h,s,v;
                Color.RGBToHSV(col, out h, out s, out v);
                col = Color.HSVToRGB(h, s, 1);
                GetComponent<Image>().color = col;

            }   
            
        }

        if (PlayerExists(0))
        {

            if (PlayerInputs.GetPlayerController(0).selectButton.wasPressedThisFrame)
            {
                AlchemyValues.playerInventory= new List<Item>[PlayerInputs.playerAdded+1];
                for (int i = 0; i < AlchemyValues.playerInventory.Length; i++)
                {
                    AlchemyValues.playerInventory[i]=new List<Item>();
                }
                SceneManager.LoadScene("DayTransition",LoadSceneMode.Single);
            }
            
        }

    }

    private bool PlayerExists(int player)
    {
        return PlayerInputs.GetPlayerController(player) != null;
    }
}
