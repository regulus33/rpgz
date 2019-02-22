﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;
    public bool justStarted;

    public static DialogManager instance;


    void Start()
    {
        instance = this;
        dialogText.text = dialogLines[currentLine];   
        //disbale on instantiation, need to figure out why the FUCK I can't just disable this in the GUI
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogBox.activeInHierarchy)
        {
            //is it being pressed
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {

                    currentLine++;
                    // SPEACH IS ENDING 
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                    }
                    else
                    {
                        CheckIfName();

                        dialogText.text = dialogLines[currentLine];
                    }

                }
                else
                {
                    justStarted = false;
                }
            }
        }

    }

    public void ShowDialog(string[] newLines, bool isPerson)
    {
        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        justStarted = true;
        //only make little box for people active when person is true(youre talking and not inspecting)
        nameBox.SetActive(isPerson);

    }

    public void CheckIfName()
    {   //syntax for my name switching
        if (dialogLines[currentLine].StartsWith("n-"))
        {   //this line is not dialog, its the name
            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }

}
