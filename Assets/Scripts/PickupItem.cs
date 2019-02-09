﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickUp;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if(canPickUp && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canPickUp = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canPickUp = false;
        }
    }
}