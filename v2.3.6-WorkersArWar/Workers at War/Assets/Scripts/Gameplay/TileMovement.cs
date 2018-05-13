using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public TurnManager tm; 

    GameObject[] tiles;
    List<Tile> selectableTiles = new List<Tile>();
    Stack<Tile> pathToSelTile = new Stack<Tile>();
    Tile currentTile;

    int actionsTaken = -1;

    public bool turn = false;
    public bool moving = false;
    public int NumOfActions = 6;
    public bool isAlive = true;
    public int moveRange = 3;
    public float moveSpeed = 2;
    public float jumpHeight = 1;

    Vector3 velocity = new Vector3();
    Vector3 direction = new Vector3();

    float halfHeight = 0;
    public Tile actualTargetTile;// target tile that we want the npc to move to 


    protected void Init()
    {
        halfHeight = GetComponent<Collider>().bounds.extents.y;
        
        TurnManager.AddUnit(this); 
    }


    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    //Uses RayCast to check what tile is under the target.
    //RETURN: the target tile.
    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile targetTile = null;

        if (Physics.Raycast(target.transform.position, Vector3.down, out hit, 5))
        {
            targetTile = hit.collider.GetComponent<Tile>();
        }

        return targetTile;
    }

    //Finds the adjacent tiles in the range of 'moves'
    public void ComputeAdjacencyMatrices(Tile targetTile)
    {
        //Stores all the tiles present on the map every frame so that tiles can be added or removed if need be.
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(targetTile);
        }
    }

    //BFS
    public void FindSelectableTiles(bool isAttackMode)
    {
        ComputeAdjacencyMatrices(null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;

        // While the queue is not empty
        while (process.Count > 0)
        {
            //Process the first tile
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            if (isAttackMode)
                t.attackTile = true;
            else
                t.selectable = true;


            if (t.distance < moveRange)
            {
                foreach (Tile tile in t.adjacencyMatrix)
                {
                    RaycastHit hit;
                    
                    //If there is something ontop of an adjacent tile
                    if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                    {
                        tile.occupied = true;
                    }

                    if ((!tile.visited && !tile.occupied))
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile); //adds all adjacent tiles to the queue, which will create a triangle shape
                    }
                }
            }
        }
    }

    //Finds the path from the destination to the current tile
    public void FindPath(Tile destination)
    {
        pathToSelTile.Clear();

        RaycastHit hit;

        if (Physics.Raycast(destination.transform.position, Vector3.up, out hit, 1))
        {
            destination.occupied = true;
            destination = destination.parent;

            destination.selected = true;
        }else
        {
            destination.selected = true;
        }

        moving = true;

        Tile previous = destination;

        while (previous != null)
        {
            if (NumOfActions > 0)
            {
                pathToSelTile.Push(previous);
                previous = previous.parent;

                actionsTaken++;
            }
        }
    }

    //Move a unit from one tile to the next
    public void Move()
    {
        if (pathToSelTile.Count > 0)
        {
            Tile t = pathToSelTile.Peek();
            Vector3 destinationPos = t.transform.position;

            //Calculate the position on top of the target tile
            destinationPos.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, destinationPos) >= 0.05f)
            {
                CalcDirection(destinationPos);
                CalcHVelocity();

                //Makes the entity face in the direction it's heading
                transform.forward = direction;

                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile Center reached
                transform.position = destinationPos;
                pathToSelTile.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();

            //Allows the player to select a new tile to move to
            moving = false;
            NumOfActions -= actionsTaken;
            actionsTaken = -1;
        }

        if (NumOfActions < moveRange)
        {
            moveRange = NumOfActions;
        }

        if (NumOfActions <= 0)
        {
            Debug.Log("You are out of moves");
            tm.EndTurn();
            NumOfActions = 5;
            moveRange = 3;
        }
    }


    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile t in selectableTiles)
        {
            t.Reset();
            
        }
        
        selectableTiles.Clear();
    }


    void CalcDirection(Vector3 destPos)
    {
        direction = destPos - transform.position;

        //Converting direction into a Unit vector 
        direction.Normalize();
    }


    void CalcHVelocity()
    {
        velocity = direction * moveSpeed;
    }
    

    public void BeginTurn()
    {
        turn = true;
    }


    public void EndTurn()
    {
        turn = false;
        NumOfActions = 5;
    }


    //----------AI Functions----------//
    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];// takes the first value as the lower

        foreach(Tile t in list)// check remaining tiles to find lowest
        {
           

            if (t.f < lowest.f && !t.occupied)
            {
                lowest = t;
            }
        }

        list.Remove(lowest); // if the lowest is found pop from list and return it;

        return lowest;
    }

    // Creates path to closest avaliable tile to target.
    protected Tile findEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();
        
        // Starting the search from the parent as it is the tile closest to the target.
        Tile next = t.parent;

        while (next != null && !next.occupied)
        {

            tempPath.Push(next);
            next = next.parent;

        }


        if (tempPath.Count <= moveRange )
        {
            t.occupied = true;
            return t.parent;
        }

        Tile endtTile = null;

        for (int i = 0; i < moveRange+1; i++)// if not in range pop off the tiles based on the move range untill only the available tile remains
        {
            endtTile = tempPath.Pop();
        }

        return endtTile;

    }


    protected void AIPathFinding(Tile target)
    {
        ComputeAdjacencyMatrices(target);
        GetCurrentTile();


        //----------------A*-----------------//
        // create open and close lists

        List<Tile> openList = new List<Tile>();// any tile not processed
        List<Tile> closeList = new List<Tile>();// every tile that has been processed


        openList.Add(currentTile);
        // currentTile.parent is null

        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);// cal distance between c.tile and t.tile
        currentTile.f = currentTile.h; // should be h +  g but as g is 0 adding it would be pointless at this moment 


        while (openList.Count > 0)// check list is not empty 
        {
            Tile t = FindLowestF(openList);

            closeList.Add(t);

            if(t == target)// if target found A* is done
            {
                actualTargetTile = findEndTile(t);
                RaycastHit hit;

                //check if occupied.
                if(Physics.Raycast(actualTargetTile.transform.position, Vector3.up, out hit, 1) || currentTile == actualTargetTile)
                {
                    actualTargetTile.occupied = true;
                    tm.EndTurn();
                }
               
                else
                {
                    FindPath(actualTargetTile);   
                }
                return;
            }

            foreach(Tile tile in t.adjacencyMatrix)
            {
                if(closeList.Contains(tile))
                {
                    // DONT DO ANYTHING, already processed !
                }
                else if ( openList.Contains(tile))// found a second way to a tile which has been processed
                {
                   
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position); // temp g created so that the newly found path  can be check against the original to see which would be faster

                    if (tempG < tile.g)//fatser way was found \(^.^)/
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tempG + tile.h;


                    }
                }
                else// new node
                {
                    tile.parent = t;// set parent to be t

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);// get cost of g (t's g cost + distance between t and current tile)

                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);// calculate h cost of tile (distance between current tile and the target tile)

                    tile.f = tile.g + tile.h;

                    openList.Add(tile);// once tile has been check add it to open list allows it to be check again in the future

                }
            }
        }

        //Tudo - What if there is no path to the target tile
        
        Debug.Log("Path not found");
    }
}
