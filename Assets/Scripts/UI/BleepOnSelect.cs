﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BleepOnSelect : MonoBehaviour, ISelectHandler
{
    public bool shouldBleep = true;
    public static BleepOnSelect instance;
    // Start is called before the first frame update
    void Start()
    {
        //do i really need this to be an instance
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleOutBounds()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("event");
        if(shouldBleep){
            AudioManager.instance.PlayUI(0);
        }

    }
   
}
