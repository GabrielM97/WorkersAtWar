using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour 
{	
	public bool occupied = false;
	public bool current = false; 
	public bool selected = false; 
	public bool selectable = false;
    public bool attackTile = false;
	public List<Tile> adjacencyMatrix = new List<Tile> ();

	//Breath First search (BFS) flags
	public bool visited = false;
	public Tile parent = null; 
	public int distance = 0;

    //for A*
    public float f = 0;
    public float g = 0;
    public float h = 0;

    private void Awake()
    {
        Reset();
    }

    void Update ()
	{
		AssignMaterial();
	}

    //Switches the material of the tile depending on the colour
    public void AssignMaterial()
    {
        if (current)
        {
            GetComponent<Renderer>().material = Resources.Load<Material>("SelectedTile");
        }
        else if (occupied)
		{
			GetComponent<Renderer> ().material = Resources.Load<Material>("OccupiedTile"); 
		} 
		else if (selected) 
		{
			GetComponent<Renderer> ().material = Resources.Load<Material>("SelectedTile"); 
		}
		else if (selectable) 
		{
			GetComponent<Renderer> ().material = Resources.Load<Material>("SelectableTile"); 
		}
        else if (attackTile)
        {
            GetComponent<Renderer>().material = Resources.Load<Material>("OccupiedTile");
            
        }
		else 
		{
			GetComponent<Renderer> ().material = Resources.Load<Material>("GrassTile");
		}
	}

    //Resets the tile
    public void Reset()
    {
        occupied = false;
        current = false;
        selected = false;
        selectable = false;
        attackTile = false;
        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(Tile targetTile)
	{
        Reset();

        // for A* we want to find the path to the tile so target must be passes in.
        //Finds adjacent tiles
        CheckTile (Vector3.forward, targetTile);
		CheckTile (Vector3.back, targetTile);
		CheckTile (Vector3.left, targetTile);
		CheckTile (Vector3.right, targetTile);
	}

    //Checks if there are any adjacent tiles to this one
	public void CheckTile(Vector3 direction, Tile TargetTile)
	{
		float jumpHeight = 1;

		//Half of the size of the box in each direction
		Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight)/2, 0.25f);

		//Checks if there's something on a specific side of the tile, where transform.position is the position vector and direction is adding one in a specific direction of the position. 
		//EX a tile at pos (5, 0, 0) + right direction vector (1, 0, 0) = pos of tile to the right of the first. 
		Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents); //?? why an array here when only checking in one direction!!!

		foreach (Collider c in colliders) 
		{
			Tile tile = c.GetComponent<Tile> (); // Check if it's a tile

            if (tile != null && !tile.occupied) //If checking occupied first it might crash when it's null so check if it's null first
			{
				RaycastHit hit;

				//If sth is not on the tile
				if(!Physics.Raycast(transform.position, Vector3.up, out hit, 1) || (tile == TargetTile)) // inlcudes target but gets the tile closest to it.
				{
					adjacencyMatrix.Add(tile);
                }
			}
		}
	}

}
//TODO: research Unity loader to optimise