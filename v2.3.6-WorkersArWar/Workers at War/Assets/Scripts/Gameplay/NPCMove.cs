using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPCMove : TileMovement
{
    GameObject target; // Player
    public int HitPoints = 250;
    public bool attack = false;
    static int UnitsDead = 0;

    /* ABILITIES
    * References to the sprites for each ability with the names of the abilities.
    * Used to swap the battle hud for each character. 
    */
    public Sprite ability1;
    public Sprite ability2;
    public Sprite ability3;
    public Sprite ability4;

    Ability ability = new Ability();


    void Awake ()
    {
        Init();
    }
	
	void Update ()
    {

        if(UnitsDead == tm.GetNPC().Count)
        {
            Debug.Log("Player Wins");
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(3);
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
            moving = false;
            ability.Attack(target, gameObject);
            attack = false;
            tm.EndTurn();
            
        }
        else if (!moving)
        {
            FindNearestTarget();
            CalcPath();

            GetAttackTarget();

            FindSelectableTiles(attack);
            actualTargetTile.selected = true;
        }
        else
        {
            Move();
        }

    }

    void CalcPath()
    {
        Tile targetTile = GetTargetTile(target);
        AIPathFinding(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearest = null;
        float distance = Mathf.Infinity;//set to infinity so that it's always less than on the if in the beginning 

        foreach (GameObject obj in targets)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            
            if (dist < distance)
            {
                distance = dist;
                nearest = obj; 
            }
        }

        target = nearest;
    }


    public void GetAttackTarget()
    {
        RaycastHit hit;

        Debug.Log("the target is " + target.name);

        if (target.tag == "Player")
        {
            if (Physics.Raycast(target.transform.position, Vector3.down, out hit, 1))
            {
                //Set the target to be that game object.
                GameObject targetTile = hit.collider.gameObject;

                

                /* Check if the NPC is in range of the player 
                    * by sending a ray from the NPCs position down.
                    * This is done to check if the tile the Player is on is in range. 
                    */
                //We use occupied as that is the state of the tile when thre is something on top of it. 
                if (targetTile.tag == "Tile" && targetTile.GetComponent<Tile>().occupied)
                {
                    Debug.Log("target in range" + target.name);
                    attack = true;
                }
            }
        }
    }

    public void Die()
    {
        isAlive = false;
        UnitsDead++;
    }

}
