using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : TileMovement
{
    public float HitPoints = 0;
    public int baseDMG = 10;
    public bool attack = false;
    static int unitsDead = 0;
    public GameObject targetUnit;

    /* ABILITIES
     * References to the sprites for each ability with the names of the abilities.
     * Used to swap the battle hud for each character. 
     */
    public Sprite ability1;
    public Sprite ability2;
    public Sprite ability3;
    public Sprite ability4;

    void Awake()
    {
        Init();
    }

    void Update()
    {

        if(unitsDead == tm.GetPlayers().Count)
        {
            Debug.Log("NPC Wins !!!");
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(4);
            
        }

        if(!isAlive)
        {
            Destroy(gameObject);
        }
        if (!turn)
        {
            
            return;
        }

        if (attack)
        {
            FindSelectableTiles(attack);
            GetAttackTarget();
            

        }
        else if (!moving)
        {
            FindSelectableTiles(attack);
            GetMouseClick();
        }
        else
        {
            Move();
        }
    }

    // Handles the clicks to move
    public void GetMouseClick()
    {
        //Creates a ray at the position of the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray, out hit))
        { 
            if (hit.collider.tag == "Tile")
            {
                Tile t = hit.collider.GetComponent<Tile>();

                if (t.selectable && moveRange > 0)
                {
                    //t.gameObject.GetComponent<Renderer>().material.color = Color.blue;

                    if (Input.GetMouseButtonUp(0))
                    {
                        FindPath(t);
                    }
                }
            }
        }
    }

    //Handles the clicks to attack an enemy unit.
    public void GetAttackTarget()
    {
        //Gets click
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        RaycastHit targetPos;

        //If player left clicks on an NPC
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonUp(0)) 
            {
                if (hit.collider.tag == "NPC") 
                {
                    //Set the target to be that game object.
                    targetUnit = hit.collider.gameObject;

                    /* Check if the NPC is in range of the player 
                     * by sending a ray from the NPCs position down.
                     * This is done to check if the tile the NPC is on is in range. 
                     */ 
                    if (Physics.Raycast(targetUnit.transform.position, Vector3.down, out targetPos, 1))
                    {
                        //We use occupied as that is the state of the tile when thre is something on top of it. 
                        if (targetPos.collider.tag == "Tile" && targetPos.collider.GetComponent<Tile>().occupied)
                        {
                            Debug.Log("hit target" + targetUnit.name);
                            attack = false;
                        }
                        else
                        {
                            targetUnit = null;
                        }
                    } 
                }
            }
        }

    }

    public void Die()
    {
        isAlive = false;
        unitsDead++;
    }
}
