using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Vector2i
{
    public int x, y;

    public Vector2i(int xVal, int yVal)
    {
        x = xVal;
        y = yVal;
    }

    public int manhattandistance(Vector2i other)
    {
        return Mathf.Abs(x - other.x) + Mathf.Abs(y - other.y);
    }

    public override bool Equals(object obj)
    {
        return obj is Vector2i && this == (Vector2i)obj;
    }

    public static bool operator ==(Vector2i v1, Vector2i v2)
    {
        return (v1.x == v2.x) && (v1.y == v2.y);
    }

    public static bool operator !=(Vector2i v1, Vector2i v2)
    {
        return !(v1 == v2);
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    public override string ToString()
    {
        return x.ToString() + " " + y.ToString();
    }
}

public class Tile
{
    public enum TileType
    {
        Plains = 0,
        Forest,
        Mountain,
        Cave,
        Lake,
        Graveyard,
        Volcano,
        N_TILE_TYPES
    };

    public static int[] terrainPenalty = new int[(int)TileType.N_TILE_TYPES];

    public static int[] defaultTerrainPenalties = new int[(int)TileType.N_TILE_TYPES];

    public bool Traversed { get;  set;}

    public int SpriteNumber { get; private set; }   //Which sprite to draw for this tile

    public TileType Type { get; private set; }

    public Vector2i Position { get; private set; }

    public Tile[] neighbors = new Tile[4];

    public Tile(TileType t, int spriteNumber, Vector2i position)
    {
        Type = t;
        SpriteNumber = spriteNumber;
        Position = position;
        neighbors[(int)Direction.Up] = null;
        neighbors[(int)Direction.Down] = null;
        neighbors[(int)Direction.Left] = null;
        neighbors[(int)Direction.Right] = null;
    }

    public override string ToString()
    {
        return "Type: " + Type.ToString() + " Position: " + Position.ToString();
    }
}

public class TileManager : PersistentObject {

    private Dictionary<Vector2i, Tile> _tiles;  //Tiles that have been seen.
    public int[] probabilities = new int[(int)Tile.TileType.N_TILE_TYPES];  //Probabilities of encountering each terrain type
   

    //Returns a random tile type based on probabilities (assumes probabilities adds up to 100).
    public Tile.TileType getRandomType()
    {
        float value = Random.value;
        float tileMaxValue = 0;
        float divisor = 0;
        foreach(int val in probabilities)
        {
            divisor += val;
        }

        float[] normalizedProbabilities = new float[(int)Tile.TileType.N_TILE_TYPES];

        for(int i = 0; i < (int)Tile.TileType.N_TILE_TYPES; i++)
        {
            normalizedProbabilities[i] = (float)probabilities[i] / divisor;
        }

        for(int i = 0; i < (int)Tile.TileType.N_TILE_TYPES; i++)
        {
            tileMaxValue += probabilities[i];
            if(value <= tileMaxValue)
            {
                return (Tile.TileType)i;
            }
        }
        return Tile.TileType.Plains;
    }

    //Returns the opposite direction of the direction d.
    public static Direction oppositeDirection(Direction d)
    {
        switch(d)
        {
            case Direction.Up:
                return Direction.Down;

            case Direction.Down:
                return Direction.Up;

            case Direction.Left:
                return Direction.Right;

            case Direction.Right:
                return Direction.Left;

            default:
                return Direction.Up;
        }
    }

    //Returns the tile at the position "position" if it exists. If it doesnt exist, returns null.
    public Tile getTile(Vector2i position)
    {
        if(_tiles.ContainsKey(position))
        {
            return _tiles[position];
        }
        return null;
    }

    //Links the tile t with the tile located at position pos, where d is the direction pos is located in relative to tile t.
    private void linkTiles(Tile t, Vector2i pos, Direction d)
    {
        if (_tiles.ContainsKey(pos))
        {
            t.neighbors[(int)d] = _tiles[pos];
            _tiles[pos].neighbors[(int)oppositeDirection(d)] = t;
        }
        else
        {
            t.neighbors[(int)d] = null;
        }
    }

    //Like the tile t with its neighbors.
    public void linkTile(Tile t)
    {
        Vector2i neighborPos;

        neighborPos = t.Position;
        neighborPos.y += 1;
        linkTiles(t, neighborPos, Direction.Up);    //Link neighbhor above this tile

        neighborPos.y -= 2;
        linkTiles(t, neighborPos, Direction.Down);  //Link neighbor below this tile

        neighborPos.y += 1;
        neighborPos.x += 1;
        linkTiles(t, neighborPos, Direction.Right); //Link neighbor right of this tile

        neighborPos.x -= 2;
        linkTiles(t, neighborPos, Direction.Left);  //Link neighbor left of this tile
    }

    //Returns which tiles are visible in an 11x11 screen where center is the center.
    public List<Tile> getVisibleTiles(Vector2i center)
    {
        List<Tile> tiles = new List<Tile>();

        Vector2i botCorner = center;
        botCorner.x -= 5;
        botCorner.y -= 5;

        for (int x = 0; x < 11; x++, botCorner.x += 1)
        {
            for (int y = 0; y < 11; y++, botCorner.y += 1)
            {
                if (_tiles.ContainsKey(botCorner))
                {
                    tiles.Add(_tiles[botCorner]);
                }
            }
            botCorner.y -= 11;
        }

        return tiles;
    }

    //Returns the shortest path from start to dest. If the dest can not be reached befor remainingEnergy runs out (or for other reasons), returns null.
    public List<Tile> getPath(Vector2i start, Vector2i dest, int remainingEnergy)
    {
        if(!_tiles.ContainsKey(dest) || !_tiles.ContainsKey(start) || start.manhattandistance(dest) > remainingEnergy)
        {
            return null;
        }

        List<Tile> path = new List<Tile>();

        Dictionary<Tile, int> distanceFromStart = new Dictionary<Tile, int>();  //Minimum distance from start to each tile.
        Dictionary<Tile, Tile> tileOrigins = new Dictionary<Tile, Tile>();  //Originating tile. (Also acts as a closed list).
        List<Tile> openNodes = new List<Tile>();

        openNodes.Add(_tiles[start]);
        tileOrigins[_tiles[start]] = _tiles[start];
        distanceFromStart[_tiles[start]] = 0;

        Tile curNode, curNeighbor;
        int curDistance;
        bool done = false;
        //While there are still open nodes
        while(!done && openNodes.Count > 0)
        {
            //Pop the first node
            curNode = openNodes[0];
            openNodes.RemoveAt(0);
            
            //For each of its neighbors, get the distance to that neighbor if it exists, and add the distance from start to the current node to that distance to get the distance from start to that neighbor through the current node.
            for(int i = 0; i < 4; i++)
            {
                curNeighbor = curNode.neighbors[i];

                if(curNeighbor == null)
                {
                    continue;
                }

                curDistance = Tile.terrainPenalty[(int)curNeighbor.Type] + distanceFromStart[curNode];

                //If the neighbor has already been found, if the distance through the cur tile to the neighbor is less than the current known shortest distance, change the neighbor's tile origin and distance.
                if(tileOrigins.ContainsKey(curNeighbor) && distanceFromStart[curNeighbor] > curDistance)
                {
                    tileOrigins[curNeighbor] = curNode;
                    distanceFromStart[curNeighbor] = curDistance;
                }
                else if(curDistance <= remainingEnergy) //Else if this is the first time the tile has been found, add it to open nodes and set the tile origins and distance from start.
                {
                    tileOrigins[curNeighbor] = curNode;
                    distanceFromStart[curNeighbor] = curDistance;
                    openNodes.Add(curNeighbor);
                }
                //If the neighbor can not physically be reached, do not add it to open nodes.
                if(curNeighbor.Position == dest)
                {
                    done = true;
                }
            }
        }

        //If the destination tile was not reached, there is no path to that tile.
        if(!tileOrigins.ContainsKey(_tiles[dest]))
        {
            return null;
        }

        curNode = _tiles[dest];
        path.Add(curNode);

        //Recreate the path.
        while(curNode != _tiles[start])
        {
            curNode = tileOrigins[curNode];
            path.Insert(0, curNode);
        }
        path.Insert(0, _tiles[start]);
        return path;
    }

    //Create new tiles around the new position if they have nto already been created.
    public void createTiles(Vector2i newPosition, int viewDistance)
    {
        HashSet<Tile> openTiles = new HashSet<Tile>();      //Current tiles to look through
        HashSet<Tile> closedTiles = new HashSet<Tile>();    //Tiles to ignore (already traversed)
        HashSet<Tile> curNeighbors = new HashSet<Tile>();   //Current neighbors (tiles next to the open tiles that are not in the closed tiles list.
        openTiles.Add(_tiles[newPosition]);                 //Add initial tile
        Tile curNeighbor;

        //Only need to run this loop until it reaches as far as it can view.
        for(int i = 0; i < viewDistance; i++)
        {
            //Go through each node and add its neighbor to curNeighbors if it exists, create a neighbhor there if it doesnt.
            foreach(Tile curNode in openTiles)
            {
                for(int j = 0; j < 4; j++)
                {
                    curNeighbor = curNode.neighbors[j];

                    //If there is no neighbor, create a new neighbor and link it.
                    if(curNeighbor == null)
                    {
                        Vector2i neighborPosition = curNode.Position;
                        switch((Direction)j)
                        {
                            case Direction.Up:
                                neighborPosition.y += 1;
                                break;

                            case Direction.Down:
                                neighborPosition.y -= 1;
                                break;

                            case Direction.Left:
                                neighborPosition.x -= 1;
                                break;

                            case Direction.Right:
                                neighborPosition.x += 1;
                                break;
                        }
                        curNeighbor = new Tile(getRandomType(), Random.Range(0, 4), neighborPosition);
                        _tiles.Add(neighborPosition, curNeighbor);
                        linkTile(_tiles[neighborPosition]);
                        curNeighbors.Add(curNeighbor);
                    }
                    else if(!closedTiles.Contains(curNeighbor)) //Else if the neighbor is not a closed tile, add it to cur neighbors (to prevent the code from going in circles).
                    {
                        curNeighbors.Add(curNeighbor);
                    }
                }
            }

            //Reassign closed and open tiles.
            closedTiles.Clear();
            closedTiles = new HashSet<Tile>(openTiles);
            openTiles.Clear();
            openTiles = new HashSet<Tile>(curNeighbors);
            curNeighbors.Clear();
        }
    }

    public void resetTerrainPenalties()
    {
        Tile.defaultTerrainPenalties.CopyTo(Tile.terrainPenalty, 0);
    }

    void Awake()
    {
        _tiles = new Dictionary<Vector2i, Tile>();  //Create a new tile dictionary
        Vector2i initialPosition = new Vector2i(0, 0);  //Set initial position
        _tiles.Add(initialPosition, new Tile(Tile.TileType.Plains, Random.Range(0, 4), initialPosition));   //Create tile at initial position
        createTiles(initialPosition, 3);    //Create tiles around initial position. TODO: get view distance
        Tile.defaultTerrainPenalties[0] = 1;
        Tile.defaultTerrainPenalties[1] = 2;
        Tile.defaultTerrainPenalties[2] = 3;
        Tile.defaultTerrainPenalties[3] = 2;
        Tile.defaultTerrainPenalties[4] = 2;
        Tile.defaultTerrainPenalties[5] = 2;
        Tile.defaultTerrainPenalties[6] = 4;
        resetTerrainPenalties();
        foreach(Tile t in _tiles.Values)
        {
            Debug.Log(t.ToString());
        }
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
