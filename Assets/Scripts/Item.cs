﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;
    
    [Header("Item Details")]
    public string itemName;
    public string description; 
    public Sprite itemSprite;
    [Header("Item Details")]
    public int amountToChange; //how much mp,hp etc will this give you 
    public int value; //store cost 
    public bool  affectHP, affectMP, affectStr;
    [Header("Weapon/Armor Details")]
    public int weaponStr;
    public int armorStr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}