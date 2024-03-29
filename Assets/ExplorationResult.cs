﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplorationResult : MonoBehaviour
{
    public float minX, maxX; // 130,390

    void Start()
    {

        float distanceBetweenDisplays = (maxX - minX) / AlchemyValues.explorationPlayers.Count;
        for (int i = 0; i < AlchemyValues.explorationPlayers.Count; i++)
        {
            GameObject display = Instantiate((GameObject)Resources.Load("Prefabs/PlayerResult"), GameObject.Find("Canvas").transform);
            display.transform.localPosition = display.transform.localPosition = new Vector3(minX + i * distanceBetweenDisplays, display.transform.localPosition.y, display.transform.localPosition.z);
            string text = "Player " + (AlchemyValues.explorationPlayers[i]+1) + "\n\n";

            for (int j = 0; j < AlchemyValues.playerInventory[AlchemyValues.explorationPlayers[i]].Count; j++)
            {
                text = text + "\n" + AlchemyValues.playerInventory[AlchemyValues.explorationPlayers[i]][j].name + " X " + AlchemyValues.playerInventory[AlchemyValues.explorationPlayers[i]][j].qty;
            }
            display.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    
}
