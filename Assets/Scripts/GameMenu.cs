﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    //for switching 
    public GameObject[] windows;

    private CharStats[] playerStats;
    public Text[] nameText, hpText, mpText, levlText, expText;
    public Slider[] expSlider; 
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public ItemButton[] itemButtons; 

    public static GameMenu instance;

    public string selectedItem;

    public Item activeItem;

    public Text itemName, itemDescription, useButtonText;

    // Start is called before the first frame update

    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;
    public Image statusImage;  
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false; 
                CloseMenu();

            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {

        playerStats = GameManager.instance.playerStats; 
        for(int i = 0; i < playerStats.Length; i++) 
        {
            //if the player object these stats belong to is actively in the game 
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                
                //get his stat UI container activated
                charStatHolder[i].SetActive(true);
                //hook up player stats with UI display
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                levlText[i].text = "MP: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;

            } else {

                charStatHolder[i].SetActive(false);

            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else 
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        //reset window state
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus() 
    {
        UpdateMainStats();
        //get status of character show and populate
        StatusChar(0);
        
        for(int i = 0; i < statusButtons.Length; i++)
        {
            
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selected) 
    {
        statusName.text = playerStats[selected].charName;
        
        statusHP.text = playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        
        statusStr.text = playerStats[selected].strength.ToString();
        
        statusDef.text = playerStats[selected].defence.ToString();
        
        if(playerStats[selected].equippedWpn != "")
        {
            statusWpnEqpd.text = playerStats[selected].equippedWpn;
        }
        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
        
        if(playerStats[selected].equippedWpn != "")
        {
            statusArmrEqpd.text = playerStats[selected].equippedArmr;
        }
        statusArmrPwr.text = playerStats[selected].armrPwr.ToString();

        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel]-playerStats[selected].currentEXP).ToString();

        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for(int i=0; i < itemButtons.Length; i++){
            itemButtons[i].buttonValue = i;
            
            if (GameManager.instance.itemsHeld[i] != null && GameManager.instance.itemsHeld[i] != "" )
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            } else {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }

        }
    }

    public void SelectItem(Item newItem)
    {   
       activeItem = newItem;

       if(activeItem.isItem){
           useButtonText.text = "Use";
       }

       if(activeItem.isWeapon || activeItem.isArmor) 
       {
           useButtonText.text = "Equip";
       }

       itemName.text = activeItem.itemName;
       itemDescription.text = activeItem.description;
    }
}
