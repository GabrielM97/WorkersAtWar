using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NOTE: Is teamList redundant? Why not add it to the unitTurn queue straight away??
//NOTE: must be attached to all the tiles in the map or their parent
public class TurnManager : MonoBehaviour
{
    /*  Manages all the teams in the game.
     *  Contains a list of all the units in a team and the name of the team as the key.
     */
    static Dictionary<string, List<TileMovement>> units = new Dictionary<string, List<TileMovement>>();

    /*  Stores the names of the teams in the order at which they will take turns.
     *  The first active team is the one at the top of the list. 
     */
    static Queue<string> teamTurnKey = new Queue<string>(); 

    //Stores the units form a team in the order at which they will take turns. 
    static Queue<TileMovement> unitTurn = new Queue<TileMovement>();
    Dictionary<string, List<TileMovement>> numOfUnits;

    void Update ()
    {
		if(unitTurn.Count == 0)
        {
            InitTeamTurnQueue();
        }
	}

    //Adds the active team to the unitTurn queue so each unit can take it's turn.
    static void InitTeamTurnQueue()
    {
        //Gets whose turn it is by picking a team with the turn key and passing it's units to teamList.
        List<TileMovement> teamList = units[teamTurnKey.Peek()];
        if(teamList.Count == 0)
        {
            Debug.Log(teamList[0].tag + " team lost");
        }

        foreach (TileMovement unit in teamList)
        {
            //Add to turn queue
            if (unit.isAlive)
            {
                Debug.Log(unit.name);
                unitTurn.Enqueue(unit);
            }
           
            
        }

        StartTurn();
    }

    //Tells the first unit in the team to take their turn.
    public static void StartTurn()
    {
        if(unitTurn.Count > 0)//REPEATS IN END TURN 
        {
            //Get the first unit in the active team
            unitTurn.Peek().BeginTurn();
        }
    }

 
    public void EndTurn()
    {
        //Removes that unit from the unitTurn queue

        TileMovement unit = unitTurn.Dequeue();

        //Deinitialize if need be
        
        unit.EndTurn();

        

        if (unitTurn.Count > 0)//REPEATS IN END TURN 
        {
            StartTurn();
        }
        else
        {
            //No more units in the team left so make this team inactive
            string teamName = teamTurnKey.Dequeue();

            //Put that team at the back of the queue as they just took their turn
            teamTurnKey.Enqueue(teamName);

            //Get the next team and starts the turn of the first unit.
            InitTeamTurnQueue();
        }
    }


    //Uses the Publish-Subscriber pattern
    public static void AddUnit(TileMovement unit)
    {
        List<TileMovement> unitList;

        if (!units.ContainsKey(unit.tag))//Add a new team 
        {
            unitList = new List<TileMovement>();
            units[unit.tag] = unitList;
            
            if (!teamTurnKey.Contains(unit.tag))
            {
                teamTurnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            unitList = units[unit.tag]; //Get this unit's team
        }

        unitList.Add(unit);
    }



    public List<TileMovement> GetPlayers()
    {
        return units["Player"];
    }

    public List<TileMovement> GetNPC()
    {
        return units["NPC"];
    }
}
