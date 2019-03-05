﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    //this is not data this is a socket to put the data in, we will loop through premade ui elements that child this gameobject and populate them with item strings.
    public GameObject items;

    public GameObject menuDisplay;

    public static ItemMenu instance;

    private bool shouldShow = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
 
    }

    // Update is called once per frame
    void Update()
    {
        showMenu();
    }

    public GameObject[] ExtractUIItems()
    {
        int childCount = items.transform.childCount;
        GameObject[] itemsChildren = new GameObject[childCount];
        for(int i = 0; i < childCount; i++)
        {
            itemsChildren[i] = items.transform.GetChild(i).gameObject;
        }
        
        return itemsChildren;
    }
    
    public void showMenu()
    {
        if(Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.M))
        {
            //flip should show bool
            shouldShow = !shouldShow;
            menuDisplay.SetActive(shouldShow);
        }
    }

    public void PopulateItems()
    {
        GameObject[] itemsToModify = ExtractUIItems();   
        for(int i=0; i < PlayerData.instance.itemList.Count; i++)
        {
            itemsToModify[i].GetComponent<UnityEngine.UI.Text>().text = PlayerData.instance.itemList[i];
        }
    }
}
