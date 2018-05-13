using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public Sprite abilitySprite; //Not Used

    public int cooldown = 0;
    public int range; //Not Used
    public int damage = 100;
    GameObject healthBar;
    float dmgPercent = 0f;
    Vector3 uiChange;
    GameObject[] UI;
    
    public void Attack(GameObject target, GameObject attacker) //To get the user of the ability
    {
        UI = GameObject.FindGameObjectsWithTag("InfoUi");
        float currentHealth = 0;
        uiChange = new Vector3(1f, 0.9f, 0.7f);
        Debug.Log("Attack " + target.name);

        if (target.tag == "NPC")
        {
            currentHealth = target.GetComponent<NPCMove>().HitPoints;
            target.GetComponent<NPCMove>().HitPoints -= damage;
            healthBar = GameObject.Find(target.name + "HP");
            dmgPercent = target.GetComponent<NPCMove>().HitPoints / currentHealth;

            uiChange = new Vector3(1, (healthBar.transform.localScale.y * dmgPercent), 0.7f);

            printMsg(attacker, target);
            if (target.GetComponent<NPCMove>().HitPoints <= 0)
            {
                printDeathMsg(attacker, target);
                target.GetComponent<NPCMove>().Die();
            }
        }
        else if (target.tag == "Player")
        {
            currentHealth = target.GetComponent<PlayerController>().HitPoints;
            target.GetComponent<PlayerController>().HitPoints -= damage;
            healthBar = GameObject.Find(target.name + "HP");
            dmgPercent = target.GetComponent<PlayerController>().HitPoints / currentHealth;

            uiChange = new Vector3(1, (healthBar.transform.localScale.y * dmgPercent), 0.7f);
            printMsg(attacker, target);
            if (target.GetComponent<PlayerController>().HitPoints <= 0)
            {
                printDeathMsg(attacker, target);
                target.GetComponent<PlayerController>().Die();
            }
        }

        healthBar.transform.localScale = uiChange;
    }


    public void printMsg(GameObject attacker, GameObject target)
    {
        for (int i = 0; i < UI.Length; i++)
        {
            if (UI[i].GetComponentInChildren<Text>().text == "")
            {
                UI[i].GetComponentInChildren<Text>().text = attacker.name + " Attacks  " + target.name + "  For  " + damage.ToString() + "  Damage !";
                break;
            }

            if (i == 4)
            {
                foreach (GameObject ui in UI)
                    ui.GetComponentInChildren<Text>().text = "";
                UI[0].GetComponentInChildren<Text>().text = attacker.name + " Attacks  " + target.name + "  For  " + damage.ToString() + "  Damage !";
            }
        }
    }

    public void printDeathMsg(GameObject attacker, GameObject target)
    {
        for (int i = 0; i < UI.Length; i++)
        {
            if (UI[i].GetComponentInChildren<Text>().text == "")
            {
                UI[i].GetComponentInChildren<Text>().text = target.name + " Was Killed By  " + attacker.name + " !";
                break;
            }

            if (i == 4)
            {
                foreach (GameObject ui in UI)
                    ui.GetComponentInChildren<Text>().text = "";
                UI[0].GetComponentInChildren<Text>().text = target.name + " Was  Killed  By  " + attacker.name + " !";
            }
        }
    }
}
