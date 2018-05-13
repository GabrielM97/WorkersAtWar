using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectPlayer : MonoBehaviour
{

    GameObject[] UI;
    GameObject[] players;
    GameObject[] NPC;


    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        NPC = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject p in players)
        {
            if (p.name == "Student" && p.GetComponent<PlayerController>().turn)
            {
                ChangeUI(p);

            }
            else if (p.name == "Worker" && p.GetComponent<PlayerController>().turn)
            {
                ChangeUI(p);
            }

        }

        foreach (GameObject npc in NPC)
        {
            if (npc.name == "Student NPC" && npc.GetComponent<NPCMove>().turn)
            {
                ChangeNPCUI(npc);

            }
            else if (npc.name == "Worker NPC" && npc.GetComponent<NPCMove>().turn)
            {
                ChangeNPCUI(npc);
            }

        }
    }


    public void ChangeUI(GameObject p)
    {

        UI = GameObject.FindGameObjectsWithTag("pUI");

        foreach (GameObject ui in UI)
        {
            if (ui.name == "NameField")
            {
                ui.GetComponentInChildren<Text>().text = p.name;
            }
            if (ui.name == "HealthField")
            {
                ui.GetComponent<Text>().text = p.GetComponent<PlayerController>().HitPoints.ToString();
            }
            if (ui.name == "APField")
            {

                ui.GetComponent<Text>().text = p.GetComponent<PlayerController>().NumOfActions.ToString();
            }
            if (ui.name == "Ability1Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<PlayerController>().ability1;
                ui.GetComponent<Button>().interactable = true;
            }
            if (ui.name == "Ability2Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<PlayerController>().ability2;
                ui.GetComponent<Button>().interactable = true;
                
            }
            if (ui.name == "Ability3Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<PlayerController>().ability3;
                ui.GetComponent<Button>().interactable = true;
            }
            if (ui.name == "Ability4Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<PlayerController>().ability4;
                ui.GetComponent<Button>().interactable = true;
            }
            if (ui.name == "TextA1")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<PlayerController>().ability1.name;
            }
            if (ui.name == "TextA2")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<PlayerController>().ability2.name;
            }
            if (ui.name == "TextA3")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<PlayerController>().ability3.name;
            }
            if (ui.name == "TextA4")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<PlayerController>().ability4.name;
            }

        }

    }

    public void ChangeNPCUI(GameObject p)
    {

        UI = GameObject.FindGameObjectsWithTag("pUI");

        foreach (GameObject ui in UI)
        {
            if (ui.name == "NameField")
            {
                ui.GetComponentInChildren<Text>().text = p.name;
            }
            if (ui.name == "HealthField")
            {
                ui.GetComponent<Text>().text = p.GetComponent<NPCMove>().HitPoints.ToString();
            }
            if (ui.name == "APField")
            {

                ui.GetComponent<Text>().text = p.GetComponent<NPCMove>().NumOfActions.ToString();
            }
            if (ui.name == "Ability1Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<NPCMove>().ability1;
                ui.GetComponent<Button>().interactable = false;
            }
            if (ui.name == "Ability2Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<NPCMove>().ability2;
                ui.GetComponent<Button>().interactable = false;
            }
            if (ui.name == "Ability3Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<NPCMove>().ability3;
                ui.GetComponent<Button>().interactable = false;
            }
            if (ui.name == "Ability4Btn")
            {
                ui.GetComponent<Image>().sprite = p.GetComponent<NPCMove>().ability4;
                ui.GetComponent<Button>().interactable = false;
            }
            if (ui.name == "TextA1")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<NPCMove>().ability1.name;
            }
            if (ui.name == "TextA2")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<NPCMove>().ability2.name;
            }
            if (ui.name == "TextA3")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<NPCMove>().ability3.name;
            }
            if (ui.name == "TextA4")
            {
                ui.GetComponentInChildren<Text>().text = p.GetComponent<NPCMove>().ability4.name;
            }

        }


    }
}
