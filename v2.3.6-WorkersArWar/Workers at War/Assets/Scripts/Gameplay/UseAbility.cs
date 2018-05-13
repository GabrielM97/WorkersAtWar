using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    //Ability cooldowns
    public int CoffeeThrow_CD = 0;
    public int BookSlap_CD = 0;
    public int Sleep_CD = 0;
    public int Plagiarism_CD = 0;

    GameObject activePlayer = null;

    public bool abilty1InUse = false;
    public bool abilty2InUse = false;
    public bool abilty3InUse = false;
    public bool abilty4InUse = false;

    Ability ability = new Ability();

    void Update()
    {
        CheckTurn();
        CoffeeThrow_CD = 0;
        BookSlap_CD = 0;
        Sleep_CD = 0;
        Plagiarism_CD = 0;
        if (activePlayer.tag == "Player")
        {
            PlayerController ap = activePlayer.GetComponent<PlayerController>();

            if (abilty1InUse)
            {
                if (activePlayer.name == "Student")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        ability.Attack(ap.targetUnit, activePlayer);
                        Debug.Log("Hot Coffee Thrown!");
                        CoffeeThrow_CD = 2;

                        //reset
                        abilty1InUse = false;
                        ap.targetUnit = null;
                    }
                }
                else if (activePlayer.name == "Worker")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        ability.Attack(ap.targetUnit, activePlayer);
                        Debug.Log("Paper Plane Thrown!");

                        //reset
                        abilty1InUse = false;
                        ap.targetUnit = null;
                    }
                }
            }
            else if (abilty2InUse)
            {
                if (ap.name == "Student")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        ability.Attack(ap.targetUnit, activePlayer);
                        Debug.Log("Book Slap!");
                        BookSlap_CD = 3;

                        //reset
                        abilty2InUse = false;
                        ap.targetUnit = null;
                    }
                }
                else if (ap.name == "Worker")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        ability.Attack(ap.targetUnit, activePlayer);
                        Debug.Log("Arm the Spitball cannons!");

                        //reset
                        abilty2InUse = false;
                        ap.targetUnit = null;
                    }
                }
            }
            else if (abilty3InUse)
            {
                if (activePlayer.gameObject.name == "Student")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        Debug.Log("Falling Asleep zZzZzZ ");
                        CoffeeThrow_CD = 0;
                        BookSlap_CD = 0;
                        Plagiarism_CD = 0;
                        Sleep_CD = 5;

                        //reset
                        abilty3InUse = false;
                        ap.targetUnit = null;
                    }
                }
                else if (activePlayer.gameObject.name == "Worker")
                {//Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        Debug.Log("Let's bury them in work!");
                        ability.Attack(ap.targetUnit, activePlayer);

                        //reset
                        abilty3InUse = false;
                        ap.targetUnit = null;
                    }
                }
            }
            else if (abilty4InUse)
            {
                if (activePlayer.name == "Student")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        ability.Attack(ap.targetUnit, activePlayer);
                        Debug.Log("You have Plagiarized you oponents abilities!");
                        Plagiarism_CD = 6;

                        //reset
                        abilty4InUse = false;
                        ap.targetUnit = null;
                    }
                }
                else if (activePlayer.name == "Worker")
                {
                    //Do the attack if there's a target
                    if (ap.targetUnit != null)
                    {
                        Debug.Log("I'm in a good mood!");

                        //reset
                        abilty4InUse = false;
                        ap.targetUnit = null;
                    }
                }
            }
        }
        
        
    }

    // Checks if any of the player units is currently taking its turn and set them to be the active player. 
    void CheckTurn()
    {
        //Get all the players 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            // Get the each player's turn.
            bool turn = player.GetComponent<PlayerController>().turn;

            if (turn)
            {
                activePlayer = player;
            }
        }

        //Get all the players 
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in npcs)
        {
            // Get the each player's turn.
            bool turn = npc.GetComponent<NPCMove>().turn;

            if (turn)
            {
                activePlayer = npc;
            }
        }
    }

    /** 
     * These ability functions are being called when the corresponding button is clicked.
     * In each function the ablity gets switched depending on who is the active player. 
     **/
    public void Ability1()
    {
        PlayerController pc = activePlayer.GetComponent<PlayerController>();

        //If when we click the button the ability is in use, turn it off. 
        if (abilty1InUse)
        {
            abilty1InUse = false;
            pc.attack = false;
        }
        else if (CoffeeThrow_CD == 0)
        {
            // Tell the unit to attack
            pc.attack = true;
            abilty1InUse = true;
        }
    }

    public void Ability2()
    {
        PlayerController pc = activePlayer.GetComponent<PlayerController>();

        //If when we click the button the ability is in use, turn it off. 
        if (abilty2InUse)
        {
            abilty2InUse = false;
            pc.attack = false;
        }
        else if (BookSlap_CD == 0)
        {
            // Tell the unit to attack
            pc.attack = true;
            abilty2InUse = true;
        }
    }

    public void Ability3()
    {
        PlayerController pc = activePlayer.GetComponent<PlayerController>();

        //If when we click the button the ability is in use, turn it off. 
        if (abilty3InUse)
        {
            abilty3InUse = false;
            pc.attack = false;
        }
        else if (Sleep_CD == 0)
        {
            // Tell the unit to attack
            pc.attack = true;
            abilty3InUse = true;
        }
    }

    public void Ability4()
    {
        PlayerController pc = activePlayer.GetComponent<PlayerController>();

        //If when we click the button the ability is in use, turn it off. 
        if (abilty4InUse)
        {
            abilty4InUse = false;
            pc.attack = false;
        }
        else if (Plagiarism_CD == 0)
        {
            // Tell the unit to attack
            pc.attack = true;
            abilty4InUse = true;
        }
    }

    public void Reset()
    {
        PlayerController pc = activePlayer.GetComponent<PlayerController>();

        abilty1InUse = false;
        abilty2InUse = false;
        abilty3InUse = false;
        abilty4InUse = false;

        if (activePlayer.tag == "Player")
            pc.attack = false;
    }

    //TODO
    //public void DoAbility(bool inUse, int cooldown, text){}
}
